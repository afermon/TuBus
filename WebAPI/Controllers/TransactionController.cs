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
    public class TransactionController : ApiController
    {
        private ApiResponse _apiResponse;

        /// <summary>
        /// Get All transactions
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetAllTransactions()
        {
            try
            {
                var mng = new TransactionManager();
                _apiResponse = new ApiResponse();
                _apiResponse.Data = mng.GetAllTransactions();
                _apiResponse.Message = "Execution Success";
                return Ok(_apiResponse);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }


        /// <summary>
        /// Get Transactions by user mail
        /// </summary>
        /// <param name="userMail"></param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult ListTransactionsByUser(string userMail)
        {
            try
            {
                var mng = new TransactionManager();
                _apiResponse = new ApiResponse();
                _apiResponse.Data = mng.GetTrtansactionsByUser(new Transaccion{Usuario = new Usuario{Email = userMail}});
                _apiResponse.Message = "Execution Success";
                return Ok(_apiResponse);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        /// <summary>
        /// Get Transactions by Terminal
        /// </summary>
        /// <param name="terminal"></param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult ListTransactionsByTerm(int terminal)
        {
            try
            {
                var mng = new TransactionManager();
                _apiResponse = new ApiResponse();
                _apiResponse.Data = mng.GetTransactionsByTerminal(new Transaccion{Terminal = new Terminal{Id = terminal}});
                _apiResponse.Message = "Execution Success";
                return Ok(_apiResponse);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        /// <summary>
        /// Create transaction
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="isFromBus"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult CreateTransaction(Transaccion transaction, bool isFromBus = false)
        {
            try
            {
                var mng = new TransactionManager();
                _apiResponse = new ApiResponse();
                mng.CreateUserTransaction(transaction, isFromBus);
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
