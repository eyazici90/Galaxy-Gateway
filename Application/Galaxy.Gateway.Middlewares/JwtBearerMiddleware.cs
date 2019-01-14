using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading.Tasks;

namespace Galaxy.Gateway.Middlewares
{
   public class JwtBearerMiddleware
    {
        private readonly RequestDelegate _next; 

        public JwtBearerMiddleware(RequestDelegate next )
        {
            _next = next ?? throw new ArgumentNullException(nameof(next)); 
        }

        public async Task Invoke(HttpContext context)
        {
            //TokenValidationParameters validationParameters =
            //        new TokenValidationParameters
            //        {
            //            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("Pgw13213-rqwe21wdasdfqweqwdasdfqkıhjewıowhorıo"))
            //        };

            //SecurityToken validatedToken;
            //JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            //var user = handler.ValidateToken("eyJhbGciOiJSUzI1NiIsImtpZCI6IkU1N0RBRTRBMzU5NDhGODhBQTg2NThFQkExMUZFOUIxMkI5Qzk5NjIiLCJ0eXAiOiJKV1QifQ.eyJ1bmlxdWVfbmFtZSI6IkJvYkBDb250b3NvLmNvbSIsIkFzcE5ldC5JZGVudGl0eS5TZWN1cml0eVN0YW1wIjoiM2M4OWIzZjYtNzE5Ni00NWM2LWE4ZWYtZjlmMzQyN2QxMGYyIiwib2ZmaWNlIjoiMjAiLCJqdGkiOiI0NTZjMzc4Ny00MDQwLTQ2NTMtODYxZi02MWJiM2FkZTdlOTUiLCJ1c2FnZSI6ImFjY2Vzc190b2tlbiIsInNjb3BlIjpbImVtYWlsIiwicHJvZmlsZSIsInJvbGVzIl0sInN1YiI6IjExODBhZjQ4LWU1M2ItNGFhNC1hZmZlLWNmZTZkMjU4YWU2MiIsImF1ZCI6Imh0dHA6Ly9sb2NhbGhvc3Q6NTAwMS8iLCJuYmYiOjE0Nzc1MDkyNTQsImV4cCI6MTQ3NzUxMTA1NCwiaWF0IjoxNDc3NTA5MjU0LCJpc3MiOiJodHRwOi8vbG9jYWxob3N0OjUwMDAvIn0.Lmx6A3jhwoyZ8KAIkjriwHIOAYkgXYOf1zBbPbFeIiU2b-2-nxlwAf_yMFx3b1Ouh0Bp7UaPXsPZ9g2S0JLkKD4ukUa1qW6CzIDJHEfe4qwhQSR7xQn5luxSEfLyT_LENVCvOGfdw0VmsUO6XT4wjhBNEArFKMNiqOzBnSnlvX_1VMx1Tdm4AV5iHM9YzmLDMT65_fBeiekxQNPKcXkv3z5tchcu_nVEr1srAk6HpRDLmkbYc6h4S4zo4aPcLeljFrCLpZP-IEikXkKIGD1oohvp2dpXyS_WFby-dl8YQUHTBFHqRHik2wbqTA_gabIeQy-Kon9aheVxyf8x6h2_FA"
            //    , validationParameters, out validatedToken);
            
            await _next(context);
        }
    }
}
