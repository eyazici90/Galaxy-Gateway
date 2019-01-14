using FluentValidation;
using Galaxy.Gateway.Shared.Queries;
using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.Gateway.Application.Validations
{ 
    public class BlackListByIpQueryValidation : AbstractValidator<BlackListByIpQuery>
    {
        public BlackListByIpQueryValidation()
        {
            RuleFor(t => t.Ip)
                .NotEmpty().NotNull();
        }
    } 
}
