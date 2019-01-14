using Galaxy.Serialization;
using Microsoft.AspNetCore.Http;
using Galaxy.Gateway.Services.Contracts;
using Galaxy.Gateway.Shared.Commands;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Galaxy.Gateway.Middlewares
{ 
    public class HttpGlobalExceptionMiddleware
    {
        private readonly ILogService _logService;
        private readonly ISerializer _serializer;
        private readonly RequestDelegate _next;

        public HttpGlobalExceptionMiddleware(RequestDelegate next
            , ISerializer serializer
            , ILogService logService)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _serializer = serializer ?? throw new ArgumentNullException(nameof(serializer));
            _logService = logService ?? throw new ArgumentNullException(nameof(logService));
        }

        public async Task Invoke(HttpContext context)
        {
            try
            { 
                await _next(context);
            }

            catch (Exception ex)
            {
                await _logService.LogException(new LogExceptionByRequestCommand
                {
                    CreatedException = ex
                });

                context.Response.Clear();

                context.Response.OnStarting(async () => {

                    context.Response.StatusCode = (int)HttpStatusCode.OK;
                    context.Response.ContentType = "application/json";

                    var errorResult = this._serializer.Serialize(
                            new { ErrorMsj = ex.Message, Description = ex.InnerException?.Message }
                            );

                    await context.Response.WriteAsync(errorResult);
                });
                
              
            }

        }
       

    }
}
