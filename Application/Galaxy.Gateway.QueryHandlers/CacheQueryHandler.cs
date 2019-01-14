using Galaxy.Cache;
using MediatR;
using Galaxy.Gateway.Shared.Queries;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Galaxy.Gateway.QueryHandlers
{
    public class CacheQueryHandler : IRequestHandler<GetCacheValueByKeyQuery, object>
    {
        private readonly ICache _cache;
        public CacheQueryHandler(ICache cache)
        {
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
        }
        public async Task<object> Handle(GetCacheValueByKeyQuery request, CancellationToken cancellationToken)
        {
            return await this._cache.GetAsync<object>(request.CacheKey);
        }
    }
}
