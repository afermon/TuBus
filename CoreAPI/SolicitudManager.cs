using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Crud;
using Entities;
using Exceptions;

namespace CoreAPI
{
    public class SolicitudManager
    {
        private SolicitudCrudFactory _crudFactory;

        public SolicitudManager()
        {
            _crudFactory = new SolicitudCrudFactory();
        }

        public void Create(Solicitud solicitud)
        {
            try
            {
                _crudFactory.Create(solicitud);
            }
            catch (Exception e)
            {
                ExceptionManager.GetInstance().Process(e);
            }
        }

        public List<Solicitud> GetAll(int terminal = 0)
        {
            try
            {
                var solicitudes = _crudFactory.RetrieveAll<Solicitud>();
                var terminalManger = new TerminalManager();
                var convenioManager = new ConvenioManager();
                solicitudes.ForEach(s =>
                {
                    s.Terminal = terminalManger.RetrieveById(s.Terminal);
                    s.Convenio = convenioManager.GetById(s.Convenio);
                });

                if(terminal != 0)
                    return solicitudes.Where(s => s.Terminal.Id == terminal).ToList();

                return solicitudes;
            }
            catch (Exception e)
            {
                ExceptionManager.GetInstance().Process(e);
            }
            return new List<Solicitud>();
        }

        public Solicitud GetSolicitudById(Solicitud solicitud)
        {
            try
            {
                var solicitudResponse =  _crudFactory.Retrieve<Solicitud>(solicitud);
                var terminalManger = new TerminalManager();
                var convenioManager = new ConvenioManager();

                if (solicitudResponse == null) return null;

                solicitudResponse.Terminal = terminalManger.RetrieveById(solicitudResponse.Terminal);
                solicitudResponse.Convenio = convenioManager.GetById(solicitudResponse.Convenio);

                return solicitudResponse;
            }
            catch (Exception e)
            {
                ExceptionManager.GetInstance().Process(e);
            }
            return new Solicitud();
        }

        public void Update(Solicitud solicitud)
        {
            try
            {
                _crudFactory.Update(solicitud);
            }
            catch (Exception e)
            {
                ExceptionManager.GetInstance().Process(e);
            }
        }

        public void ProcesarSolicitud(string solicitud, bool isFromAdmin, bool isDenied)
        {
            try
            {
                var currentSolicitud = GetSolicitudById(new Solicitud{ SolicitudId = solicitud });

                if(currentSolicitud == null)
                    throw new BusinessException(308);

                if(currentSolicitud.Estado.Equals("Aceptado") || currentSolicitud.Estado.Equals("Rechazado"))
                     throw new BusinessException(309);

                if (isDenied)
                {
                    currentSolicitud.Estado = "Rechazado";
                    Update(currentSolicitud);
                    return;
                }

                if (currentSolicitud.Estado.Equals("Pendiente administrador") && !isFromAdmin)
                    throw new BusinessException(310);

                if (currentSolicitud.Estado.Equals("Pendiente representante") && isFromAdmin)
                    throw new BusinessException(311);
                
                if (currentSolicitud.Estado.Equals("Pendiente administrador") && isFromAdmin)
                {
                    currentSolicitud.Estado = "Aceptado";
                    GenerarTarjetas(currentSolicitud);
                    Update(currentSolicitud);
                }
                else if (currentSolicitud.Estado.Equals("Pendiente representante") && !isFromAdmin)
                {
                    currentSolicitud.Estado = "Aceptado";
                    GenerarTarjetas(currentSolicitud);
                    Update(currentSolicitud);
                }
                else if (isFromAdmin)
                {
                    currentSolicitud.Estado = "Pendiente representante";
                    Update(currentSolicitud);
                }
                else
                {
                    currentSolicitud.Estado = "Pendiente administrador";
                    Update(currentSolicitud);
                }
            }
            catch (Exception e)
            {
                ExceptionManager.GetInstance().Process(e);
            }
        }

        private void GenerarTarjetas(Solicitud currentSolicitud)
        {
            var allEmails = currentSolicitud.ListEmails.Split(',').ToList();
            var tarjetaManager = new TarjetaManager();
            allEmails.ForEach(mail =>
            {
                if (!string.IsNullOrEmpty(mail))
                {
                    tarjetaManager.InitializeCard(new Tarjeta
                    {
                        Convenio = currentSolicitud.Convenio,
                        Terminal = currentSolicitud.Terminal,
                        Usuario = new Usuario
                        {
                            Nombre = "Usuario del convenio: " + currentSolicitud.Convenio.NombreInstitucion,
                            Email = mail
                        },
                        TipoTarjeta = new TipoTarjeta { TipoTarjetaId = 1}
                    });
                }
            });


        }

    }
}
