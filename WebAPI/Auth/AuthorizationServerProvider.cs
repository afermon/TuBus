using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CoreAPI;
using Entities;
using Exceptions;
using Microsoft.Owin.Security.OAuth;

namespace WebAPI.Auth
{
    public class AuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var header = context.OwinContext.Response.Headers.SingleOrDefault(h => h.Key == "Access-Control-Allow-Origin");
            if (header.Equals(default(KeyValuePair<string, string[]>)))
            {
                context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });
            }

            Usuario usuario = null;

            try
            {
                var usuarioManager = new UsuarioManager();

                usuario = usuarioManager.ValidateUser(new Usuario { Email = context.UserName, Password = context.Password });
            }
            catch (BusinessException bex)
            {
                context.SetError("invalid_grant", bex.ExceptionId + " - " + bex.AppMessage.Message);
                return;
            }
           
            var identity = new ClaimsIdentity(context.Options.AuthenticationType);

            var appClaimManager = new AppClaimManager();
            foreach (var appClaim in appClaimManager.RetrieveAppClaimByRole(usuario.RoleId))
                identity.AddClaim(new Claim(appClaim.Type, appClaim.AppClaimId));

            identity.AddClaim(new Claim("Email", usuario.Email));
            identity.AddClaim(new Claim("RoleId", usuario.RoleId));

            context.Validated(identity);

        }
    }
}