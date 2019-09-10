using Galaxy.Events;
using Galaxy.Serialization;
using Microsoft.AspNetCore.Http;
using Galaxy.Gateway.Application.Contracts;
using Galaxy.Gateway.Shared;
using Galaxy.Gateway.Shared.Commands;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Galaxy.Gateway.Middlewares
{
    public class QueueMiddleware
    {
        private readonly IQueueService _queueService;
        private readonly ISerializer _serializer;
        private readonly RequestDelegate _next;

        public QueueMiddleware(RequestDelegate next
            , IQueueService queueService
            , ISerializer serializer)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _queueService = queueService ?? throw new ArgumentNullException(nameof(queueService));
            _serializer = serializer ?? throw new ArgumentNullException(nameof(serializer));
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Headers.Keys.Any(k => k.Trim() == SettingConsts.GATEWAY_QUEUE_HEADER))
            {
                await _queueService.PublishCommandThroughEventBus(new PublishToQueueThroughEventBusCommand
                {

                });
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(this._serializer.Serialize(new
                {
                    Result = $"Request Queued by Gateway."
                }));
            }
            await _next(context);
        }
    }
}
