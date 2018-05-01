using CoreAPI;
using Entities;
using Exceptions;
using System;
using System.Web.Http;
using WebAPI.Models;
using WebAPI.Auth;

namespace WebAPI.Controllers
{
    public class ChoferController : ApiController
    {
        ApiResponse apiResp = new ApiResponse();

        ///<summary>Registra un nuevo chofer</summary>
        /// <param name="chofer"></param>
        [HttpPost]
        [ClaimsAuthorization(ClaimType = "action", ClaimValue = "chofer-registrarChofer")]
        public IHttpActionResult RegistrarChofer(Chofer chofer)
        {

            try
            {
                var mng = new ChoferManager();
                mng.Create(chofer);
                apiResp.Message = "Chofer registrado";
                return Ok(apiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }


        ///<summary>Obtiene una lista de todos los choferes</summary>
        /// <returns>Lista de choferes</returns>
        [HttpGet]
        //[ClaimsAuthorization(ClaimType = "action", ClaimValue = "chofer-obtenerChoferes")]
        [AllowAnonymous] //Usado por el servicio
        public IHttpActionResult ObtenerChofer()
        {

            try
            {
                var mng = new ChoferManager();
                apiResp.Data = mng.RetrieveAll();
                apiResp.Message = "Choferes en el sistema";

                return Ok(apiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        ///<summary>Obtiene una lista de los choferes por ID de ruta</summary>
        /// <returns>Lista de choferes</returns>
        [HttpGet]
        [ClaimsAuthorization(ClaimType = "action", ClaimValue = "chofer-obtenerChoferRuta")]
        public IHttpActionResult ObtenerChoferRuta(int Ruta)
        {

            try
            {
                var mng = new ChoferManager();
                apiResp.Data = mng.RetrieveAllRuta(Ruta);
                apiResp.Message = "Choferes en el sistema";

                return Ok(apiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        ///<summary>Actualiza la información de un chofer</summary>
        /// <param name="chofer"></param>
        [HttpPut]
        [ClaimsAuthorization(ClaimType = "action", ClaimValue = "chofer-modificarChofer")]
        public IHttpActionResult ModificarChofer(Chofer chofer)
        {
            try
            {
                var mng = new ChoferManager();
                mng.Update(chofer);

                apiResp = new ApiResponse();
                apiResp.Message = "Chofer modificado";

                return Ok(apiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        ///<summary>Método para desactivar un chofer del sistema</summary>
        [HttpDelete]
        [ClaimsAuthorization(ClaimType = "action", ClaimValue = "chofer-desactivarChofer")]
        public IHttpActionResult DesactivarChofer(Chofer chofer)
        {

            try
            {
                var mng = new ChoferManager();
                mng.Delete(chofer);
                apiResp.Message = "Chofer eliminado";

                return Ok(apiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        /// <summary>
        /// Activa un chofer
        /// </summary>
        /// <param name="chofer">Chofer</param>
        [HttpPut]
        [ClaimsAuthorization(ClaimType = "action", ClaimValue = "chofer-activarChofer")]
        public IHttpActionResult ActivarChofer(Chofer chofer)
        {
            try
            {
                var mng = new ChoferManager();
                mng.Activate(chofer);
                apiResp.Message = "Chofer actualizado";

                return Ok(apiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }
    }
}