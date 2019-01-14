using Galaxy.Application;
using Galaxy.Gateway.Shared.Commands;
using Galaxy.Gateway.Shared.Queries;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Galaxy.Gateway.Services.Contracts
{
    public interface ILogService : IApplicationService
    {
        Task<IList<object>> GetAllLogs(GetAllLogsQuery query);

        Task<object> GetLogById(GetLogByIdQuery query);

        Task<bool> LogRequest(LogRequestCommand command);

        Task<bool> LogResponse(LogResponseCommand command);

        Task<bool> LogException(LogExceptionByRequestCommand command);
    }
}
