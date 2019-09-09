using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.Gateway.Shared.Commands
{
    public class LogResponseCommand : IRequest<bool>
    {
        public readonly string Headers;
        public readonly string Body;
        public readonly string Url;
        public readonly string CorrelationId;
        public LogResponseCommand(string headers, string body,
            string url, string correlationId)
        {
            Headers = headers;
            Body = body;
            Url = url;
            CorrelationId = correlationId;
        }
    }
}
