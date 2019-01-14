using MediatR;
using Galaxy.Gateway.Services.Contracts;
using Galaxy.Gateway.Shared.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Galaxy.Gateway.Application.Services
{
    public class RequestValidatorService : IRequestValidatorService
    {
        private readonly IMediator _mediatr;
        public RequestValidatorService(IMediator mediatr) =>
            _mediatr = mediatr ?? throw new ArgumentNullException(nameof(mediatr));
        
        public async Task ValidateRequest(ValidateRequestCommand command) =>
            await _mediatr.Send(command);
        
    }
}
