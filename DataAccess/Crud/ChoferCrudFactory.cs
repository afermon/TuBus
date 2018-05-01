using System;
using System.Collections.Generic;
using Entities;
using DataAccess.Mapper;
using DataAccess.Dao;

namespace DataAccess.Crud
{
    public class ChoferCrudFactory : CrudFactory
    {
        ChoferMapper mapper;

        public ChoferCrudFactory() : base()
        {
            mapper = new ChoferMapper();
            dao = SqlDao.GetInstance();
        }

        public override void Create(BaseEntity entity)
        {
            var chofer = (Chofer)entity;
            var sqlOperation = mapper.GetCreateStatement(chofer);

            dao.ExecuteProcedure(sqlOperation);
        }

        public override void Delete(BaseEntity entity)
        {
            var chofer = (Chofer)entity;
            dao.ExecuteProcedure(mapper.GetDeleteStatement(chofer));
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
            var lstChoferes = new List<T>();

            var lstResult = dao.ExecuteQueryProcedure(mapper.GetRetriveAllStatement());
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                var objs = mapper.BuildObjects(lstResult);
                foreach (var c in objs)
                {
                    lstChoferes.Add((T)Convert.ChangeType(c, typeof(T)));
                }
            }

            return lstChoferes;
        }

        public List<T> RetrieveAllRuta<T>(int ruta)
        {
            var lstChoferes = new List<T>();

            var lstResult = dao.ExecuteQueryProcedure(mapper.GetRetriveByRutaStatement(ruta));
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                var objs = mapper.BuildObjects(lstResult);
                foreach (var c in objs)
                {
                    lstChoferes.Add((T)Convert.ChangeType(c, typeof(T)));
                }
            }

            return lstChoferes;
        }

        public override void Update(BaseEntity entity)
        {
            var chofer = (Chofer)entity;
            dao.ExecuteProcedure(mapper.GetUpdateStatement(chofer));
        }
    }
}
