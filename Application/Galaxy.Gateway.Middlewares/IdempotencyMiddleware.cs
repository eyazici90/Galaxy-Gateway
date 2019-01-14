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
            if (context.Request.Headers.Keys.Any(k => k.Trim() == Settings.PGW_IDEMPOTANCY_HEADER))
            {
                var cacheKey = context.Request.Headers[Settings.PGW_IDEMPOTANCY_HEADER].SingleOrDefault();
                
                var cacheValue = await this._cacheService.GetCacheValueByKey(new GetCacheValueByKeyQuery(cacheKey));
                if (cacheValue == null)
                {
                    await _next(context);
                }
                else
                {
                    await context.Response.WriteAsync(this._serializer.Serialize(cacheValue));
                    return;
                }
            }
            await _next(context);
        }
    }
}
