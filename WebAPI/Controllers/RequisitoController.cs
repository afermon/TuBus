using CoreAPI;
using Entities;
using Exceptions;
using System;
using System.Web.Http;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    //[Authorize]
    [ExceptionFilter]
    public class RequisitoController : ApiController
    {
        ApiResponse _apiResponse = new ApiResponse();

        /// <summary>
        /// Retorna todos los requisitos por placa en el sistema
        /// </summary>
        /// <returns>Lista de requisitos por placa</returns>
        [HttpGet]
        public IHttpActionResult ObtenerPorPlaca(Bus bus)
        {
            _apiResponse = new ApiResponse();
            var requisitoManager = new RequisitoManager();

            try
            {
                _apiResponse.Data = requisitoManager.RetrieveAllById(new Requisito { Placa = bus.Id });
                _apiResponse.Message = "Lista de buses";
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }

            return Ok(_apiResponse);
        }
    }
}