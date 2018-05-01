using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Crud;
using Entities;
using Entities.Messaging;
using Exceptions;
using Resources.Messaging;

namespace CoreAPI
{
    public class ConvenioManager : BaseManager
    {
        private readonly ConvenioCrudFactory _crudFactory;

        public ConvenioManager()
        {
            _crudFactory = new ConvenioCrudFactory();
        }

        public List<Convenio> GetAll(int terminal = 0)
        {
            try
            {
                var allAgg = _crudFactory.RetrieveAll<Convenio>();
                return allAgg;
            }
            catch (Exception e)
            {
                ExceptionManager.GetInstance().Process(e);
            }
            return new List<Convenio>();
        }

        public void Create(Convenio convenio)
        {
            try
            {
                var convenioId = GetById(convenio);

                if(convenioId != null)
                    throw new BusinessException(303);

                _crudFactory.Create(convenio);
                _crudFactory.CreateRelationIds(convenio);
            }
            catch (Exception e)
            {
                ExceptionManager.GetInstance().Process(e);
            }
        }

        public void Update(Convenio cardType)
        {
            try
            {
                _crudFactory.Update(cardType);
            }
            catch (Exception e)
            {
                ExceptionManager.GetInstance().Process(e);
            }
        }

        public Convenio GetById(Convenio cardType)
        {
            try
            {
                return _crudFactory.Retrieve<Convenio>(cardType);
            }
            catch (Exception e)
            {
                ExceptionManager.GetInstance().Process(e);
            }
            return new Convenio();
        }

        public void Delete(Convenio cardType)
        {
            try
            {
                _crudFactory.Delete(cardType);
            }
            catch (Exception e)
            {
                ExceptionManager.GetInstance().Process(e);
            }
        }

        public List<Convenio> GetByTerminalId(Convenio convenio)
        {
            try
            {
                return _crudFactory.RetrieveByTerminalId<Convenio>(convenio);
            }
            catch (Exception e)
            {
                ExceptionManager.GetInstance().Process(e);
            }
            return new List<Convenio>();
        }

        public void SolicitudTarjetaConvenio(Solicitud solicitud)
        {
            try
            {
                var solicitudManager = new SolicitudManager();
                var convenio = GetById(solicitud.Convenio);

                if (convenio == null)
                    throw new BusinessException(308);

                solicitud.SolicitudId = Guid.NewGuid().ToString();

                solicitud.Emails.ForEach(e => { solicitud.ListEmails += e+","; });

                solicitudManager.Create(solicitud);

                var sendMails = new SendEmail();

                sendMails.SendNotificationMessage( new EmailMessage
                {
                    To = convenio.EmailEncargado,
                    Url = $"SolicitudId={solicitud.SolicitudId}",
                    UserName = solicitud.EmailSolicitante
                });

            }
            catch (Exception e)
            {
                ExceptionManager.GetInstance().Process(e);
            }
        }

    }
}
