using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Galaxy.Gateway.Shared.Commands
{
    public class ExecuteThroughCircuitBreakerCommand : IRequest<bool>
    {
        public readonly Func<Task> Execution;
        public readonly object CircuitBreaker;

        public ExecuteThroughCircuitBreakerCommand(Func<Task> execution, object circuitBreaker = null)
        {
            Execution = execution;
            CircuitBreaker = circuitBreaker;
        }
    }
}
