using System;
using System.Linq;
using System.Security.Claims;
using System.Web.Http;
using CoreAPI;
using Entities;
using Exceptions;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Authorize]
    [ExceptionFilter]
    public class EmpresaController : ApiController
    {
        private ApiResponse _apiResponse = new ApiResponse();

        /// <summary>Obtine todas las empresas</summary>
        /// <returns>Lista de empresas</returns>
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            try
            {
                var manager = new EmpresaManager();

                _apiResponse = new ApiResponse
                {
                    Data = manager.GetAllBussines(),
                    Message = "Execution Success"
                };

                return Ok(_apiResponse);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }


        /// <summary>Obtiene todas las empresas que tiene estado Pendiente</summary>
        /// <returns>Lista de empresas</returns>
        [HttpGet]
        public IHttpActionResult GetAllPendientes()
        {
            try
            {
                var manager = new EmpresaManager();

                _apiResponse = new ApiResponse
                {
                    Data = manager.GetAllPendientes(),
                    Message = "Execution Success"
                };

                return Ok(_apiResponse);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        ///<summary>Registra una nueva empresa en el sistema</summary>
        /// <param name="empresa">Objeto Empresa</param>
        [HttpPost]
        [AllowAnonymous]
        //[ClaimsAuthorization(ClaimType = "action", ClaimValue = "empresa-registrarEmpresa")]
        public IHttpActionResult RegistrarEmpresa(Empresa empresa)
        {
            try
            {
                var manager = new EmpresaManager();
                var usuarioManager = new UsuarioManager();

                usuarioManager.Create(empresa.Usuario);
                manager.Create(empresa);

                _apiResponse.Message = "Empresa registrada.";

                return Ok(_apiResponse);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        /// <summary>
        /// Retorna la empresa del empresario actualmente logueado.
        /// </summary>
        /// <returns>Role</returns>
        [HttpGet]
        public IHttpActionResult Current()
        {
            var identity = User.Identity as ClaimsIdentity;
            var email = identity.Claims.Where(c => c.Type == "Email").Select(c => c.Value).SingleOrDefault();

            var mng = new EmpresaManager();

            _apiResponse.Data = mng.GetByEmpresario(new Empresa { EmailEncargado = email });

            return Ok(_apiResponse);
        }

        ///<summary>Actualiza el estado de una empresa</summary>
        /// <param name="empresa">Objeto empresa</param>
        [HttpPut]
        //[ClaimsAuthorization(ClaimType = "action", ClaimValue = "empresa-desactivarEmpresa")]
        public IHttpActionResult DesactivarEmpresa(Empresa empresa)
        {
            try
            {
                var manager = new EmpresaManager();

                manager.Disable(empresa);

                var estado = empresa.Estado == "Activo" ? "activada" : "desactivada";

                _apiResponse = new ApiResponse
                {
                    Message = "Empresa " + estado + "."
                };

                return Ok(_apiResponse);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }


        ///<summary>Aprueba o rechaza una solicitud de empresa</summary>
        /// <param name="empresa">Objeto empresa</param>
        [HttpPut]
        //[ClaimsAuthorization(ClaimType = "action", ClaimValue = "empresa-aprobarEmpresa")]
        public IHttpActionResult AprobarEmpresa(Empresa empresa)
        {
            try
            {
                var manager = new EmpresaManager();
                var usuarioManager = new UsuarioManager();

                manager.Aprobar(empresa);

                var usuarioEncargado = usuarioManager.Retrieve(new Usuario { Email = empresa.EmailEncargado });

                if (usuarioEncargado != null) {
                    usuarioEncargado.Estado = empresa.Estado;
                    usuarioEncargado.TerminalId = 0;
                    usuarioManager.Update(usuarioEncargado);
                }

                var estado = empresa.Estado == "Activo" ? "aprobada" : "rechazada";

                _apiResponse = new ApiResponse
                {
                    Message = string.Format("{0} {1}.", "Solicitud de la empresa", estado)
                };

                return Ok(_apiResponse);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }


        ///<summary>Modifica los datos de una empresa</summary>
        /// <param name="empresa">Objeto empresa</param>
        [HttpPut]
        //[ClaimsAuthorization(ClaimType = "action", ClaimValue = "empresa-updateEmpresa")]
        public IHttpActionResult UpdateEmpresa(Empresa empresa)
        {
            try
            {
                var manager = new EmpresaManager();

                manager.Update(empresa);

                _apiResponse = new ApiResponse
                {
                    Message = "Empresa actualizada correctamente."
                };

                return Ok(_apiResponse);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        [HttpGet]
        public IHttpActionResult RealizarCobro(int empresaId)
        {

            var mng = new PagosPendientesManager();
            mng.Create(empresaId);
            _apiResponse.Message = "Proceso realizado";

            return Ok(_apiResponse);
        }

        [HttpGet]
        public IHttpActionResult ObtenerPagos(int empresaId)
        {

            var mng = new PagosPendientesManager();
            _apiResponse.Data = mng.ObtenerPagosPorEmpresa(empresaId);
            _apiResponse.Message = "Proceso realizado";

            return Ok(_apiResponse);
        }

        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult GetAllNombreEmpresas()
        {

            var manager = new TarifaManager();

            _apiResponse = new ApiResponse
            {
                Data = manager.RetrieveAll().GroupBy(x => x.Operator).Select(group => group.First()),
                Message = "Proceso realizado"
            };

            return Ok(_apiResponse);
        }
    }
}