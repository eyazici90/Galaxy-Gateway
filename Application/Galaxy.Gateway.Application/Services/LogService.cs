using MediatR;
using Galaxy.Gateway.Services.Contracts;
using Galaxy.Gateway.Shared.Commands;
using Galaxy.Gateway.Shared.Queries;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Galaxy.Gateway.Application.Services
{
    public class LogService : ILogService
    {
        private readonly IMediator _mediatr;
        public LogService(IMediator mediatr)
        {
            _mediatr = mediatr ?? throw new ArgumentNullException(nameof(mediatr)) ;
        }

        public async Task<bool> LogRequest(LogRequestCommand command) =>
            await _mediatr.Send(command);

        public async Task<bool> LogResponse(LogResponseCommand command) =>
           await _mediatr.Send(command);

        public async Task<bool> LogException(LogExceptionByRequestCommand command) =>
           await _mediatr.Send(command);

        public async Task<IList<object>> GetAllLogs(GetAllLogsQuery query) =>
            await _mediatr.Send(query);

        public async Task<object> GetLogById(GetLogByIdQuery query) =>
            await _mediatr.Send(query);
    }
}
