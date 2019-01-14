using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Galaxy.Gateway.Services.Contracts;
using Galaxy.Gateway.Shared.Commands;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Galaxy.Gateway.Middlewares
{
    public class HttpRequestValidationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IRequestValidatorService _reqValidatorService;

        public HttpRequestValidationMiddleware(RequestDelegate next
            , IRequestValidatorService reqValidatorService)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _reqValidatorService = reqValidatorService ?? throw new ArgumentNullException(nameof(reqValidatorService));
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Method != HttpMethods.Get)
            {
                await this._reqValidatorService.ValidateRequest(new ValidateRequestCommand
                {
                    ContentType = context.Request.ContentType,
                    Url = context.Request.Path,
                    Body = await this.FormatRequest(context.Request)
                });
            }
            await _next(context);
        }

        private async Task<string> FormatRequest(HttpRequest request)
        {
            request.EnableRewind();
            var body = request.Body;
            var buffer = new byte[Convert.ToInt32(request.ContentLength)];
            await request.Body.ReadAsync(buffer, 0, buffer.Length);
            var bodyAsText = Encoding.UTF8.GetString(buffer);
            body.Seek(0, SeekOrigin.Begin);
            request.Body = body;

            return $"{bodyAsText}";
        }

    }
}
