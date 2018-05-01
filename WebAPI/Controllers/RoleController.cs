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
    //[Authorize]
    [ExceptionFilter]
    public class RoleController : ApiController
    {
        ApiResponse apiResp = new ApiResponse();

        /// <summary>
        /// Retorna el role del usuario actualmente logueado.
        /// </summary>
        /// <returns>Role</returns>
        [HttpGet]
        public IHttpActionResult Current()
        {
            var identity = User.Identity as ClaimsIdentity;
            var roleId = identity.Claims.Where(c => c.Type == "RoleId").Select(c => c.Value).SingleOrDefault();
            var mng = new RoleManager();

            apiResp.Data = mng.RetrieveById(new Role { RoleId = roleId });

            return Ok(apiResp);
        }

        /// <summary>
        /// Retorna todos los roles registrados.
        /// </summary>
        /// <returns>Role</returns>
        [HttpGet]
        //[ClaimsAuthorization(ClaimType = "action", ClaimValue = "rol-obtenerRoles")]
        public IHttpActionResult ObtenerRoles()
        {
            try
            {
                var manager = new RoleManager();

                apiResp.Data = manager.RetrieveAll();
                apiResp.Message = "Roles en el sistema";

                return Ok(apiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        /// <summary>
        /// Retorna todos los roles registrados.
        /// </summary>
        /// <returns>Role</returns>
        [HttpGet]
        //[ClaimsAuthorization(ClaimType = "action", ClaimValue = "rol-obtenerVistasPorRol")]
        public IHttpActionResult ObtenerVistasPorRol(Role role)
        {
            try
            {
                var manager = new RoleManager();

                apiResp.Data = manager.RetrieveViewsById(role);
                apiResp.Message = "Vistas del rol";

                return Ok(apiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        ///<summary>Registra un nuevo rol</summary>
        /// <param name="role">Objeto Rol</param>
        [HttpPost]
        //[ClaimsAuthorization(ClaimType = "action", ClaimValue = "role-registrarRol")]
        public IHttpActionResult RegistrarRol(Role role)
        {
            try
            {
                var manager = new RoleManager();

                manager.Create(role);
                apiResp.Message = "Rol registrado";

                return Ok(apiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        ///<summary>Registra un nuevo rol</summary>
        /// <param name="role">Objeto Rol</param>
        [HttpPost]
        //[ClaimsAuthorization(ClaimType = "action", ClaimValue = "role-registrarVistaPorRol")]
        public IHttpActionResult RegistrarVistaPorRol(Role role)
        {
            try
            {
                var roleManager = new RoleManager();

                roleManager.AddViewToRol(role);
                apiResp.Message = "Registro realizado correctamente.";

                return Ok(apiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        ///<summary>Actualiza la información de un rol</summary>
        /// <param name="rol">Objeto Rol</param>
        [HttpPut]
        //[ClaimsAuthorization(ClaimType = "action", ClaimValue = "role-modificarRol")]
        public IHttpActionResult ModificarRol(Role rol)
        {
            try
            {
                var mng = new RoleManager();

                mng.Update(rol);

                apiResp = new ApiResponse
                {
                    Message = "Rol modificado"
                };

                return Ok(apiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        ///<summary>Actualiza el estado de un rol</summary>
        /// <param name="rol">Objeto Rol</param>
        [HttpPut]
        //[ClaimsAuthorization(ClaimType = "action", ClaimValue = "role-desactivarRol")]
        public IHttpActionResult DesactivarRol(Role rol)
        {
            try
            {
                var mng = new RoleManager();

                mng.Disable(rol);

                apiResp = new ApiResponse
                {
                    Message = "Estado del rol modificado."
                };

                return Ok(apiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }
    }
}