using System.Web.Http;
using CoreAPI;
using WebAPI.Models;
using Exceptions;
using System;
using Entities;

namespace WebAPI.Controllers
{
    public class BusController : ApiController
    {
        ApiResponse _apiResponse = new ApiResponse();

        ///<summary>Registra ingreso al parqueo</summary>
        /// <param name="bus">Objeto Bus</param>
        [HttpPost]
        //[ClaimsAuthorization(ClaimType = "action", ClaimValue = "bus-registrarBus")]
        public IHttpActionResult RegistrarBus(Bus bus)
        {
            try
            {
                var manager = new BusManager();

                manager.CreateBus(bus);
                _apiResponse.Message = "El bus se registró exitosamente.";

                return Ok(_apiResponse);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        /// <summary>
        /// Retorna todos los buses en el sistema
        /// </summary>
        /// <returns>Lista de buses</returns>
        [HttpGet]
        [AllowAnonymous] //Usado por el servicio
        public IHttpActionResult ObtenerBuses()
        {
            _apiResponse = new ApiResponse();
            var busManager = new BusManager();
            var requisitoManager = new RequisitoManager();

            try
            {
                _apiResponse.Data = busManager.RetrieveAll();
                _apiResponse.Message = "Lista de buses";
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }

            return Ok(_apiResponse);
        }

        ///<summary>Actualiza el estado de un bus</summary>
        /// <param name="bus">Objeto Bus</param>
        [HttpPut]
        //[ClaimsAuthorization(ClaimType = "action", ClaimValue = "bus-desactivarBus")]
        public IHttpActionResult DesactivarBus(Bus bus)
        {
            try
            {
                var manager = new BusManager();

                manager.Disable(bus);

                _apiResponse = new ApiResponse
                {
                    Message = "Estado del bus modificado."
                };

                return Ok(_apiResponse);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        ///<summary>Actualiza la información de un bus</summary>
        /// <param name="bus">Objeto Bus</param>
        [HttpPut]
        //[ClaimsAuthorization(ClaimType = "action", ClaimValue = "role-modificarBus")]
        public IHttpActionResult ModificarBus(Bus bus)
        {
            try
            {
                var manager = new BusManager();

                manager.Update(bus);

                _apiResponse = new ApiResponse
                {
                    Message = "Bus modificado correctamente."
                };

                return Ok(_apiResponse);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        /// <summary>
        /// Retorna una ruta por su id
        /// </summary>
        /// <param name="placa">Id del bus</param>
        /// <returns>Bus</returns>
        [HttpGet]
        public IHttpActionResult ObtenerBus(string placa)
        {
            _apiResponse = new ApiResponse();

            var busManager = new BusManager();
            try
            {
                _apiResponse.Data = busManager.Retrieve(new Bus { Id = placa });
                _apiResponse.Message = "Bus";
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }

            return Ok(_apiResponse);
        }
    }
}