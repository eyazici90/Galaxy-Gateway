using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.Gateway.Shared.Exceptions
{
    public class InvalidJsonException : Exception
    {
        public InvalidJsonException()
        { }

        public InvalidJsonException(string message)
            : base($"Invalid Json string: {message}")
        { }

        public InvalidJsonException(string message, Exception innerException)
            : base($"Invalid Json string: {message}, innerException")
        { }
    }
}
