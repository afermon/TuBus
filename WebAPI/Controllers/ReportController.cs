using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using CoreAPI;
using Exceptions;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [ExceptionFilter]
    [Authorize]
    public class ReportController : ApiController
    {
        private ApiResponse _apiResponse;
        /// <summary>
        /// Retorna un reporte de prueba
        /// </summary>
        /// <returns>PDF report</returns>
        [HttpGet]
        public HttpResponseMessage GetReport(string role)
        {
            try
            {
                var manager = new ReportManager();
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                
                response.Content = new StreamContent(manager.ReporteTest(role));

                response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
                response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = "Report-" + DateTime.Now.ToString("yyyy-MM-dd-HHmm") + ".pdf"
                };

                return response;
            }
            catch (BusinessException bex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError,
                    bex.ExceptionId + "-" + bex.AppMessage.Message);
            }
        }

        [HttpGet]
        public HttpResponseMessage GetReportAllGanancias()
        {
            try
            {
                var manager = new ReportManager();
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);

                response.Content = new StreamContent(manager.GetReportAllGanancias());

                response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
                response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = "ReportReportAllGanancias-" + DateTime.Now.ToString("yyyy-MM-dd-HHmm") + ".pdf"
                };

                return response;
            }
            catch (BusinessException bex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError,
                    bex.ExceptionId + "-" + bex.AppMessage.Message);
            }
        }

        [HttpGet]
        public HttpResponseMessage GetReportAllTransactionsTipo()
        {
            try
            {
                var manager = new ReportManager();
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);

                response.Content = new StreamContent(manager.GetReportAllTransactiontipo());

                response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
                response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = "ReportAllTransactionsTipo-" + DateTime.Now.ToString("yyyy-MM-dd-HHmm") + ".pdf" 
                };

                return response;
            }
            catch (BusinessException bex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError,
                    bex.ExceptionId + "-" + bex.AppMessage.Message);
            }
        }

        [HttpGet]
        public HttpResponseMessage GetReportAllTipoTarjeta()
        {
            try
            {
                var manager = new ReportManager();
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);

                response.Content = new StreamContent(manager.GetReportAllTipoTarjeta());

                response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
                response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = "ReportAllTipoTarjeta-" + DateTime.Now.ToString("yyyy-MM-dd-HHmm") + ".pdf"
                };

                return response;
            }
            catch (BusinessException bex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError,
                    bex.ExceptionId + "-" + bex.AppMessage.Message);
            }
        }

        [HttpGet]
        public IHttpActionResult GetReportAllGananciasAllTerminales()
        {
            try
            {
                var mng = new ReportManager();
                _apiResponse = new ApiResponse();
                _apiResponse.Message = "";
                _apiResponse.Data = mng.ObtenerTodasGanaciasGenerales();
                return Ok(_apiResponse);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        [HttpGet]
        public IHttpActionResult GetReportAllGananciasTipoTransaccion()
        {
            try
            {
                var mng = new ReportManager();
                _apiResponse = new ApiResponse();
                _apiResponse.Message = "";
                _apiResponse.Data = mng.ObtenerTodasTransaccionesPorTipo();
                return Ok(_apiResponse);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        [HttpGet]
        public IHttpActionResult GetReportAllTransactionsByTarjeta()
        {
            try
            {
                var mng = new ReportManager();
                _apiResponse = new ApiResponse();
                _apiResponse.Message = "";
                _apiResponse.Data = mng.ObtenerTodasPorTipoTarjeta();
                return Ok(_apiResponse);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        [HttpGet]
        public IHttpActionResult GetMovimientosUsuario(string email)
        {
            try
            {
                var mng = new ReportManager();
                _apiResponse = new ApiResponse();
                _apiResponse.Message = "";
                _apiResponse.Data = mng.ObtenerTodosMovimientos(email);
                return Ok(_apiResponse);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }
    }
}
