using FluentValidation;
using Galaxy.Gateway.Shared.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.Gateway.Application.Validations
{ 
    public class ValidateRequestCommandValidation : AbstractValidator<ValidateRequestCommand>
    {
        public ValidateRequestCommandValidation()
        {
            RuleFor(t => t.ContentType)
                .NotEmpty();

            RuleFor(t => t.Body)
                .NotEmpty();
        }
    }
}
