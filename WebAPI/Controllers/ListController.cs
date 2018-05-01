using System;
using System.Web.Http;
using CoreAPI;
using Exceptions;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Authorize]
    [ExceptionFilter]
    public class ListController : ApiController
    {
        ApiResponse _apiResp = new ApiResponse();

        /// <summary>
        /// Retorna los elementos de la lista especificada
        /// </summary>
        /// <param name="id">ID de la lista</param>
        public IHttpActionResult Get(string id)
        {
            try
            {
                var mng = new ListManager();
                _apiResp = new ApiResponse {Data = mng.RetrieveListById(id)};

                return Ok(_apiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }
    }
}
