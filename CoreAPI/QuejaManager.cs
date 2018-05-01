using DataAccess.Crud;
using Entities;
using Exceptions;
using System;
using System.Collections.Generic;

namespace CoreAPI
{
    public class QuejaManager : BaseManager
    {
        private readonly QuejaCrudFactory _crudQueja;
                
        public QuejaManager()
        {
            _crudQueja = new QuejaCrudFactory();
        }

        public void Create(Queja queja)
        {

            try
            {


                _crudQueja.Create(queja);

               
                var configManager = new ConfiguracionManager();
                var empresaManager = new EmpresaManager();
                var sancionManager = new SancionManager();
                var rutaManager = new RutaManager();
                var ruta = new Ruta();
                var empresa = new Empresa();
                var configuracionTerminal = new ConfiguracionTerminal();

                //Obtengo la empresa a la que se le agregó la queja
                ruta.Id = queja.Ruta;
                empresa = empresaManager.RetrieveEmpresaByRuta(ruta);

                //Obtengo el Id de la terminal
                ruta = rutaManager.Retrieve(ruta);
                var terminalId = ruta.TerminalId;

                //Obtengo la configuración de terminal
                configuracionTerminal = configManager.RetrieveConfiguracionTerminal(terminalId);

                //Obtengo la cantidad de quejas para una sanción 
                var cantidadSancion = configuracionTerminal.CantidadQuejasSancion;

                //Obtengo la cantidad de quejas hechas a una ruta
                var cantidadQuejas = _crudQueja.RetriveQuejasActivasByRutaStatement<Queja>(queja.Ruta).Count;

                //Valido si hay quejas suficientes para una sanción
                if (cantidadSancion <= cantidadQuejas) {

                    var sancion = new Sancion();

                    sancion.Descripcion = "Sanción generada automaticamente por el sistema";
                    sancion.Multa = 3000;
                    sancion.Estado = "Activo";
                    sancion.TerminalId = ruta.TerminalId; 
                    sancion.Fecha = DateTime.Now;
                    sancion.Suspencion = "Activo";
                    sancion.FechaReactivacion = DateTime.Now.AddDays(7);
                    sancion.Empresa = empresa.CedulaJuridica;

                    sancionManager.Create(sancion);
                }
                            
            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
            }
            
                       
        }

        public List<Queja> RetrieveAll()
        {
            return _crudQueja.RetrieveAll<Queja>();
        }

        public List<Queja> RetrieveQuejasActivas()
        {
            return _crudQueja.RetrieveQuejasActivas<Queja>();
        }

        public Queja RetrieveById(Queja queja)
        {
            return _crudQueja.Retrieve<Queja>(queja);
        }

        public void Update(Queja queja)
        {
            throw new NotImplementedException();
        }

        public void Delete(Queja queja)
        {
            _crudQueja.Delete(queja);
        }
    }
}
