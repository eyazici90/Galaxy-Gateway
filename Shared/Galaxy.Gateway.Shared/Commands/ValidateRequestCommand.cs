using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.Gateway.Shared.Commands
{
    public class ValidateRequestCommand : IRequest<bool>
    {
        public readonly string ContentType;
        public readonly string Headers;
        public readonly string Body;
        public readonly string Url;
        public ValidateRequestCommand(string contentType, string headers,
            string body, string url)
        {
            ContentType = contentType;
            Headers = headers;
            Body = body;
            Url = url;
        }
    }
}
