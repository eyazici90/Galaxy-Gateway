using Galaxy.Application;
using Galaxy.Gateway.Shared.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Galaxy.Gateway.Services.Contracts
{
    public interface IRequestValidatorService : IApplicationService
    {
        Task ValidateRequest(ValidateRequestCommand command);
    }
}
