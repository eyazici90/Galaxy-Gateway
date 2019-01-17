using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Autofac.Extras.DynamicProxy;
using Galaxy.Application;
using Galaxy.Bootstrapping;
using Galaxy.Cache.Bootstrapper;
using Galaxy.FluentValidation;
using Galaxy.FluentValidation.Bootstrapper;
using Galaxy.Serilog.Bootstrapper;
using Galaxy.Utf8Json.Bootstrapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Galaxy.Gateway.Application.Contracts;
using Galaxy.Gateway.Application.Services;
using Galaxy.Gateway.Application.Validations;
using Galaxy.Gateway.CommandHandlers;
using Galaxy.Gateway.Middlewares;
using Galaxy.Gateway.QueryHandlers;
using Galaxy.Gateway.Services.Contracts;
using Galaxy.Gateway.Shared;
using Galaxy.Gateway.Shared.Commands;
using Serilog;
using Galaxy.Gateway.API.Aggregator;

namespace Galaxy.Gateway.API
{
    public partial class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var cfgBuilder = new ConfigurationBuilder();

            cfgBuilder.SetBasePath(env.ContentRootPath);
            cfgBuilder.AddJsonFile(path: Settings.PGW_APPSETTINGS_PATH 
                ,optional: false, reloadOnChange: true);
            cfgBuilder.AddJsonFile(path: Path.Combine(Settings.PGW_CONFIGURATION_DIRECTORY , Settings.PGW_CONFIGURATION_PATH)
                ,optional: false, reloadOnChange: true);
           
            Configuration = cfgBuilder.Build(); 
        }

        public IConfiguration Configuration { get; }
         
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddCors();

            services.AddOptions(); 

            services.AddOcelot(Configuration)
                .AddTransientDefinedAggregator<GalaxyGatewayAggregator>();

            var container = this.ConfigureGalaxy(services);

            ConfigureBlackListIPs(container)
                .ConfigureAwait(false)
                .GetAwaiter().GetResult();
            
            return new AutofacServiceProvider(container);
        }
         
        public  void Configure(IApplicationBuilder app, IHostingEnvironment env )
        {
            app.UseCors(builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });

            ConfigureMiddlewares(app);
            
            app.UseOcelot(conf =>
            {
                conf.PreQueryStringBuilderMiddleware = async (ctx, next) =>
                { 
                        await next.Invoke();
                };
                conf.PreErrorResponderMiddleware = async (ctx, next) =>
                { 
                        await next.Invoke();
                        await AssertDownStreamErrorsFromOcelotPipeline(app,ctx);
                };
            })
               .ConfigureAwait(false)
               .GetAwaiter().GetResult();
            
        }

        #region GalaxyConfiguration
        private IContainer ConfigureGalaxy(IServiceCollection services)
        {
            var containerBuilder = GalaxyCoreModule.New
                 .RegisterGalaxyContainerBuilder()
                     .UseGalaxyCore(b =>
                     {
                         b.UseConventionalCommandHandlers(typeof(LogCommandHandler).Assembly, typeof(LogQueryHandler).Assembly);
                          
                         b.RegisterAssemblyTypes(typeof(LogService).Assembly)
                            .AssignableTo<IApplicationService>()
                            .AsImplementedInterfaces()
                            .EnableInterfaceInterceptors()
                            .InterceptedBy(typeof(ValidatorInterceptor))
                            .InstancePerLifetimeScope();
                     })
                     .UseGalaxyUtf8JsonSerialization()
                     .UseGalaxyFluentValidation(typeof(LogRequestCommandValidation).Assembly)
                     .UseGalaxyInMemoryCache(services)
                     .UseGalaxySerilogger(configs => 
                     {
                         configs.WriteTo.File($"log.txt",
                            rollingInterval: RollingInterval.Day,
                            rollOnFileSizeLimit: true);
                     });
            

            containerBuilder.Populate(services);

            return containerBuilder.InitializeGalaxy();
        }
        #endregion

        #region MiddlewaresConfigurations
        private void ConfigureMiddlewares(IApplicationBuilder app)
        {
            app.UseMiddleware<HttpGlobalExceptionMiddleware>();
            app.UseMiddleware<BlackListingMiddleware>();
            app.UseMiddleware<HealthCheckMiddleware>();
            app.UseMiddleware<CorrelationIdMiddleware>();
            app.UseMiddleware<CircuitBreakerMiddleware>();
            app.UseMiddleware<IdempotencyMiddleware>();
            app.UseMiddleware<LogMiddleware>();
            app.UseMiddleware<HttpRequestValidationMiddleware>();
            app.UseMiddleware<CachingMiddleware>();
            app.UseMiddleware<JwtBearerMiddleware>();            
            app.UseMiddleware<OcelotResponderMiddleware>(); 
        }
        #endregion

        private void ConfigureSwagger(IApplicationBuilder app)
        {
            var swaggerUrls = Configuration.GetSection("SwaggerUrls").GetChildren();

            app.UseSwaggerUI(c =>
            {
                foreach (var swaggerConf in swaggerUrls)
                {
                    c.SwaggerEndpoint(swaggerConf.Value, swaggerConf.Key);     
                }
            });
        }

        private async Task AssertDownStreamErrorsFromOcelotPipeline(IApplicationBuilder app, DownstreamContext ctx )
        {
            if (ctx.Errors.Any())
            {
                var errorMsgs = string.Join(",", ctx.Errors);
                var logServices = app. ApplicationServices.GetService<ILogService>();
                var ex = new Exception(errorMsgs);
                await logServices.LogException(new LogExceptionByRequestCommand
                {
                    CreatedException = ex
                });
            }
        }

        private async Task ConfigureBlackListIPs(IContainer container)
        { 
            var blackListServices = container.Resolve<IBlackListService>();

            string[] blackListIps = Configuration.GetValue<string>("BlackListIPs")
                .Split(',');

            foreach (var ip in blackListIps)
            {
                await blackListServices.AddToBlackList(new AddIPToBlacklistCommand
                {
                    ClientIp = ip
                });
            }
        }
    }
}
