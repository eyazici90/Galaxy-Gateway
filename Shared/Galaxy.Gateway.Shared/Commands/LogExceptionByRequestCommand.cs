using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.Gateway.Shared.Commands
{
    public class LogExceptionByRequestCommand : IRequest<bool>
    {
        public readonly Exception CreatedException;
        public LogExceptionByRequestCommand(Exception createdException)
        {
            CreatedException = createdException;
        }
    }
}
