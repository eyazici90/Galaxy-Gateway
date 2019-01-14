using MediatR;
using Galaxy.Gateway.Application.Contracts;
using Galaxy.Gateway.Shared;
using Galaxy.Gateway.Shared.Commands;
using Polly;
using Polly.CircuitBreaker;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Galaxy.Gateway.Application.Services
{
    public class ResilenceService : IResilenceService
    {
        private static CircuitBreakerPolicy _circuitBreaker =  Policy
             .Handle<Exception>() 
             .CircuitBreakerAsync(
                 exceptionsAllowedBeforeBreaking: Settings.EXCEPTION_ALLOWED_BEFORE_BREAKING,
                 durationOfBreak: TimeSpan.FromSeconds(Settings.CIRCUIT_BREAKER_OPEN_STATE_DURATION_SECONDS)
             );

        private readonly IMediator _mediatr;
        public ResilenceService(IMediator mediatr)
        {
            _mediatr = mediatr ?? throw new ArgumentNullException(nameof(mediatr));
        }

        public Exception LastHandledExceptionByCircuitBreaker =>
           _circuitBreaker.LastException;

        public bool CheckIfBreakerStateOpened =>
            _circuitBreaker.CircuitState == CircuitState.Open;

        public async Task ExecuteWithCircuitBreakerAsync(ExecuteThroughCircuitBreakerCommand command)
        {
            command.CircuitBreaker = _circuitBreaker;
            await _mediatr.Send(command);
        } 
       
    }
}
