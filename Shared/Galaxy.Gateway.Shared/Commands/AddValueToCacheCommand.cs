using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.Gateway.Shared.Commands
{
    public class AddValueToCacheCommand : IRequest<bool>
    {
        public readonly string CacheKey;
        public readonly string CacheValue;
        public readonly int? SlidingExpireTime;
        public readonly int? AbsoluteExpireTime;

        public AddValueToCacheCommand(string cacheKey, string cacheValue,
            int? slidingExpireTime, int? absoluteExpireTime)
        {
            CacheKey = cacheKey;
            CacheValue = cacheValue;
            SlidingExpireTime = slidingExpireTime;
            AbsoluteExpireTime = absoluteExpireTime;
        }

    }
}
