using MediatR;
using Galaxy.Gateway.Application.Contracts;
using Galaxy.Gateway.Shared.Commands;
using Galaxy.Gateway.Shared.Queries;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Galaxy.Gateway.Application.Services
{
    public class BlackListService : IBlackListService
    {
        private readonly IMediator _mediatr;
        
        public BlackListService(IMediator mediatr)=>
            _mediatr = mediatr ?? throw new ArgumentNullException(nameof(mediatr));
        

        public async Task<bool> AddToBlackList(AddIPToBlacklistCommand command) =>
            await _mediatr.Send(command);

        public async Task<bool> IsIpInBlackList(BlackListByIpQuery query) =>
            await _mediatr.Send(query);
    }
}
