using FluentValidation;
using Galaxy.Gateway.Shared.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.Gateway.Application.Validations
{ 
    public class AddIPToBlacklistCommandValidation : AbstractValidator<AddIPToBlacklistCommand>
    {
        public AddIPToBlacklistCommandValidation()
        {
            RuleFor(t => t.ClientIp)
                .NotEmpty().NotNull();
        }
    }
}
