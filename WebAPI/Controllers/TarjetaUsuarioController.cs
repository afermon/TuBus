using System;
using System.Web.Http;
using CoreAPI;
using Entities;
using Exceptions;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class TarjetaUsuarioController : ApiController
    {
        private ApiResponse _apiResponse;

        /// <summary>
        /// Create user
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult CreateUser(Usuario user)
        {
            _apiResponse = new ApiResponse();
            var mng = new UsuarioManager();
            try
            {
                mng.Create(user);
                _apiResponse.Message = "Execution Success";
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }

            return Ok(_apiResponse);
        }

        /// <summary>
        /// Create Card 
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult CreateCard(Tarjeta card)
        {
            try
            {
                var mng = new TarjetaManager();
                _apiResponse = new ApiResponse();
                mng.Create(card);
                _apiResponse.Message = "Execution Success";
                return Ok(_apiResponse);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        /// <summary>
        /// Get User By Email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public IHttpActionResult GetUserByEmail(string email)
        {
            _apiResponse = new ApiResponse();
            var mng = new UsuarioManager();
            try
            {
                _apiResponse.Data = mng.Retrieve(new Usuario { Email = email });
                _apiResponse.Message = "Execution Success";
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }

            return Ok(_apiResponse);
        }
    }
}
