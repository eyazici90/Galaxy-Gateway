using Galaxy.Application;
using Galaxy.Gateway.Shared.Commands;
using Galaxy.Gateway.Shared.Queries;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Galaxy.Gateway.Application.Contracts
{
    public interface ICacheService : IApplicationService
    {
        Task<bool> AddToCache(AddValueToCacheCommand command);
        Task<object> GetCacheValueByKey(GetCacheValueByKeyQuery command);
    }
}
