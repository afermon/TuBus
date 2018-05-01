using DataAccess.Crud;
using Entities;
using Exceptions;
using System;
using System.Collections.Generic;

namespace CoreAPI
{
    public class MultaManager
    {
        private readonly MultaCrudFactory _crudMulta;

        public MultaManager()
        {
            _crudMulta = new MultaCrudFactory();
        }

        public void Create(Multa multa)
        {
            try
            {
                _crudMulta.Create(multa);
            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
            }
        }

        public List<Multa> RetrieveAll()
        {
            return _crudMulta.RetrieveAll<Multa>();
        }

        public void Delete(Multa multa)
        {
            _crudMulta.Delete(multa);
        }

        public void Update(Multa multa)
        {
            _crudMulta.Update(multa);
        }
    }
}
