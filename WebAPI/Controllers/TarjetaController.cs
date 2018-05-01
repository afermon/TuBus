using System;
using System.Collections.Generic;
using System.Web.Http;
using CoreAPI;
using Entities;
using Exceptions;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class TarjetaController : ApiController
    {
        private ApiResponse _apiResponse;

        /// <summary>
        /// Initialize Card
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult InitializeCard(Tarjeta card)
        {
            try
            {
                var mng = new TarjetaManager();
                mng.InitializeCard(card);
                _apiResponse = new ApiResponse();
                _apiResponse.Message = "Se ha enviado un correo a " + card.Usuario.Email + " con el link de activación de la tarjeta";
                return Ok(_apiResponse);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }
        
        /// <summary>
        /// Get Cards by Terminal
        /// </summary>
        /// <param name="terminal"></param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult ListCardsByTerm(int terminal)
        {
            try
            {
                var mng = new TarjetaManager();
                _apiResponse = new ApiResponse();
                _apiResponse.Data = mng.GetTarjetasByTerminalId(terminal);
                _apiResponse.Message = "Execution Success";
                return Ok(_apiResponse);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        /// <summary>
        /// Return Cards by User
        /// </summary>
        /// <param name="userMail"></param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult ListCardsByUser(string userMail)
        {
            try
            {
                var mng = new TarjetaManager();
                _apiResponse = new ApiResponse();
                _apiResponse.Data = mng.GeTarjetasByUser(userMail);
                _apiResponse.Message = "Execution Success";
                return Ok(_apiResponse);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        [HttpGet]
        public IHttpActionResult GetCardByUniqueCode(string uniqueCode)
        {
            try
            {
                var mng = new TarjetaManager();
                _apiResponse = new ApiResponse();
                _apiResponse.Data = mng.GeTarjetaByUniquecode(uniqueCode);
                _apiResponse.Message = "Execution Success";
                return Ok(_apiResponse);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        /// <summary>
        /// Get all cards 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous] // Usado por el servico
        public IHttpActionResult GetAllCards()
        {
            try
            {
                var mng = new TarjetaManager();
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

        /// <summary>
        /// Eliminar tarjeta
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        [HttpDelete]
        public IHttpActionResult DeleteCard(Tarjeta card)
        {
            try
            {
                var mng = new TarjetaManager();
                _apiResponse = new ApiResponse();
                 mng.Delete(card);
                _apiResponse.Message = "Tarjeta eliminada";
                return Ok(_apiResponse);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        /// <summary>
        /// Obtener tarjetas de reposicion
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetRepositionCards()
        {
            try
            {
                var mng = new TarjetaManager();
                _apiResponse = new ApiResponse();
                _apiResponse.Data = mng.GetRequestedRepositionCards();
                _apiResponse.Message = "Execution Success";
                return Ok(_apiResponse);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        /// <summary>
        /// Obtener tarjetas por terminal id
        /// </summary>
        /// <param name="terminal"></param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetRepositionCardsByTerminal(int terminal)
        {
            try
            {
                var mng = new TarjetaManager();
                _apiResponse = new ApiResponse();
                _apiResponse.Data = mng.GetRequestedRepositionCardsByTerm(terminal);
                _apiResponse.Message = "Execution Success";
                return Ok(_apiResponse);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        /// <summary>
        /// Actualizar estado
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        [HttpPut]
        public IHttpActionResult UpdateEstadoCard(Tarjeta card)
        {
            try
            {
                var mng = new TarjetaManager();
                _apiResponse = new ApiResponse();
                mng.UpdateEstadoTarjeta(card);
                _apiResponse.Message = "Estado Actualizado";
                return Ok(_apiResponse);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        /// <summary>
        /// Elimina la tarjeta vieja y crea la nueva
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        [HttpDelete]
        public IHttpActionResult AprobarReposicion(Tarjeta card)
        {
            try
            {
                var mng = new TarjetaManager();
                mng.AprobarReposicion(card);
                _apiResponse = new ApiResponse();
                _apiResponse.Message = "Tarjeta repuesta";
                return Ok(_apiResponse);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

    }
}
