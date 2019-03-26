using Galaxy.Serialization;
using Microsoft.AspNetCore.Http;
using Galaxy.Gateway.Application.Contracts;
using Galaxy.Gateway.Shared.Queries;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Galaxy.Gateway.Shared.Exceptions;

namespace Galaxy.Gateway.Middlewares
{ 
    public class BlackListingMiddleware
    {
        private readonly IBlackListService _blackListService;
        private readonly ISerializer _serializer;
        private readonly RequestDelegate _next;

        public BlackListingMiddleware(RequestDelegate next
            , IBlackListService blackListService
            , ISerializer serializer)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _blackListService = blackListService ?? throw new ArgumentNullException(nameof(blackListService));
            _serializer = serializer ?? throw new ArgumentNullException(nameof(serializer));
        }

        public async Task Invoke(HttpContext context)
        {
            var host = context.Request.HttpContext?.Connection?.RemoteIpAddress?.ToString();

            var isIpInblackList = await _blackListService.IsIpInBlackList(new BlackListByIpQuery(host));

            if (isIpInblackList)
                throw new BlackListErrorException(host);

            await _next(context);
        }
    }
}
