using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Galaxy.Gateway.Shared.Commands
{
    public class ExecuteThroughCircuitBreakerCommand : IRequest<bool>
    {
        public Func<Task> Execution{ get; set; }
        public object CircuitBreaker { get; set; }
    }
}
