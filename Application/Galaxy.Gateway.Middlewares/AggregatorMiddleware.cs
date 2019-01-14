using Microsoft.AspNetCore.Http;
using System; 
using System.Threading.Tasks;

namespace Galaxy.Gateway.Middlewares
{ 
    public class AggregatorMiddleware
    {
        private readonly RequestDelegate _next;

        public AggregatorMiddleware(RequestDelegate next)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
        }

        public async Task Invoke(HttpContext context)
        {
            await _next(context);
        }
    }
}
