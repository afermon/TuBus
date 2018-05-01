using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using WebAPI.Models;

namespace WebAPI.Auth
{
    public class ClaimsAuthorizationAttribute : AuthorizationFilterAttribute
    {
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }

        public override Task OnAuthorizationAsync(HttpActionContext actionContext, System.Threading.CancellationToken cancellationToken)
        {

            var identity = actionContext.RequestContext.Principal.Identity as ClaimsIdentity;

            ApiResponse apiResp = new ApiResponse();

            Debug.Assert(identity != null, nameof(identity) + " != null");
            if (!identity.IsAuthenticated)
            {
                apiResp.Message = "Token invalido";
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized, apiResp);
                return Task.FromResult<object>(null);
            }

            if (!(identity.HasClaim(x => x.Type == ClaimType && x.Value == ClaimValue)))
            {
                apiResp.Message = "Acceso no autorizado.";
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized, apiResp);

                return Task.FromResult<object>(null);
            }

            //User is Authorized, complete execution
            return Task.FromResult<object>(null);

        }
    }
}