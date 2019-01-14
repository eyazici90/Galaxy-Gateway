using FluentValidation;
using Galaxy.Gateway.Shared.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.Gateway.Application.Validations
{ 
    public class AddValueToCacheCommandValidation : AbstractValidator<AddValueToCacheCommand>
    {
        public AddValueToCacheCommandValidation()
        {
            RuleFor(t => t.CacheKey)
                .NotEmpty();

            RuleFor(t => t.CacheValue)
                .NotEmpty();
        }
    }
}
