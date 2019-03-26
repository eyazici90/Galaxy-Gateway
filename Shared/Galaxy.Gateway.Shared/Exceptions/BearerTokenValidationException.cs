using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.Gateway.Shared.Exceptions
{
    public class BearerTokenValidationException : Exception
    {
        public BearerTokenValidationException()
            : base($"Bearer token is not validated !!!")
        { }

        public BearerTokenValidationException(Exception innerException)
            : base($"Bearer token is not validated !!!", innerException)
        { }
    }
}
