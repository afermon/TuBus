using System;
using System.Web.Http;
using CoreAPI;
using Entities;
using Exceptions;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class TipoTarjetaController : ApiController
    {
        private ApiResponse _apiResponse;
        
        /// <summary>Obtine todos los tipos de tarjetas </summary>
        /// <returns>Lista de tipos de tarjetas</returns>
        [HttpGet]
        public IHttpActionResult GetAllTypes()
        {
            try
            {
                var mng = new TipoTarjetaManager();
                _apiResponse = new ApiResponse();
                _apiResponse.Data = mng.GetAllTypesCards();
                _apiResponse.Message = "Execution Success";
                return Ok(_apiResponse);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }
        /// <summary> Ontiene un tipo de tarjeta, por nombre o por Id </summary>
        /// <param name="cardType"> Tipo Tarjeta</param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetType(TipoTarjeta cardType)
        {
            try
            {
                var mng = new TipoTarjetaManager();
                _apiResponse = new ApiResponse();
                _apiResponse.Data = mng.GetCardTypeById(cardType);
                _apiResponse.Message = "Execution Success";
                
                return Ok(_apiResponse);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        /// <summary>Crear un tipo de tarjeta </summary>
        /// <param name="cardtype"> Tipo Tarjeta</param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult Create([FromBody]TipoTarjeta cardtype)
        {
            try
            {
                var mng = new TipoTarjetaManager();
                _apiResponse = new ApiResponse();
                mng.CreateCardType(cardtype);
                _apiResponse.Message = "Tipo de Tarjeta Creado";

                return Ok(_apiResponse);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        /// <summary>Actualiza tipo tarjeta </summary>
        /// <param name="cardtype"></param>
        [HttpPut]
        public IHttpActionResult Update([FromBody]TipoTarjeta cardtype)
        {
            try
            {
                var mng = new TipoTarjetaManager();
                _apiResponse = new ApiResponse();
                mng.UpdateCardType(cardtype);
                _apiResponse.Message = "Tipo de Tarjeta Actualizado";

                return Ok(_apiResponse);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        /// <summary> Inactiva un tipo de tarjeta </summary>
        /// <param name="cardtype"></param>
        [HttpDelete]
        public IHttpActionResult Delete(TipoTarjeta cardtype)
        {
            try
            {
                var mng = new TipoTarjetaManager();
                _apiResponse = new ApiResponse();
                mng.DeleteCardType(cardtype);
                _apiResponse.Message = "Tipo de tarjeta eliminada";

                return Ok(_apiResponse);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }
    }
}
