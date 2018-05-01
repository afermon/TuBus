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
    public class TerminalController : ApiController
    {
        ApiResponse apiResp = new ApiResponse();

        /// <summary>
        /// Registra una nueva terminal
        /// </summary>
        /// <param name="terminal">Terminal</param>
        /// <returns></returns>
        [HttpPost]
        [ClaimsAuthorization(ClaimType = "action", ClaimValue = "terminal-registrarTerminal")]
        public IHttpActionResult RegistrarTerminal(Terminal terminal)
        {

            try
            {
                var mng = new TerminalManager();
                mng.Create(terminal);
                apiResp.Message = "Terminal registrada";
                return Ok(apiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }


        /// <summary>
        /// Obtiene una lista de todas las terminales
        /// </summary>
        /// <returns>Lista terminales</returns>
        [HttpGet]
        [AllowAnonymous]  //Usado en homepage
        public IHttpActionResult ObtenerTerminales()
        {

            try
            {
                var mng = new TerminalManager();
                apiResp.Data = mng.RetrieveAll();
                apiResp.Message = "Terminales en el sistema";

                return Ok(apiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        /// <summary>
        /// Actualiza la información de una Terminal
        /// </summary>
        /// <param name="terminal">Terminal</param>
        /// <returns></returns>
        [HttpPut]
        [ClaimsAuthorization(ClaimType = "action", ClaimValue = "terminal-modificarTerminal")]
        public IHttpActionResult ModificarTerminal(Terminal terminal)
        {
            try
            {
                var mng = new TerminalManager();
                mng.Update(terminal);

                apiResp = new ApiResponse();
                apiResp.Message = "Terminal modificada";

                return Ok(apiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        /// <summary>
        /// Método para desactivar una terminal del sistema
        /// </summary>
        /// <param name="terminal">Terminal</param>
        /// <returns></returns>
        [HttpDelete]
        [ClaimsAuthorization(ClaimType = "action", ClaimValue = "terminal-desactivarTerminal")]
        public IHttpActionResult DesactivarTerminal(Terminal terminal)
        {

            try
            {
                var mng = new TerminalManager();
                mng.Delete(terminal);
                apiResp.Message = "Terminal eliminada";

                return Ok(apiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        /// <summary>
        /// Activa una terminal
        /// </summary>
        /// <param name="terminal">Terminal</param>
        [HttpPut]
        [ClaimsAuthorization(ClaimType = "action", ClaimValue = "terminal-activarTerminal")]
        public IHttpActionResult ActivarTerminal(Terminal terminal)
        {
            try
            {
                var mng = new TerminalManager();
                mng.Activate(terminal);
                apiResp.Message = "Terminal actualizada";

                return Ok(apiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }
    }

}