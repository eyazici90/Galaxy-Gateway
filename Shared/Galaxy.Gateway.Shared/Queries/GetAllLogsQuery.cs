using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.Gateway.Shared.Queries
{
    public class GetAllLogsQuery : IRequest<IList<object>>
    {
        public DateTime? CreationDate { get; private set; }
        public GetAllLogsQuery()
        {
            CreationDate = DateTime.Now;
        }
    }
}
