using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.Gateway.Shared.Commands
{
    public class AddIPToBlacklistCommand : IRequest<bool>
    {
        public readonly string ClientIp;

        public readonly int? BlacklistedDurationSeconds;
        public AddIPToBlacklistCommand(string clientIp, int? blacklistedDurationSeconds)
        {
            ClientIp = clientIp;
            BlacklistedDurationSeconds = blacklistedDurationSeconds;
        }
    }
}
