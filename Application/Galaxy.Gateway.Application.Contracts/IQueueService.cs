using Galaxy.Application;
using Galaxy.Gateway.Shared.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Galaxy.Gateway.Application.Contracts
{
    public interface IQueueService : IApplicationService
    {
        Task<bool> PublishCommandThroughEventBus(PublishToQueueThroughEventBusCommand command);
    }
}
