using System;
using System.Web.Http;
using CoreAPI;
using Entities;
using Exceptions;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class ConvenioController : ApiController
    {
        private ApiResponse _apiResponse;

        /// <summary>
        /// Obtiene todos los convenios
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetAllAgreements(int terminal = 0)
        {
            try
            {
                var mng = new ConvenioManager();
                _apiResponse = new ApiResponse();
                _apiResponse.Data = mng.GetAll(terminal);
                _apiResponse.Message = "Execution Success";
                return Ok(_apiResponse);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }
        
        /// <summary>
        /// Obtiene un convenio por cedula juridica
        /// </summary>
        /// <param name="convenio"></param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetAgreement(Convenio convenio)
        {
            try
            {
                var mng = new ConvenioManager();
                _apiResponse = new ApiResponse();
                _apiResponse.Data = mng.GetById(convenio);
                _apiResponse.Message = "Execution Success";

                return Ok(_apiResponse);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        /// <summary>
        /// Crear Convenio
        /// </summary>
        /// <param name="convenio"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult Create(Convenio convenio)
        {
            try
            {
                var mng = new ConvenioManager();
                _apiResponse = new ApiResponse();
                mng.Create(convenio);
                _apiResponse.Message = "Convenio creado de manera correcta";

                return Ok(_apiResponse);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        /// <summary>
        /// Actualizar Convenio
        /// </summary>
        /// <param name="convenio"></param>
        /// <returns></returns>
        [HttpPut]
        public IHttpActionResult Update([FromBody]Convenio convenio)
        {
            try
            {
                var mng = new ConvenioManager();
                _apiResponse = new ApiResponse();
                mng.Update(convenio);
                _apiResponse.Message = "Convenio actualizado de manera correcta";

                return Ok(_apiResponse);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }


        [HttpDelete]
        public IHttpActionResult Delete(Convenio convenio)
        {
            try
            {
                var mng = new ConvenioManager();
                _apiResponse = new ApiResponse();
                mng.Delete(convenio);
                _apiResponse.Message = "Convenio eliminado";

                return Ok(_apiResponse);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        /// <summary>
        /// Obtine los convenios de una terminal
        /// </summary>
        /// <param name="terminal"></param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetAgreementsByTerminal(int terminal)
        {
            try
            {
                var mng = new ConvenioManager();
                _apiResponse = new ApiResponse();
                _apiResponse.Data = mng.GetByTerminalId(new Convenio{Terminal = new Terminal{Id = terminal}});
                _apiResponse.Message = "Execution Success";
                return Ok(_apiResponse);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        /// <summary>
        /// Solicitar tarjetas
        /// </summary>
        /// <param name="solicitud"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult SolictudTarjetas(Solicitud solicitud)
        {
            try
            {
                var mng = new ConvenioManager();
                _apiResponse = new ApiResponse();
                mng.SolicitudTarjetaConvenio(solicitud);
                _apiResponse.Message = "Solicitud realizada";
                return Ok(_apiResponse);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        [HttpGet]
        public IHttpActionResult GetAllSolicitudes(int terminal = 0)
        {
            try
            {
                var mng = new SolicitudManager();
                _apiResponse = new ApiResponse();
                _apiResponse.Data = mng.GetAll(terminal);
                _apiResponse.Message = "Execution Success";
                return Ok(_apiResponse);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        /// <summary>
        /// Procesa una solicitud por Id
        /// </summary>
        /// <param name="solicitudId"></param>
        /// <param name="isFromAdmin"></param>
        /// <param name="isDenied"></param>
        /// <returns></returns>
        [HttpPut]
        public IHttpActionResult ProcesarSolicitud(string solicitudId, bool isFromAdmin = false, bool isDenied = false)
        {
            try
            {
                var mng = new SolicitudManager();
                _apiResponse = new ApiResponse();
                mng.ProcesarSolicitud(solicitudId, isFromAdmin,isDenied);
                _apiResponse.Message = !isDenied ?"Solicitud procesada, tarjetas generadas" : "Solicitud rechazda de manera correcta";
                return Ok(_apiResponse);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

    }
}
