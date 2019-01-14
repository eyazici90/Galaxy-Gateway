using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Galaxy.Gateway.API.Extensions
{
    public static class HttpGlobalExtensions
    { 
        public static void EnsureSuccesfullJsonResponse(this HttpContext context, string responseJsonBody)
        {
            context.Response.Clear();
            context.Response.OnStarting(async () => 
            {
                context.Response.StatusCode = (int)HttpStatusCode.OK;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(responseJsonBody);
            });
        }
    }
}
