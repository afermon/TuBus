using System;
using System.Collections.Generic;
using DataAccess.Crud;
using Entities;
using Exceptions;

namespace CoreAPI
{
    public class TipoTarjetaManager : BaseManager
    {
        private readonly TipoTarjetaCrudFactory _crudFactory;

        public TipoTarjetaManager()
        {
            _crudFactory =  new TipoTarjetaCrudFactory();
        }

        public List<TipoTarjeta> GetAllTypesCards()
        {
            try
            {
                return _crudFactory.RetrieveAll<TipoTarjeta>();
            }
            catch (Exception e)
            {
                ExceptionManager.GetInstance().Process(e);
            }
            return new List<TipoTarjeta>();
        }

        public void CreateCardType(TipoTarjeta cardType)
        {
            try
            {
                _crudFactory.Create(cardType);
            }
            catch (Exception e)
            {
                ExceptionManager.GetInstance().Process(e);
            }
        }

        public void UpdateCardType(TipoTarjeta cardType)
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

        public TipoTarjeta GetCardTypeById(TipoTarjeta cardType)
        {
            try
            {
                return _crudFactory.Retrieve<TipoTarjeta>(cardType);
            }
            catch (Exception e)
            {
                ExceptionManager.GetInstance().Process(e);
            }
            return new TipoTarjeta();
        }

        public void DeleteCardType(TipoTarjeta cardType)
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
    }
}
