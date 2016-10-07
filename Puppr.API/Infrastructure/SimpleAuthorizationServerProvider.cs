using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.Owin.Security;

namespace Puppr.API.Infrastructure
{
    public class SimpleAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        // validates our front end clients
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            return Task.FromResult<object>(null);
        }

        // validates our 
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

            string ownerId = string.Empty;

            using (var authRepository = new AuthRepository())
            {
                var user = await authRepository.FindUser(context.UserName, context.Password);

                if(user == null)
                {
                    context.SetError("invalid_grant", "The username or password is incorrect");
                    return;
                }

                ownerId = user.Id;
            }


            // print a token
            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            identity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));

            var props =
            new AuthenticationProperties(
                new Dictionary<string, string>
                    {
                        { "ownerId", ownerId }
                    });

            var ticket = new AuthenticationTicket(identity, props);
            context.Validated(ticket);
        }
    }
}