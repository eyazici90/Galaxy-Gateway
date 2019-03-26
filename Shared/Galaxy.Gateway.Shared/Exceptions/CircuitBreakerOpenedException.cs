using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.Gateway.Shared.Exceptions
{
    public class CircuitBreakerOpenedException : Exception
    {
        public CircuitBreakerOpenedException()
        { }

        public CircuitBreakerOpenedException(string message)
            : base($"CircuitBreaker Is Opened !!! LastHandled Exception by CircuitBreaker is: {message}")
        { }

        public CircuitBreakerOpenedException(string message, Exception innerException)
            : base($"CircuitBreaker Is Opened !!! LastHandled Exception by CircuitBreaker is: {message}", innerException)
        { }
    }
}
