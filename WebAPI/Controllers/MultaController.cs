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
    public class MultaController : ApiController
    {
        ApiResponse apiResp = new ApiResponse();

        ///<summary>Registra una nueva multa</summary>
        /// <param name="multa"></param>
        [HttpPost]
        [ClaimsAuthorization(ClaimType = "action", ClaimValue = "multa-registrarMulta")]
        public IHttpActionResult RegistrarMulta(Multa multa)
        {

            try
            {
                var mng = new MultaManager();
                mng.Create(multa);
                apiResp.Message = "Multa registrada";
                return Ok(apiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        ///<summary>Obtiene una lista de todas las multas</summary>
        /// <returns>Lista de multas</returns>
        [HttpGet]
        [ClaimsAuthorization(ClaimType = "action", ClaimValue = "multa-obtenerMultas")]
        public IHttpActionResult ObtenerMultas()
        {

            try
            {
                var mng = new MultaManager();
                apiResp.Data = mng.RetrieveAll();
                apiResp.Message = "Multas en el sistema";

                return Ok(apiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        ///<summary>Método para cancelar una multa en el sistema</summary>
        [HttpDelete]
        [ClaimsAuthorization(ClaimType = "action", ClaimValue = "multa-desactivarMulta")]
        public IHttpActionResult DesactivarMulta(Multa multa)
        {

            try
            {
                var mng = new MultaManager();
                mng.Delete(multa);
                apiResp.Message = "Multa eliminada";

                return Ok(apiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        ///<summary>Actualiza la información de una Multa</summary>
        /// <param name="multa"></param>
        [HttpPut]
        [ClaimsAuthorization(ClaimType = "action", ClaimValue = "multa-modificarMulta")]
        public IHttpActionResult ModificarMulta(Multa multa)
        {
            try
            {
                var mng = new MultaManager();
                mng.Update(multa);

                apiResp = new ApiResponse();
                apiResp.Message = "Multa modificada";

                return Ok(apiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }
    }
}