using MediatR;
using Galaxy.Gateway.Shared.Queries;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Galaxy.Gateway.QueryHandlers
{
    public class LogQueryHandler : IRequestHandler<GetAllLogsQuery, IList<object>>
        , IRequestHandler<GetLogByIdQuery, object>
    {
        public async Task<IList<object>> Handle(GetAllLogsQuery request, CancellationToken cancellationToken)
        {
            return await Task.FromResult(new List<object>
            {
                new { Body = "Test"}
            });
        }

        public async Task<object> Handle(GetLogByIdQuery request, CancellationToken cancellationToken)
        {
            return await Task.FromResult(new { Body = $"{request.Id} log query result" });
        }
    }
}
