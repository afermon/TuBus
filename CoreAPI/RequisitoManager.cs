using DataAccess.Crud;
using Entities;
using Exceptions;
using System;
using System.Collections.Generic;

namespace CoreAPI
{
    public class RequisitoManager : BaseManager
    {
        private readonly RequisitoCrudFactory _crudRequisito;

        public RequisitoManager()
        {
            _crudRequisito = new RequisitoCrudFactory();
        }

        public void Create(Role rol)
        {
            try
            {
                _crudRequisito.Create(rol);
            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
            }
        }

        public List<Requisito> RetrieveAllById(Requisito requisito)
        {
            return _crudRequisito.RetriveAllByPlacaStatement<Requisito>(requisito);
        }

        public void Update(Requisito requisito)
        {
            try
            {
                _crudRequisito.Update(requisito);
            }
            catch (Exception e)
            {
                ExceptionManager.GetInstance().Process(e);
            }
        }
    }
}