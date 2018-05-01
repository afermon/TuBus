using CoreAPI;
using Entities;
using Exceptions;
using System;
using System.Web.Http;
using WebAPI.Models;
using WebAPI.Auth;

namespace WebAPI.Controllers
{
    public class TarifaController : ApiController
    {
        ApiResponse apiResp = new ApiResponse();

        /// <summary>
        /// Obtiene una lista de las tarifas de ARESEP
        /// </summary>
        /// <returns>Tarifas</returns>
        [HttpGet]
        public IHttpActionResult GetTarifas()
        {

            try
            {
                var mng = new TarifaManager();
                apiResp.Data = mng.RetrieveAll();
                apiResp.Message = "Tarifas";

                return Ok(apiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        /// <summary>
        /// Obtiene una lista de los operadores autorizados ARESEP
        /// </summary>
        /// <returns>Tarifas</returns>
        [HttpGet]
        public IHttpActionResult GetOperadores()
        {

            try
            {
                var mng = new TarifaManager();
                apiResp.Data = mng.RetrieveEmpresarios();
                apiResp.Message = "Operadores";

                return Ok(apiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        /// <summary>
        /// Obtener una tarifa por su id en ARESEP
        /// </summary>
        /// <param name="id">Id de tarifa en ARESEP</param>
        /// <returns>Tarifa</returns>
        [HttpGet]
        public IHttpActionResult GetTarifa(int id)
        {

            try
            {
                var mng = new TarifaManager();
                apiResp.Data = mng.Retrieve(new Tarifa { RouteId = id });
                apiResp.Message = "Tarifa";

                return Ok(apiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        /// <summary>
        /// Actualizar tarifas desde ARESEP.
        /// </summary>
        [HttpPut]
        public IHttpActionResult UpdateTarifas()
        {
            try
            {
                var mng = new TarifaManager();
                mng.UpdateTarifasAresep();
                apiResp.Message = "Tarifas actualizadas.";

                return Ok(apiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }
    }
}