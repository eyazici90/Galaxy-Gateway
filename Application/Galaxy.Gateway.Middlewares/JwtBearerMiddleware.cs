using Galaxy.Gateway.Shared;
using Galaxy.Gateway.Shared.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxy.Gateway.Middlewares
{
    public class JwtBearerMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtBearerMiddleware(RequestDelegate next)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
        }

        public async Task Invoke(HttpContext context)
        {
            if (ShouldValidateJwt(context))
            {
                HandleJwtRequest(context);
            }
            await _next(context);
        }

        private void HandleJwtRequest(HttpContext context)
        {
            // Todo: Enviroumentea çekilecek!!! 
            TokenValidationParameters validationParameters =
                    new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SettingConsts.GATEWAY_SECRET_KEY))
                    };

            SecurityToken validatedToken;

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();

            var jwtToken = GetBearerTokenFromRequestHeader(context);

            handler.ValidateToken(jwtToken
                , validationParameters, out validatedToken);

            if (validatedToken == null)
                throw new BearerTokenValidationException();
        }

        private string GetBearerTokenFromRequestHeader(HttpContext context) =>
            context.Request.Headers.Where(h => h.Key == "Authorization").SingleOrDefault()
                .Value.ToString()
                .Replace("Bearer", string.Empty)
                .Replace("bearer", string.Empty)
                .Trim();

        private bool ShouldValidateJwt(HttpContext context) =>
             SettingConsts.IS_JWT_AUTH_ENABLED && context.Request.Headers.Keys.Any(k => k == "Authorization");
    }
}
