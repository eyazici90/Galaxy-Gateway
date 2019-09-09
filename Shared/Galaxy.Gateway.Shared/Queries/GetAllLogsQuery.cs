using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.Gateway.Shared.Queries
{
    public class GetAllLogsQuery : IRequest<IList<object>>
    {
        public readonly DateTime? CreationDate;
        public GetAllLogsQuery()
        {
            CreationDate = DateTime.Now;
        }
    }
}
