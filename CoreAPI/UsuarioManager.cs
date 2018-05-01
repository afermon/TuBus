using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using DataAccess.Crud;
using Entities;
using Entities.Messaging;
using Exceptions;
using Resources.Messaging;

namespace CoreAPI
{
    public class UsuarioManager : BaseManager
    {
        private readonly UsuarioCrudFactory _crudUsuario;
        private readonly HistorialContrasenaCrudFactory _crudContrasena;

        private const int PasswordIterations = 16;
        private const int PasswordSaltBytes = 16;
        private const int PasswordHashBytes = 16;

        public UsuarioManager()
        {
            _crudUsuario = new UsuarioCrudFactory();
            _crudContrasena = new HistorialContrasenaCrudFactory();
        }

        public void Create(Usuario usuario)
        {
            try
            {
                var userDB = _crudUsuario.Retrieve<Usuario>(usuario);
                if (userDB != null)
                    throw new BusinessException(207);

                usuario.PasswordSalt = GenerateSalt();
                usuario.PasswordHash = GenerateHash(usuario.Password, usuario.PasswordSalt);
                usuario.PasswordLastSet = DateTime.Now;

                _crudUsuario.Create(usuario);
                //Record password history
                _crudContrasena.Create(new HistorialContrasena { Email = usuario.Email, PasswordHash = usuario.PasswordHash, Fecha = usuario.PasswordLastSet });
            }
            catch (Exception e)
            {
                ExceptionManager.GetInstance().Process(e);
            }
        }

        public void Delete(Usuario usuario)
        {
            try
            {
               _crudUsuario.Delete(usuario);
            }
            catch (Exception e)
            {
                ExceptionManager.GetInstance().Process(e);
            }
        }

        public void Update(Usuario usuario)
        {
            try
            {
                var user = Retrieve(usuario);
                if (user == null)
                    throw new BusinessException(204);

                if (usuario.Estado == null) usuario.Estado = user.Estado;

                _crudUsuario.Update(usuario);
            }
            catch (Exception e)
            {
                ExceptionManager.GetInstance().Process(e);
            }
        }

        public void UpdatePassword(Usuario usuario, bool expired = false)
        {
            try
            {
                var userDb = _crudUsuario.RetrieveAuth<Usuario>(usuario);
                if (userDb == null)
                    throw new BusinessException(204);

                var mngConfig = new ConfiguracionManager();
                var config = mngConfig.RetrieveConfiguracion();

                // Validate password length
                if (usuario.Password.Length < config.CantCaracteresContrasena)
                    throw new BusinessException(206);

                usuario.PasswordSalt = userDb.PasswordSalt;
                usuario.PasswordHash = GenerateHash(usuario.Password, usuario.PasswordSalt);
                usuario.PasswordLastSet = DateTime.Now;

                // Validate password has not been used before.
                var newPassword = new HistorialContrasena
                {
                    Fecha = usuario.PasswordLastSet,
                    Email = usuario.Email,
                    PasswordHash = usuario.PasswordHash,
                    Count = config.CantContrasenasAnteriores
                };

                var historial = _crudContrasena.Retrieve<HistorialContrasena>(newPassword);

                if (historial != null)
                    throw new BusinessException(205);

                _crudUsuario.UpdatePassword(usuario);

                // Record password has in history
                _crudContrasena.Create(newPassword);

                //Clean up password history
                _crudContrasena.Delete(newPassword);
            }
            catch (Exception e)
            {
                ExceptionManager.GetInstance().Process(e);
            }
        }

        public Usuario Retrieve(Usuario usuario)
        {
            Usuario usuarioDb = null;
            try
            {
                usuarioDb = _crudUsuario.Retrieve<Usuario>(usuario);

                if (usuarioDb == null)
                    throw new BusinessException(204);

                var mngConfig = new ConfiguracionManager();
                var config = mngConfig.RetrieveConfiguracion();
                usuarioDb.PasswordExpiration = usuarioDb.PasswordLastSet.AddDays(config.DiasExpiracionContrasena);
            }
            catch (Exception e)
            {
                ExceptionManager.GetInstance().Process(e);
            }

            return usuarioDb;
        }

        public List<Usuario> RetrieveAll()
        {
            return _crudUsuario.RetrieveAll<Usuario>();
        }

        public Usuario ValidateUser(Usuario usuario)
        {
            Usuario usuarioDb = null;
            try
            {
                usuarioDb = _crudUsuario.RetrieveAuth<Usuario>(usuario);

                if (usuarioDb == null) // Does not exist
                    throw new BusinessException(208);

                if (!usuarioDb.Estado.Equals("Activo")) // Not Active
                    throw new BusinessException(211);

                var mngConfig = new ConfiguracionManager();
                var config = mngConfig.RetrieveConfiguracion();
           
                if (usuarioDb.PasswordLastSet.AddDays(config.DiasExpiracionContrasena) < DateTime.Today)
                    throw new BusinessException(209); //Expired password

                usuario.PasswordHash = GenerateHash(usuario.Password, usuarioDb.PasswordSalt);

                if (!usuario.PasswordHash.Equals(usuarioDb.PasswordHash)) // Wrong password
                    throw new BusinessException(208);

                // clean the object
                usuarioDb.PasswordHash = null;
                usuarioDb.PasswordSalt = null;
                usuarioDb.ResetToken = null;
            }
            catch (Exception e)
            {
                ExceptionManager.GetInstance().Process(e);
            }

            return usuarioDb; // Ok
        }

        public void GetResetToken(Usuario usuario)
        {
            try
            {
                var usuarioDb = _crudUsuario.RetrieveAuth<Usuario>(usuario);

                if (usuarioDb == null) // Does not exist
                    throw new BusinessException(208);

                usuarioDb.ResetToken = Guid.NewGuid().ToString();

                _crudUsuario.UpdateResetToken(usuarioDb);

                var email = new SendEmail();

                email.SendResetEmail(new EmailMessage
                {
                    To = usuarioDb.Email,
                    Url = usuarioDb.ResetToken,
                    UserName = usuarioDb.Nombre
                });

            }
            catch (Exception e)
            {
                ExceptionManager.GetInstance().Process(e);
            }
        }

        public void ResetPassword(Usuario usuario)
        {
            try
            {
                var usuarioDb = _crudUsuario.RetrieveAuth<Usuario>(usuario);

                if (usuarioDb == null) // Does not exist
                    throw new BusinessException(208);

                if(usuarioDb.ResetToken == null || !usuarioDb.ResetToken.Equals(usuario.ResetToken))
                    throw new BusinessException(210); //Invalid Reset token

                UpdatePassword(usuario);

            }
            catch (Exception e)
            {
                ExceptionManager.GetInstance().Process(e);
            }
        }

        private string GenerateSalt()
        {
            var bytes = new byte[PasswordSaltBytes];
            using (var rng = new RNGCryptoServiceProvider())
            rng.GetBytes(bytes);
            
            return Convert.ToBase64String(bytes);
        }

        private string GenerateHash(string password, string salt)
        {
            using (var deriveBytes = new Rfc2898DeriveBytes(password, Convert.FromBase64String(salt), PasswordIterations))
                return Convert.ToBase64String(deriveBytes.GetBytes(PasswordHashBytes));
        }
    }
}