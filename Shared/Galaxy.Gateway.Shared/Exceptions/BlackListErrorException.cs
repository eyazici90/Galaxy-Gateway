using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.Gateway.Shared.Exceptions
{
    public class BlackListErrorException : Exception
    {
        public BlackListErrorException()
        { }

        public BlackListErrorException(string message)
            : base($"IP : {message} is in BlackList")
        { }

        public BlackListErrorException(string message, Exception innerException)
            : base($"IP : {message} is in BlackList", innerException)
        { }
    }
}
