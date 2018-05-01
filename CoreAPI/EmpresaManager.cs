using System;
using System.Collections.Generic;
using DataAccess.Crud;
using Entities;
using Exceptions;
using Entities.Messaging;
using Resources.Messaging;

namespace CoreAPI
{
    public class EmpresaManager : BaseManager
    {
        private readonly EmpresaCrudFactory _crudFactory;

        public EmpresaManager()
        {
            _crudFactory = new EmpresaCrudFactory();
        }

        public List<Empresa> GetAllBussines()
        {
            try
            {
                return _crudFactory.RetrieveAll<Empresa>();
            }
            catch (Exception e)
            {
                ExceptionManager.GetInstance().Process(e);
            }
            return new List<Empresa>();
        }

        public List<Empresa> GetAllPendientes()
        {
            try
            {
                return _crudFactory.RetrieveAllPendientes<Empresa>();
            }
            catch (Exception e)
            {
                ExceptionManager.GetInstance().Process(e);
            }
            return new List<Empresa>();
        }

        public Empresa GetEmpresaById(Empresa empresa)
        {
            try
            {
                return _crudFactory.Retrieve<Empresa>(empresa);
            }
            catch (Exception e)
            {
                ExceptionManager.GetInstance().Process(e);
            }
            return new Empresa();
        }

        public void Create(Empresa empresa)
        {
            try
            {
                var empresaId = _crudFactory.Retrieve<Empresa>(empresa);

                if (empresaId != null)
                    throw new BusinessException(405);

                _crudFactory.Create(empresa);

                var email = new SendEmail();

                email.SendBussinesEmail(new EmailMessage
                {
                    To = empresa.EmailEncargado,
                    Message = empresa.NombreEmpresa
                });
            }
            catch (Exception e)
            {
                ExceptionManager.GetInstance().Process(e);
            }
        }

        public Empresa GetByEmpresario(Empresa empresa)
        {
            Empresa emp = null;
            try
            {
                emp = _crudFactory.RetrieveByEmpresario<Empresa>(empresa);

                if (emp == null)
                    throw new BusinessException(216);
            }
            catch (Exception e)
            {
                ExceptionManager.GetInstance().Process(e);
            }

            return emp;
        }

        public void Disable(Empresa empresa)
        {
            _crudFactory.Delete(empresa);
        }

        public void Aprobar(Empresa empresa)
        {
            _crudFactory.AprobarEmpresaStatement(empresa);

            var email = new SendEmail();

            if (empresa.Estado == "Inactivo")
            {
                email.SendRechazoEmpresaEmail(new EmailMessage
                {
                    To = empresa.EmailEncargado,
                    EmpresaNombre = empresa.NombreEmpresa,
                    Message = empresa.Rechazo
                });
            }

            else
            {
                email.SendAprobacionEmpresaEmail(new EmailMessage
                {
                    To = empresa.EmailEncargado,
                    EmpresaNombre = empresa.NombreEmpresa
                });
            }
        }

        public void Update(Empresa empresa)
        {
            _crudFactory.Update(empresa);
        }

        public Empresa RetrieveEmpresaByRuta(Ruta ruta)
        {
            Empresa empresaDb = null;
            try
            {
                empresaDb = _crudFactory.RetrieveEmpresaByRuta<Empresa>(ruta)[0];

            }
            catch (Exception e)
            {
                ExceptionManager.GetInstance().Process(e);
            }

            return empresaDb;
        }
    }
}
