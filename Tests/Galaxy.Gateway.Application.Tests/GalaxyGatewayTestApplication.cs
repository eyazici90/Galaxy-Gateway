using Autofac;
using Autofac.Extras.DynamicProxy;
using Galaxy.Application;
using Galaxy.Bootstrapping;
using Galaxy.Cache.Bootstrapper;
using Galaxy.FluentValidation;
using Galaxy.FluentValidation.Bootstrapper;
using Galaxy.Serilog.Bootstrapper;
using Galaxy.Utf8Json.Bootstrapper;
using Microsoft.Extensions.DependencyInjection;
using Galaxy.Gateway.Application.Services;
using Galaxy.Gateway.Application.Validations;
using Galaxy.TestBase;
using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.Gateway.Application.Tests
{
    public class GalaxyGatewayTestApplication : ApplicationTestBase
    { 
        public GalaxyGatewayTestApplication()
        {
            var services = new ServiceCollection();

            services
                .AddOptions()
                .AddLogging();

            Build(builder =>
            {
                 builder.UseGalaxyCore(b =>
                  { 
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
                  });
            });
        }
    }
}
