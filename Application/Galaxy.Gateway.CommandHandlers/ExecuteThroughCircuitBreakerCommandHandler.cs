using MediatR;
using Galaxy.Gateway.Shared.Commands;
using Polly.CircuitBreaker;
using System.Threading;
using System.Threading.Tasks;

namespace Galaxy.Gateway.CommandHandlers
{
    public class ExecuteThroughCircuitBreakerCommandHandler : IRequestHandler<ExecuteThroughCircuitBreakerCommand, bool>
    {
        public async Task<bool> Handle(ExecuteThroughCircuitBreakerCommand request, CancellationToken cancellationToken)
        {
            var circuitBreaker = request.CircuitBreaker as CircuitBreakerPolicy;
            await circuitBreaker.ExecuteAsync(async () =>
            {
                await request.Execution();
            });
            return true;
        }
    }
}
