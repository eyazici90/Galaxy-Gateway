using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.Gateway.Shared.Commands
{
    public class AddIPToBlacklistCommand : IRequest<bool>
    {
        public string ClientIp { get; set; }

        public int? BlacklistedDurationSeconds { get; set; }
    }
}
