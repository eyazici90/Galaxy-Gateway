using Galaxy.Serialization;
using Microsoft.AspNetCore.Http;
using Galaxy.Gateway.Application.Contracts;
using Galaxy.Gateway.Shared;
using Galaxy.Gateway.Shared.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace Galaxy.Gateway.Middlewares
{
    public class IdempotencyMiddleware
    {
        private readonly ICacheService _cacheService;
        private readonly ISerializer _serializer;
        private readonly RequestDelegate _next;

        public IdempotencyMiddleware(RequestDelegate next
            , ICacheService cacheService
            , ISerializer serializer)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _cacheService = cacheService ?? throw new ArgumentNullException(nameof(cacheService));
            _serializer = serializer ?? throw new ArgumentNullException(nameof(serializer));
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Headers.Keys.Any(k => k.Trim() == SettingConsts.PGW_IDEMPOTANCY_HEADER))
            {
                var cacheKey = context.Request.Headers[SettingConsts.PGW_IDEMPOTANCY_HEADER].SingleOrDefault();

                var cacheValue = await this._cacheService.GetCacheValueByKey(new GetCacheValueByKeyQuery(cacheKey));
                if (cacheValue == null)
                {
                    await _next(context);
                }
                else
                {
                    context.Response.Clear();

                    context.Response.OnStarting(async () => {

                        context.Response.StatusCode = (int)HttpStatusCode.OK;
                        context.Response.ContentType = "application/json";

                        await context.Response.WriteAsync(this._serializer.Serialize(cacheValue));
                    });
                }
            }
            await _next(context);
        }
    }
}
