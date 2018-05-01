using System;
using System.Collections.Generic;
using DataAccess.Crud;
using Entities;
using Exceptions;

namespace CoreAPI
{
    public class ConfiguracionManager : BaseManager
    {
        private readonly ConfiguracionCrudFactory _crudConfiguracion;
        private readonly ConfiguracionTerminalCrudFactory _crudConfiguracionTerminal;
        private static List<ConfiguracionItem> _configuraciones;

        public ConfiguracionManager()
        {
            _crudConfiguracion = new ConfiguracionCrudFactory();
            _crudConfiguracionTerminal = new ConfiguracionTerminalCrudFactory();
            _configuraciones = _crudConfiguracion.RetrieveAll<ConfiguracionItem>();
        }

        public ConfiguracionItem RetrieveConfiguracionById(string id)
        {
            ConfiguracionItem config = null; 
            try
            {
                config = _configuraciones.Find(x => x.Id == id);
                if (config == null) throw new BusinessException(201);
            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
            }
            return config;
        }

        public Configuracion RetrieveConfiguracion()
        {
            Configuracion config = null;
            try
            {
                config = new Configuracion
                {
                    DiasExpiracionContrasena = (int)_configuraciones.Find(x => x.Id == "EXPIRACION_CONTRASENA").NumberValue,
                    CantCaracteresContrasena = (int)_configuraciones.Find(x => x.Id == "CANT_CARACTERES_CONTRASENNA").NumberValue,
                    CantContrasenasAnteriores = (int)_configuraciones.Find(x => x.Id == "CANT_CONTRASENA_ANTERIORES").NumberValue
                };
            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
            }
            return config;
        }

        public void UpdateConfiguracion(Configuracion config)
        {
            try
            {
                _crudConfiguracion.Update(new ConfiguracionItem{ Id = "EXPIRACION_CONTRASENA", NumberValue = config.DiasExpiracionContrasena });
                _crudConfiguracion.Update(new ConfiguracionItem { Id = "CANT_CARACTERES_CONTRASENNA", NumberValue = config.CantCaracteresContrasena });
                _crudConfiguracion.Update(new ConfiguracionItem { Id = "CANT_CONTRASENA_ANTERIORES", NumberValue = config.CantContrasenasAnteriores });
            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
            }
        }

        public void CreateConfiguracionTerminal(int terminalId)
        {
            try
            {
                var config = _crudConfiguracionTerminal.Retrieve<ConfiguracionTerminal>(new ConfiguracionTerminal {TerminalId = terminalId});
                if (config != null) throw new BusinessException(202);

                config = new ConfiguracionTerminal
                {
                    TerminalId = terminalId,
                    CantidadQuejasSancion = 5,
                    CostoParqueoDia = 4000,
                    CostoParqueoHora = 500,
                    MontoInicialTarjeta = 3000
                };

                _crudConfiguracionTerminal.Create(config);
            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
            }
        }

        public ConfiguracionTerminal RetrieveConfiguracionTerminal(int terminalId)
        {
            ConfiguracionTerminal config = null;
            try
            {
                config = _crudConfiguracionTerminal.Retrieve<ConfiguracionTerminal>(new ConfiguracionTerminal { TerminalId = terminalId });
                if (config == null) throw new BusinessException(203);
            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
            }
            return config;
        }

        public void UpdateConfiguracionTerminal(ConfiguracionTerminal config)
        {
            try
            {
                var c = _crudConfiguracionTerminal.Retrieve<ConfiguracionTerminal>(config);
                if (c == null) throw new BusinessException(203);

                _crudConfiguracionTerminal.Update(config);
            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
            }
        }

        public void DeleteConfiguracionTerminal(ConfiguracionTerminal config)
        {
            try
            {
                var c = _crudConfiguracionTerminal.Retrieve<ConfiguracionTerminal>(config);
                if (c == null) throw new BusinessException(203);

                _crudConfiguracionTerminal.Delete(config);
            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
            }
            
        }
    }
}