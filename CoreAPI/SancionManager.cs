using DataAccess.Crud;
using Entities;
using Exceptions;
using System;
using System.Collections.Generic;

namespace CoreAPI
{
    public class SancionManager : BaseManager
    {
        private readonly SancionCrudFactory _crudSancion;

        public SancionManager()
        {
            _crudSancion = new SancionCrudFactory();
        }

        public void Create(Sancion sancion)
        {
            try
            {

                _crudSancion.Create(sancion);
                               
            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
            }
        }

        public List<Sancion> RetrieveAll()
        {
            return _crudSancion.RetrieveAll<Sancion>();
        }

        public List<Sancion> RetrieveAllActivas()
        {
            return _crudSancion.RetrieveAllActivas<Sancion>();
        }

        public void Update(Sancion sancion)
        {

            try
            {
                _crudSancion.Update(sancion);

            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
            }

            
        }

        public void Delete(Sancion sancion)
        {
            try
            {
                _crudSancion.Delete(sancion);

            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
            }

        }

    }
}
