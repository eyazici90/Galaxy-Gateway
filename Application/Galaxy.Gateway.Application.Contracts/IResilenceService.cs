using Galaxy.Application;
using Galaxy.Gateway.Shared.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Galaxy.Gateway.Application.Contracts
{
    public interface IResilenceService : IApplicationService
    {
        Task ExecuteWithCircuitBreakerAsync(ExecuteThroughCircuitBreakerCommand command);

        bool CheckIfBreakerStateOpened { get; }

        Exception LastHandledExceptionByCircuitBreaker { get; }
    }
}
