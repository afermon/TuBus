using System;
using System.Collections.Generic;
using DataAccess.Crud;
using Entities;
using Exceptions;

namespace CoreAPI
{
    public class EstadoTarjetaManager : BaseManager
    {
        private readonly EstadoTarjetaCrudFactory _crudFactory;

        public EstadoTarjetaManager()
        {
            _crudFactory = new EstadoTarjetaCrudFactory();
        }

        public EstadoTarjeta GeTEstadoById(int id)
        {
            try
            {
                return _crudFactory.Retrieve<EstadoTarjeta>(new EstadoTarjeta{EstadoTarjetaId = id});
            }
            catch (Exception e)
            {
                ExceptionManager.GetInstance().Process(e);
            }

            return null;
        }

        public List<EstadoTarjeta> GetTarjetas()
        {
            try
            {
               return _crudFactory.RetrieveAll<EstadoTarjeta>();
            }
            catch (Exception e)
            {
                ExceptionManager.GetInstance().Process(e);
            }

            return new List<EstadoTarjeta>();
        }
    }
}
