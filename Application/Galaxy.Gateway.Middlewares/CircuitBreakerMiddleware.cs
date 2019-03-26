using Galaxy.Serialization;
using Microsoft.AspNetCore.Http;
using Galaxy.Gateway.Application.Contracts;
using Galaxy.Gateway.Shared.Commands;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Galaxy.Gateway.Shared.Exceptions;

namespace Galaxy.Gateway.Middlewares
{
    public class CircuitBreakerMiddleware
    {
        private readonly IResilenceService _resilenceService;
        private readonly ISerializer _serializer;
        private readonly RequestDelegate _next;

        public CircuitBreakerMiddleware(RequestDelegate next
            , IResilenceService resilenceService
            , ISerializer serializer)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _resilenceService = resilenceService ?? throw new ArgumentNullException(nameof(resilenceService));
            _serializer = serializer ?? throw new ArgumentNullException(nameof(serializer));
        }

        public async Task Invoke(HttpContext context)
        {
            if (this._resilenceService.CheckIfBreakerStateOpened)
            {
                throw new CircuitBreakerOpenedException(_resilenceService.LastHandledExceptionByCircuitBreaker.Message);
            }

            var command = new ExecuteThroughCircuitBreakerCommand
            {
                Execution = async () => await _next(context)
            };

            await this._resilenceService
                .ExecuteWithCircuitBreakerAsync(command);
        }
    }
}
