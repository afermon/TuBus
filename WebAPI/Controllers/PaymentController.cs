using System;
using System.Web.Http;
using CoreAPI;
using Entities;
using Exceptions;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class PaymentController : ApiController
    {
        private ApiResponse _apiResponse;

        /// <summary>
        /// Make payment trough Stripe
        /// </summary>
        /// <param name="newPayment"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult MakePayment(Payment newPayment)
        {
            try
            {
                var mng = new PaymentManager();
                _apiResponse = new ApiResponse();
                _apiResponse.Data =  mng.NewPayment(newPayment);
                _apiResponse.Message = "Pago realizado exitosamente";
                return Ok(_apiResponse);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        [HttpPost]
        public IHttpActionResult EmpresaMakePayment(Payment newPayment)
        {
            try
            {
                var mng = new PaymentManager();
                _apiResponse = new ApiResponse();
                _apiResponse.Data = mng.EmpresaNewPayment(newPayment);
                _apiResponse.Message = "Pago realizado exitosamente";
                return Ok(_apiResponse);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }
    }
}
