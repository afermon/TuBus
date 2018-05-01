using System;
using System.Collections.Generic;
using Entities;
using DataAccess.Mapper;
using DataAccess.Dao;

namespace DataAccess.Crud
{
    public class QuejaCrudFactory : CrudFactory
    {
        QuejaMapper mapper;

        public QuejaCrudFactory() : base()
        {
            mapper = new QuejaMapper();
            dao = SqlDao.GetInstance();
        }

        public override void Create(BaseEntity entity)
        {
            var queja = (Queja)entity;
            var sqlOperation = mapper.GetCreateStatement(queja);
            dao.ExecuteProcedure(sqlOperation);
        }

        public override void Delete(BaseEntity entity)
        {
            var queja = (Queja)entity;
            dao.ExecuteProcedure(mapper.GetDeleteStatement(queja));
        }

        public override T Retrieve<T>(BaseEntity entity)
        {
            var lstResult = dao.ExecuteQueryProcedure(mapper.GetRetriveStatement(entity));
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                dic = lstResult[0];
                var objs = mapper.BuildObject(dic);
                return (T)Convert.ChangeType(objs, typeof(T));
            }

            return default(T);
        }

        public override List<T> RetrieveAll<T>()
        {
            var lstQuejas = new List<T>();

            var lstResult = dao.ExecuteQueryProcedure(mapper.GetRetriveAllStatement());
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                var objs = mapper.BuildObjects(lstResult);
                foreach (var c in objs)
                {
                    lstQuejas.Add((T)Convert.ChangeType(c, typeof(T)));
                }
            }

            return lstQuejas;
        }

        public List<T> RetrieveQuejasActivas<T>()
        {
            var lstQuejas = new List<T>();

            var lstResult = dao.ExecuteQueryProcedure(mapper.GetRetriveQuejasActivasStatement());
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                var objs = mapper.BuildObjects(lstResult);
                foreach (var c in objs)
                {
                    lstQuejas.Add((T)Convert.ChangeType(c, typeof(T)));
                }
            }

            return lstQuejas;
        }

        public List<T> RetriveQuejasActivasByRutaStatement<T>(int rutaNumber)
        {
            var lstQuejas = new List<T>();

            var lstResult = dao.ExecuteQueryProcedure(mapper.GetRetriveQuejasActivasByRutaStatement(rutaNumber));
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                var objs = mapper.BuildObjects(lstResult);
                foreach (var c in objs)
                {
                    lstQuejas.Add((T)Convert.ChangeType(c, typeof(T)));
                }
            }

            return lstQuejas;
        }

        public override void Update(BaseEntity entity)
        {
            throw new NotImplementedException();
        }

        
    }
}
