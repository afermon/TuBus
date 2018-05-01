using System;
using System.Collections.Generic;
using DataAccess.Dao;
using DataAccess.Mapper;
using Entities;

namespace DataAccess.Crud
{
    public class TipoTarjetaCrudFactory : CrudFactory
    {
        private TipoTarjetaMapper _mapper;

        public TipoTarjetaCrudFactory()
        {
            _mapper = new TipoTarjetaMapper();
            dao = SqlDao.GetInstance();
        }

        public override void Create(BaseEntity entity)
        {
            var tipoTarjeta = (TipoTarjeta)entity;
            var sqlOperation = _mapper.GetCreateStatement(tipoTarjeta);
            dao.ExecuteProcedure(sqlOperation);
        }

        public override T Retrieve<T>(BaseEntity entity)
        {
            var lstResult = dao.ExecuteQueryProcedure(_mapper.GetRetriveStatement(entity));
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                dic = lstResult[0];
                var objs = _mapper.BuildObject(dic);
                return (T)Convert.ChangeType(objs, typeof(T));
            }

            return default(T);
        }

        public override List<T> RetrieveAll<T>()
        {
            var lstCustomers = new List<T>();

            var lstResult = dao.ExecuteQueryProcedure(_mapper.GetRetriveAllStatement());
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                var objs = _mapper.BuildObjects(lstResult);
                foreach (var c in objs)
                {
                    lstCustomers.Add((T)Convert.ChangeType(c, typeof(T)));
                }
            }

            return lstCustomers;
        }

        public override void Update(BaseEntity entity)
        {
            var tipoTarjeta = (TipoTarjeta)entity;
            dao.ExecuteProcedure(_mapper.GetUpdateStatement(tipoTarjeta));
        }

        public override void Delete(BaseEntity entity)
        {
            var tipoTarjeta = (TipoTarjeta)entity;
            dao.ExecuteProcedure(_mapper.GetDeleteStatement(tipoTarjeta));
        }
    }
}
