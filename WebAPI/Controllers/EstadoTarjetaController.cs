using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CoreAPI;
using Exceptions;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class EstadoTarjetaController : ApiController
    {
        private ApiResponse _apiResponse;

        /// <summary>
        /// Obtener todos los estados de las tarjetas
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetStateRepositionCards()
        {
            try
            {
                var mng = new EstadoTarjetaManager();
                _apiResponse = new ApiResponse();
                _apiResponse.Data = mng.GetTarjetas();
                _apiResponse.Message = "Execution Success";
                return Ok(_apiResponse);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }
    }
}
