using System.Linq;
using System.Security.Claims;
using System.Web.Http;
using CoreAPI;
using Entities;
using WebAPI.Models;
using Exceptions;
using System;

namespace WebAPI.Controllers
{
    [ExceptionFilter]
    public class VistaController : ApiController
    {
        ApiResponse apiResp = new ApiResponse();

        /// <summary>
        /// Retorna las vistas a las que el role del usuario actualmente logueado tiene acceso.
        /// </summary>
        /// <returns> Vista </returns>
        [HttpGet]
        public IHttpActionResult Current()
        {
            var identity = User.Identity as ClaimsIdentity;
            var roleId = identity.Claims.Where(c => c.Type == "RoleId").Select(c => c.Value).SingleOrDefault();
            var mng = new VistaManager();

            apiResp.Data = mng.RetrieveViewByRole(roleId);
            
            return Ok(apiResp);
        }

        /// <summary>
        /// Retorna todos los vistas registrados.
        /// </summary>
        /// <returns>Vista</returns>
        [HttpGet]
        //[ClaimsAuthorization(ClaimType = "action", ClaimValue = "vista-obtenerVistas")]
        public IHttpActionResult ObtenerVistas()
        {
            try
            {
                var manager = new VistaManager();

                apiResp.Data = manager.RetrieveAll();
                apiResp.Message = "Vistas en el sistema";

                return Ok(apiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

    }
}
