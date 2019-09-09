using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.Gateway.Shared.Queries
{
    public class GetLogByIdQuery : IRequest<object>
    {
        public readonly DateTime? CreationDate;
        public readonly string Id;
        public GetLogByIdQuery(string id)
        {
            Id = id;
            CreationDate = DateTime.Now;
        }
    }
}
