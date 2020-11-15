using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Demo
{
    public class DemoService : AuthenticationService, IAuthenticationService
    {
        public DemoService(IAuthenticationSchemeProvider schemes, IAuthenticationHandlerProvider handlers, IClaimsTransformation transform, IOptions<AuthenticationOptions> options)
           : base(schemes, handlers, transform, options)
        {
  
        }

        async Task<AuthenticateResult> IAuthenticationService.AuthenticateAsync(HttpContext context, string scheme)
        {
            var result = await base.AuthenticateAsync(context, scheme);
            return result;
        }

        Task IAuthenticationService.ChallengeAsync(HttpContext context, string scheme, AuthenticationProperties properties)
        {
            return base.ChallengeAsync(context, scheme, properties);
        }

        Task IAuthenticationService.ForbidAsync(HttpContext context, string scheme, AuthenticationProperties properties)
        {
            return base.ForbidAsync(context, scheme, properties);
        }

        Task IAuthenticationService.SignInAsync(HttpContext context, string scheme, ClaimsPrincipal principal, AuthenticationProperties properties)
        {
            return base.SignInAsync(context, scheme,principal, properties);
        }

        Task IAuthenticationService.SignOutAsync(HttpContext context, string scheme, AuthenticationProperties properties)
        {
            return base.SignOutAsync(context, scheme, properties);
        }
    }
}
