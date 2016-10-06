using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.Security.Claims;

namespace Puppr.API.Infrastructure
{
    public class SimpleAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        // validates our front end clients
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        // validates our 
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

            using (var authRepository = new AuthRepository())
            {
                var user = await authRepository.FindUser(context.UserName, context.Password);

                if(user == null)
                {
                    context.SetError("invalid_grant", "The username or password is incorrect");
                    return;
                }
            }

            // print a token
            var token = new ClaimsIdentity(context.Options.AuthenticationType);
            token.AddClaim(new Claim(ClaimTypes.Name, context.UserName));

            context.Validated(token);
        }
    }
}