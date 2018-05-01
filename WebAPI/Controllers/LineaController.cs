using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CoreAPI;
using Entities;
using Exceptions;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class LineaController : ApiController
    {
        private ApiResponse _apiResponse;
        /// <summary>
        /// Obtine todas la lineas
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetAllLines(int terminal, int empresaId = 0)
        {
            try
            {
                var mng = new LineaManager();
                _apiResponse = new ApiResponse();
                _apiResponse.Data = mng.GetAllLines(terminal, empresaId);
                _apiResponse.Message = "Execution Success";
                return Ok(_apiResponse);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }
        /// <summary> Ontiene una linea por Id </summary>
        /// <param name="line"> Linea</param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetLine(Linea line)
        {
            try
            {
                var mng = new LineaManager();
                _apiResponse = new ApiResponse();
                _apiResponse.Data = mng.GetLineById(line);
                _apiResponse.Message = "Execution Success";

                return Ok(_apiResponse);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        /// <summary>Crear una linea </summary>
        /// <param name="line"> Linea</param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult Create([FromBody]Linea line)
        {
            try
            {
                var mng = new LineaManager();
                _apiResponse = new ApiResponse();
                mng.CreateLine(line);
                _apiResponse.Message = "La línea se ha creado";

                return Ok(_apiResponse);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        /// <summary>Actualiza Linea </summary>
        /// <param name="line">Linea</param>
        [HttpPut]
        public IHttpActionResult Update([FromBody]Linea line)
        {
            try
            {
                var mng = new LineaManager();
                _apiResponse = new ApiResponse();
                mng.UpdateLine(line);
                _apiResponse.Message = "Linea actualizada";

                return Ok(_apiResponse);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        /// <summary> Inactiva una linea </summary>
        /// <param name="line"></param>
        [HttpDelete]
        public IHttpActionResult Delete(Linea line)
        {
            try
            {
                var mng = new LineaManager();
                _apiResponse = new ApiResponse();
                mng.DeleteLine(line);
                _apiResponse.Message = "Liena eliminada";

                return Ok(_apiResponse);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }
        [HttpGet]
        public IHttpActionResult Espacios(int terminal)
        {
            try
            {
                var mng = new LineaManager();
                _apiResponse = new ApiResponse();
                _apiResponse.Data = mng.GetTotalSpaces(new Linea {Terminal = new Terminal{Id = terminal } });
                _apiResponse.Message = "Espacios";

                return Ok(_apiResponse);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

    }
}
