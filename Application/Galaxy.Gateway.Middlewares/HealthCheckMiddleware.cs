using Galaxy.Serialization;
using Microsoft.AspNetCore.Http;
using Galaxy.Gateway.Shared;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Galaxy.Gateway.Middlewares
{
    
    public class HealthCheckMiddleware
    { 
        private readonly ISerializer _serializer;
        private readonly RequestDelegate _next;

        public HealthCheckMiddleware(RequestDelegate next
            , ISerializer serializer)
        
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
           _serializer = serializer ?? throw new ArgumentNullException(nameof(serializer));
        }

        public async Task Invoke(HttpContext context)
        {
            if (!context.Request.Path.Equals(Settings.PGW_HEALTHCHECK_URL, StringComparison.Ordinal))
            {
                await _next(context);
                return;
            }
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(this._serializer.Serialize(new { Status = "Healty" }));  
        }
    }
}
