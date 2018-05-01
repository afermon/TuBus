using System.Web.Http;
using CoreAPI;
using WebAPI.Models;
using Exceptions;
using System;
using Entities;

namespace WebAPI.Controllers
{
    public class ParqueoPublicoController : ApiController
    {
        ApiResponse _apiResponse = new ApiResponse();

        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult ObtenerParqueoPorTerminal(int terminal)
        {
            if (terminal < 1)
                terminal = 1;

            try
            {
                var manager = new ParqueoPublicoManager();

                _apiResponse = new ApiResponse
                {
                    Data = manager.RetrieveByTerminalId(terminal),
                    Message = "Success"
                };

                return Ok(_apiResponse);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        ///<summary>Registra ingreso al parqueo</summary>
        /// <param name="registro">Objeto ResgistroParqueo</param>
        [HttpPost]
        [AllowAnonymous]
        //[ClaimsAuthorization(ClaimType = "action", ClaimValue = "role-registrarIngreso")]
        public IHttpActionResult RegistrarIngreso(RegistroParqueo registro)
        {
            try
            {
                var manager = new ParqueoPublicoManager();

                manager.CreateIngreso(registro);
                _apiResponse.Message = "Ingreso al parqueo registrado";

                return Ok(_apiResponse);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        ///<summary>Registra una salida del parqueo</summary>
        /// <param name="registro">Objeto ResgistroParqueo</param>
        [HttpPost]
        [AllowAnonymous]
        //[ClaimsAuthorization(ClaimType = "action", ClaimValue = "role-registrarSalida")]
        public IHttpActionResult RegistrarSalida(RegistroParqueo registro)
        {
            try
            {
                var manager = new ParqueoPublicoManager();

                manager.CreateSalida(registro);
                _apiResponse.Message = "Salida al parqueo registrado";

                return Ok(_apiResponse);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }
    }
}