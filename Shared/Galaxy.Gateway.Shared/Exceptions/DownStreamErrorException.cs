using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.Gateway.Shared.Exceptions
{
    public class DownStreamErrorException : Exception
    {
        public DownStreamErrorException()
        { }

        public DownStreamErrorException(string message)
            : base($"DownStream Path returned {message} Server Error")
        { }


        public DownStreamErrorException(string message, Exception innerException)
            : base($"DownStream Path returned {message} Server Error", innerException)
        { }
    }
}
