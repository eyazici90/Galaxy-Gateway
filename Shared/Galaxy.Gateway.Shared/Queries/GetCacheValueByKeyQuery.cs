using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.Gateway.Shared.Queries
{
    public class GetCacheValueByKeyQuery : IRequest<object>
    {
        public readonly string CacheKey;
        public GetCacheValueByKeyQuery(string cacheKey)
        {
            this.CacheKey = cacheKey;
        }
    }
   
}
