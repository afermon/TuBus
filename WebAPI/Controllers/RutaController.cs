using CoreAPI;
using Entities;
using Exceptions;
using System;
using System.Linq;
using System.Security.Claims;
using System.Web.Http;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Authorize]
    [ExceptionFilter]
    public class RutaController : ApiController
    {
        ApiResponse apiResp = new ApiResponse();

        /// <summary>
        /// Retorna todos las rutas en el sistema
        /// </summary>
        /// <returns>Lista de rutas</returns>
        [HttpGet]
        [AllowAnonymous] //Usado en homepage
        public IHttpActionResult GetAll(int terminal, int empresaId = 0)
        {
            apiResp = new ApiResponse();
            var mng = new RutaManager();
            try
            {
                apiResp.Data = mng.RetrieveAll(terminal, empresaId);
                apiResp.Message = "Lista de rutas";
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }

            return Ok(apiResp);
        }

        /// <summary>
        /// Retorna una ruta por su id
        /// </summary>
        /// <param name="id">Id de la ruta</param>
        /// <returns>Ruta</returns>
        [HttpGet]
        [AllowAnonymous] //Usado en homepage
        public IHttpActionResult Get(int id)
        {
            apiResp = new ApiResponse();
            var mng = new RutaManager();
            try
            {
                apiResp.Data = mng.Retrieve(new Ruta {Id = id});
                apiResp.Message = "Ruta";
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }

            return Ok(apiResp);
        }

        /// <summary>
        /// Retorna rutas por terminal
        /// </summary>
        /// <param name="id">Id de la terminal</param>
        /// <returns>Ruta</returns>
        [HttpGet]
        [AllowAnonymous] //Usado en homepage
        public IHttpActionResult Terminal(int id)
        {
            apiResp = new ApiResponse();
            var mng = new RutaManager();
            try
            {
                apiResp.Data = mng.RetrieveByTerminal(new Ruta { TerminalId = id });
                apiResp.Message = "Ruta";
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }

            return Ok(apiResp);
        }

        /// <summary>
        /// Registra un nueva ruta
        /// </summary>
        /// <param name="ruta">Ruta</param>
        [HttpPost]
        public IHttpActionResult Create(Ruta ruta)
        {
            try
            {
                var mng = new RutaManager();
                mng.Create(ruta);
                apiResp.Message = "Ruta registrada";

                return Ok(apiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        /// <summary>
        /// Actualiza la información de una ruta
        /// </summary>
        /// <param name="ruta">Ruta</param>
        [HttpPut]
        public IHttpActionResult Update(Ruta ruta)
        {
            try
            {
                var mng = new RutaManager();
                mng.Update(ruta);
                apiResp.Message = "Ruta actualizada";

                return Ok(apiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        /// <summary>
        /// Activa una ruta
        /// </summary>
        /// <param name="ruta">Ruta</param>
        [HttpPut]
        public IHttpActionResult Activate(Ruta ruta)
        {
            try
            {
                var mng = new RutaManager();
                mng.Activate(ruta);
                apiResp.Message = "Ruta actualizada";

                return Ok(apiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        /// <summary>
        /// Desactiva una ruta
        /// </summary>
        /// <param name="ruta">Ruta</param>
        [HttpDelete]
        public IHttpActionResult Delete(Ruta ruta)
        {
            try
            {
                var mng = new RutaManager();
                mng.Delete(ruta);
                apiResp.Message = "Ruta descativada";

                return Ok(apiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }
    }
}