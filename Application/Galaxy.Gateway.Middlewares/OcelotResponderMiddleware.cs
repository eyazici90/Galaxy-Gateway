using Galaxy.Serialization;
using Microsoft.AspNetCore.Http;
using Galaxy.Gateway.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Galaxy.Gateway.Middlewares
{
    public class OcelotResponderMiddleware
    {
        private readonly ILogService _logService;
        private readonly RequestDelegate _next;

        public OcelotResponderMiddleware(RequestDelegate next
            , ILogService logService)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _logService = logService ?? throw new ArgumentNullException(nameof(logService));
        }

        public async Task Invoke(HttpContext context)
        {
            await _next(context);

            var responseStatusCode = context.Response.StatusCode.ToString();

            if (responseStatusCode.StartsWith("4") 
                || responseStatusCode.StartsWith("5"))
            {
               throw new Exception($"DownStream Path returned {responseStatusCode} Server Error");     
            } 
        }
    }
}
