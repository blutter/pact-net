using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;

namespace Provider.Api.Web.DotnetCore.Tests
{
    public class AuthorizationTokenReplacementMiddleware
    {
        private const string AuthorizationKey = "Authorization";
        private readonly RequestDelegate _next;
        private readonly TokenGenerator _tokenGenerator;

        public AuthorizationTokenReplacementMiddleware(RequestDelegate next, IDataProtector dataProtector)
        {
            _next = next;
            _tokenGenerator = new TokenGenerator(dataProtector);
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Headers != null &&
                context.Request.Headers.ContainsKey(AuthorizationKey) &&
                context.Request.Headers[AuthorizationKey] == "Bearer SomeValidAuthToken")
            {
                context.Request.Headers[AuthorizationKey] = $"Bearer {_tokenGenerator.Generate()}";
            }

            await _next.Invoke(context).ConfigureAwait(false);
        }
    }
}