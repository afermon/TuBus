using CoreAPI;
using Entities;
using Exceptions;
using System;
using System.Web.Http;
using WebAPI.Models;
using WebAPI.Auth;

namespace WebAPI.Controllers
{
    [ExceptionFilter]
    [Authorize]

    public class SancionController : ApiController
    {
        ApiResponse apiResp = new ApiResponse();

        ///<summary>Registra una nueva sanción</summary>
        /// <param name="sancion"></param>
        [HttpPost]
        [ClaimsAuthorization(ClaimType = "action", ClaimValue = "sancion-registrarSancion")]
        public IHttpActionResult RegistrarSancion(Sancion sancion)
        {

            try
            {
                var mng = new SancionManager();
                mng.Create(sancion);
                apiResp.Message = "Sanción registrada";
                return Ok(apiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        ///<summary>Obtiene una lista de las sanciones registradas</summary>
        /// <returns>Lista de sanciones</returns>
        [HttpGet]
        [ClaimsAuthorization(ClaimType = "action", ClaimValue = "sancion-obtenerSanciones")]
        public IHttpActionResult ObtenerSanciones()
        {

            try
            {
                var mng = new SancionManager();
                apiResp.Data = mng.RetrieveAll();
                apiResp.Message = "Sanciones en el sistema";

                return Ok(apiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        ///<summary>Obtiene una lista de las sanciones activas</summary>
        /// <returns>Lista de sanciones</returns>
        [HttpGet]
        [ClaimsAuthorization(ClaimType = "action", ClaimValue = "sancion-obtenerSancionesActivas")]
        public IHttpActionResult ObtenerSancionesActivas()
        {

            try
            {
                var mng = new SancionManager();
                apiResp.Data = mng.RetrieveAllActivas();
                apiResp.Message = "Sanciones en el sistema";

                return Ok(apiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        ///<summary>Actualiza la información de una sanción</summary>
        /// <param name="sancion"></param>
        [HttpPut]
        [ClaimsAuthorization(ClaimType = "action", ClaimValue = "sancion-modificarSancion")]
        public IHttpActionResult ModificarSancion(Sancion sancion)
        {
            try
            {
                var mng = new SancionManager();
                mng.Update(sancion);

                apiResp = new ApiResponse();
                apiResp.Message = "Sanción modificada";

                return Ok(apiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        ///<summary>Método para desactivar una terminal del sistema</summary>
        [HttpDelete]
        [ClaimsAuthorization(ClaimType = "action", ClaimValue = "sancion-desactivarSancion")]
        public IHttpActionResult DesactivarSancion(Sancion sancion)
        {

            try
            {
                var mng = new SancionManager();
                mng.Delete(sancion);
                apiResp.Message = "Sanción eliminada";

                return Ok(apiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }
    }
}