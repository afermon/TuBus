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
    public class UsuarioController : ApiController
    {
        ApiResponse apiResp = new ApiResponse();

        /// <summary>
        /// Verifica si el token esta activo.
        /// </summary>
        [HttpGet]
        public IHttpActionResult CheckToken()
        {
            apiResp = new ApiResponse();
            apiResp.Message = "Token valido";

            return Ok(apiResp);
        }

        /// <summary>
        /// Retorna el usuario actualmente logueado.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult Current()
        {
            apiResp = new ApiResponse();
            var mng = new UsuarioManager();
            try
            {
                var identity = User.Identity as ClaimsIdentity;

                var email = identity.Claims.Where(c => c.Type == "Email").Select(c => c.Value).SingleOrDefault();

                var usuario = mng.Retrieve(new Usuario {Email = email});

                apiResp.Data = usuario;
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }

            return Ok(apiResp);
        }

        /// <summary>
        /// Actualiza el usuario actualmente logueado.
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        public IHttpActionResult Current(Usuario usuario)
        {
            apiResp = new ApiResponse();
            var mng = new UsuarioManager();
            try
            {
                var identity = User.Identity as ClaimsIdentity;
                usuario.Email = identity.Claims.Where(c => c.Type == "Email").Select(c => c.Value).SingleOrDefault();

                mng.Update(usuario);
                apiResp.Message = "Actualizado correctamente.";
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }

            return Ok(apiResp);
        }

        /// <summary>
        /// Actualiza el usuario actualmente logueado.
        /// </summary>
        /// <param name="usuario">Usuario</param>
        /// <returns></returns>
        [HttpPut]
        public IHttpActionResult UpdateContrasena(Usuario usuario)
        {
            apiResp = new ApiResponse();
            var mng = new UsuarioManager();
            try
            {
                var identity = User.Identity as ClaimsIdentity;
                usuario.Email = identity.Claims.Where(c => c.Type == "Email").Select(c => c.Value).SingleOrDefault();

                mng.UpdatePassword(usuario);

                apiResp.Message = "Actualizado correctamente.";
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }

            return Ok(apiResp);
        }

       /// <summary>
        /// Inicia el proceso de restablecimiento de contrasena
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public IHttpActionResult ResetToken(Usuario user)
        {
            apiResp = new ApiResponse();
            var mng = new UsuarioManager();
            try
            {
                mng.GetResetToken(user);
                apiResp.Message = "Si existe un usuario asociado al correo indicado, el mensaje fue enviado.";
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }

            return Ok(apiResp);
        }

        /// <summary>
        /// Reestablece la contraseña del usuario.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public IHttpActionResult ResetPassword(Usuario user)
        {
            apiResp = new ApiResponse();
            var mng = new UsuarioManager();
            try
            {
                mng.ResetPassword(user);
                apiResp.Message = "Contraseña actualizada correctamente. Por favor intenta iniciar sesión con las nuevas credenciales.";
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }

            return Ok(apiResp);
        }

        /// <summary>
        /// Get User By Email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public IHttpActionResult GetUserByEmail(string email)
        {
            apiResp = new ApiResponse();
            var mng = new UsuarioManager();
            try
            {
                apiResp.Data = mng.Retrieve(new Usuario{Email = email});
                apiResp.Message = "Execution Success";
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }

            return Ok(apiResp);
        }
        /// <summary>
        /// Retorna todos los usuario en el sistema
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            apiResp = new ApiResponse();
            var mng = new UsuarioManager();
            try
            {
                apiResp.Data = mng.RetrieveAll();
                apiResp.Message = "Execution Success";
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }

            return Ok(apiResp);
        }

        ///<summary>Registra un nuevo usuario</summary>
        ///<param name="usuario">Usuario</param>
        [HttpPost]
        [AllowAnonymous]
        public IHttpActionResult Create(Usuario usuario)
        {
            try
            {
                var mng = new UsuarioManager();
                mng.Create(usuario);
                apiResp.Message = "Usuario registrado";

                return Ok(apiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        ///<summary>Actualiza la información de un usuario</summary>
        ///<param name="usuario">Usuario</param>
        [HttpPut]
        public IHttpActionResult Update(Usuario usuario)
        {
            try
            {
                var mng = new UsuarioManager();
                mng.Update(usuario);
                apiResp.Message = "Usuario actualizado";

                return Ok(apiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        ///<summary>Desactiva un usuario</summary>
        ///<param name="usuario">Usuario</param>
        [HttpDelete]
        public IHttpActionResult Delete(Usuario usuario)
        {
            try
            {
                var mng = new UsuarioManager();
                mng.Delete(usuario);
                apiResp.Message = "Usuario descativado";

                return Ok(apiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }
    }
}
