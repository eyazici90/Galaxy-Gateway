using Galaxy.Cache;
using Galaxy.Serialization;
using MediatR;
using Galaxy.Gateway.Shared.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Galaxy.Gateway.CommandHandlers
{
    public class BlacklistCommandHandler : IRequestHandler<AddIPToBlacklistCommand, bool>
    {
        private readonly ICache _cache;
        private readonly ISerializer _serializer;
        public BlacklistCommandHandler(ICache cache
            , ISerializer serializer)
        {
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
            _serializer = serializer ?? throw new ArgumentNullException(nameof(serializer));
        }

        public async Task<bool> Handle(AddIPToBlacklistCommand request, CancellationToken cancellationToken)
        {
            if (request.BlacklistedDurationSeconds.HasValue)
            {
                await _cache.SetAsync(request.ClientIp, "BlackListed!!!", null
                              , TimeSpan.FromSeconds(request.BlacklistedDurationSeconds.Value));
            }
            else
            {
                await _cache.SetAsync(request.ClientIp, "BlackListed!!!");
            }
            return true;
        }
    }
}
