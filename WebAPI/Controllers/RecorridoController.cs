using System;
using System.Web.Http;
using CoreAPI;
using Exceptions;
using WebAPI.Models;
using Entities;

namespace WebAPI.Controllers
{
    [Authorize]
    [ExceptionFilter]
    public class RecorridoController : ApiController
    {
        ApiResponse _apiResp = new ApiResponse();

        /// <summary>
        /// Registrar bus en rampa
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous] //Usado en servicio
        public IHttpActionResult RegistrarRecorrido(Recorrido recorrido)
        {
            _apiResp = new ApiResponse();
            var mng = new RecorridoManager();
            try
            {
                _apiResp.Data = mng.RegistrarRecorrido(recorrido);
                _apiResp.Message = "Recorrido Registrado";
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }

            return Ok(_apiResp);
        }

        /// <summary>
        /// Ingreso de pasajero a bus
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous] //Usado en servicio
        public IHttpActionResult Ingreso(IngresoBus ingreso)
        {
            _apiResp = new ApiResponse();
            var mng = new RecorridoManager();
            try
            {
                mng.RegistrarIngresoPasajero(ingreso);
                _apiResp.Message = "Ingreso Correcto";
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }

            return Ok(_apiResp);
        }

        /// <summary>
        /// Registrar salida bus
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [AllowAnonymous] //Usado en servicio
        public IHttpActionResult Salida(Recorrido recorrido)
        {
            _apiResp = new ApiResponse();
            var mng = new RecorridoManager();
            try
            {
                mng.RegistrarSalida(recorrido);
                _apiResp.Message = "Salida registrada.";
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }

            return Ok(_apiResp);
        }

        /// <summary>
        /// Registrar fin del recorrido
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [AllowAnonymous] //Usado en servicio
        public IHttpActionResult Llegada(Recorrido recorrido)
        {
            _apiResp = new ApiResponse();
            var mng = new RecorridoManager();
            try
            {
                mng.RegistrarLlegada(recorrido);
                _apiResp.Message = "Llegada registrada.";
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }

            return Ok(_apiResp);
        }

        /// <summary>
        /// Actualizar posicion del bus
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [AllowAnonymous] //Usado en servicio
        public IHttpActionResult UpdatePosicion(Posicion posicion)
        {
            _apiResp = new ApiResponse();
            var mng = new RecorridoManager();
            try
            {
                mng.UpdatePosition(posicion);
                _apiResp.Message = "Actualizado correctamente.";
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }

            return Ok(_apiResp);
        }

        /// <summary>
        /// Obtener posiciones actuales de los buses en recorrido.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous] //Usado en servicio
        public IHttpActionResult GetPositions()
        {
            _apiResp = new ApiResponse();
            var mng = new RecorridoManager();
            try
            {
                _apiResp.Data = mng.RetrievePosiciones();
                _apiResp.Message = "Lista de recorridos activos";
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }

            return Ok(_apiResp);
        }
    }
}
