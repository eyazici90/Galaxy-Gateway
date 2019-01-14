using FluentValidation;
using Galaxy.Gateway.Shared.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.Gateway.Application.Validations
{
     
    public class LogExceptionByRequestCommandValidation : AbstractValidator<LogExceptionByRequestCommand>
    {
        public LogExceptionByRequestCommandValidation()
        {
            RuleFor(t => t.CreatedException).NotNull();
        }
    }

}
