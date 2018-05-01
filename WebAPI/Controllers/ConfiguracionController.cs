using System;
using System.Web.Http;
using CoreAPI;
using Entities;
using Exceptions;
using WebAPI.Auth;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Authorize]
    [ExceptionFilter]
    public class ConfiguracionController : ApiController
    {

        ApiResponse _apiResp = new ApiResponse();

        /// <summary>
        /// Retorna la configuración general de la aplicación.
        /// </summary>
        /// <returns>ApiResponse</returns>
        [HttpGet]
        [ClaimsAuthorization(ClaimType = "action", ClaimValue = "configuracion-general")]
        public IHttpActionResult General()
        {
            try
            {
                var mng = new ConfiguracionManager();

                _apiResp = new ApiResponse {Data = mng.RetrieveConfiguracion()};

                return Ok(_apiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        /// <summary>
        /// Actualiza configuración general de la aplicación.
        /// </summary>
        /// <param name="config">Configuración</param>
        /// <returns>ApiResponse</returns>
        [HttpPut]
        [ClaimsAuthorization(ClaimType = "action", ClaimValue = "configuracion-general")]
        public IHttpActionResult General(Configuracion config)
        {
            try
            {
                var mng = new ConfiguracionManager();
                mng.UpdateConfiguracion(config);

                _apiResp = new ApiResponse {Message = "Actualizado correctamente."};

                return Ok(_apiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        /// <summary>
        /// Retorna la configuración de una terminal
        /// </summary>
        /// <param name="id">ID de la terminal</param>
        /// <returns>ApiResponse</returns>
        [HttpGet]
        [ClaimsAuthorization(ClaimType = "action", ClaimValue = "configuracion-terminal")]
        public IHttpActionResult Terminal(int id)
        {
            try
            {
                var mng = new ConfiguracionManager();

                _apiResp = new ApiResponse { Data = mng.RetrieveConfiguracionTerminal(id)};

                return Ok(_apiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        /// <summary>
        /// Actualiza configuración de una terminal.
        /// </summary>
        /// <param name="config">Configuración de terminal</param>
        /// <returns>ApiResponse</returns>
        [HttpPut]
        [ClaimsAuthorization(ClaimType = "action", ClaimValue = "configuracion-terminal")]
        public IHttpActionResult Terminal(ConfiguracionTerminal config)
        {
            try
            {
                var mng = new ConfiguracionManager();
                mng.UpdateConfiguracionTerminal(config);

                _apiResp = new ApiResponse { Message = "Actualizado correctamente." };

                return Ok(_apiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

    }
}
