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
    public class QuejaController : ApiController
    {
        ApiResponse apiResp = new ApiResponse();

        ///<Summary>Registra una nueva queja</Summary>
        /// <param name="queja">Objeto Queja</param>
        [HttpPost]
        [ClaimsAuthorization(ClaimType = "action", ClaimValue = "queja-registrarQueja")]
        public IHttpActionResult RegistrarQueja(Queja queja)
        {

            try
            {
                var mng = new QuejaManager();
                mng.Create(queja);
                apiResp.Message = "Queja registrada";
                return Ok(apiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        ///<summary>Obtiene una lista de todas las quejas</summary>
        /// <returns>Lista de quejas</returns>
        [HttpGet]
        [ClaimsAuthorization(ClaimType = "action", ClaimValue = "queja-obtenerQuejas")]
        public IHttpActionResult ObtenerQuejas()
        {

            try
            {
                var mng = new QuejaManager();
                apiResp.Data = mng.RetrieveAll();
                apiResp.Message = "Quejas en el sistema";

                return Ok(apiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }


        ///<summary>Obtiene una queja por el ID</summary>
        /// <param name="queja">Objeto Queja</param>
        /// <returns>Lista de quejas</returns>
        [HttpGet]
        [ClaimsAuthorization(ClaimType = "action", ClaimValue = "queja-obtenerQuejasId")]
        public IHttpActionResult ObtenerQuejasId(Queja queja)
        {

            try
            {
                var mng = new QuejaManager();
                apiResp.Data = mng.RetrieveById(queja);
                apiResp.Message = "Quejas en el sistema";

                return Ok(apiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        ///<summary>Obtiene una lista de quejas activas en el sistema</summary>
        /// <returns>Lista de quejas</returns>
        [HttpGet]
        [ClaimsAuthorization(ClaimType = "action", ClaimValue = "queja-obtenerQuejasActivas")]
        public IHttpActionResult ObtenerQuejasActivas()
        {

            try
            {
                var mng = new QuejaManager();
                apiResp.Data = mng.RetrieveQuejasActivas();
                apiResp.Message = "Quejas en el sistema";

                return Ok(apiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        ///<summary>Método para desactivar una queja del sistema</summary>
        [HttpDelete]
        [ClaimsAuthorization(ClaimType = "action", ClaimValue = "queja-desactivarQueja")]
        public IHttpActionResult DesactivarQueja(Queja queja)
        {

            try
            {
                var mng = new QuejaManager();
                mng.Delete(queja);
                apiResp.Message = "Queja eliminada";

                return Ok(apiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }
    }
}