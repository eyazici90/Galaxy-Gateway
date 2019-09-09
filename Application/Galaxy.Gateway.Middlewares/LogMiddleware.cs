using Galaxy.Log;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal; 
using Galaxy.Gateway.Services.Contracts;
using Galaxy.Gateway.Shared.Commands;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Galaxy.Gateway.Shared;
using System.Linq;

namespace Galaxy.Gateway.Middlewares
{
    public class LogMiddleware
    {
        private readonly ILogService _logService;
        private readonly RequestDelegate _next;
        public LogMiddleware(RequestDelegate next
            , ILogService logService)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _logService = logService ?? throw new ArgumentNullException(nameof(logService));
        }

        public async Task Invoke(HttpContext context)
        {
            await this._logService.LogRequest(new LogRequestCommand(string.Empty, await FormatRequest(context.Request), context.Request.Path,
                 GetCorrelationIdFromCurrentContext(context)));

            var originalBodyStream = context.Response.Body;
            var responseBody = new MemoryStream();
            try
            {
                context.Response.Body = responseBody;

                await _next(context);

                await this._logService.LogResponse(new LogResponseCommand(string.Empty, await FormatResponse(context.Response), context.Request.Path,
                     GetCorrelationIdFromCurrentContext(context)));

                await responseBody.CopyToAsync(originalBodyStream);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                responseBody.Dispose();
                context.Response.Body = originalBodyStream;
            }
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
            return $"{request.Scheme} {request.Host}{request.Path} {request.QueryString} {request.Method} {bodyAsText}";
        }

        private async Task<string> FormatResponse(HttpResponse response)
        {
            response.Body.Seek(0, SeekOrigin.Begin);
            var text = await new StreamReader(response.Body).ReadToEndAsync();
            response.Body.Seek(0, SeekOrigin.Begin);
            return $"Response: {text}";
        }

        private string GetCorrelationIdFromCurrentContext(HttpContext context) =>
            context.Response?.Headers[SettingConsts.PGW_CORRELATION_ID].SingleOrDefault();

    }
}
