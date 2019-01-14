using MediatR;
using Galaxy.Gateway.Application.Contracts;
using Galaxy.Gateway.Shared.Commands;
using Galaxy.Gateway.Shared.Queries;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Galaxy.Gateway.Application.Services
{
    public class CacheService : ICacheService
    {
        private readonly IMediator _mediatr;
        public CacheService(IMediator mediatr)
        {
            _mediatr = mediatr ?? throw new ArgumentNullException(nameof(mediatr));
        }

        public async Task<bool> AddToCache(AddValueToCacheCommand command) =>
            await _mediatr.Send(command);


        public async Task<object> GetCacheValueByKey(GetCacheValueByKeyQuery command) =>
            await _mediatr.Send(command);

    }
}
