using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.Gateway.Shared.Commands
{
    public class LogExceptionByRequestCommand : IRequest<bool>
    {
        public Exception CreatedException { get; set; }
    }
}
