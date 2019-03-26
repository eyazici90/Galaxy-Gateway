using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.Extensions.Primitives;
using Galaxy.Gateway.Application.Contracts;
using Galaxy.Gateway.Shared;
using Galaxy.Gateway.Shared.Commands;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxy.Gateway.Middlewares
{
    public class CachingMiddleware
    {
        private readonly ICacheService _cacheService;
        private readonly RequestDelegate _next;

        public CachingMiddleware(RequestDelegate next
            , ICacheService cacheService)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _cacheService = cacheService ?? throw new ArgumentNullException(nameof(cacheService));
        }

        public async Task Invoke(HttpContext context)
        {
            if (CheckIfCachedRequest(context))
            {
                var cacheKey = context.Request.Headers[SettingConsts.PGW_CACHE_HEADER].SingleOrDefault();
                var cacheValue = await this.FormatRequest(context.Request);

                await this._cacheService.AddToCache(new AddValueToCacheCommand
                {
                    CacheKey = cacheKey,
                    CacheValue = cacheValue
                });
            }

            await _next(context);
        }

        private bool CheckIfCachedRequest(HttpContext context) =>
            context.Request.Headers.Keys.Any(k => k.Trim() == SettingConsts.PGW_CACHE_HEADER);

        private async Task<string> FormatRequest(HttpRequest request)
        {
            request.EnableRewind();
            var body = request.Body;
            var buffer = new byte[Convert.ToInt32(request.ContentLength)];
            await request.Body.ReadAsync(buffer, 0, buffer.Length);
            var bodyAsText = Encoding.UTF8.GetString(buffer);
            body.Seek(0, SeekOrigin.Begin);
            request.Body = body;
            return $"{bodyAsText}";
        }

    }
}
