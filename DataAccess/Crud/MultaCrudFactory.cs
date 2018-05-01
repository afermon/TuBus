using System;
using System.Collections.Generic;
using Entities;
using DataAccess.Mapper;
using DataAccess.Dao;

namespace DataAccess.Crud
{
    public class MultaCrudFactory : CrudFactory

    {
        MultaMapper mapper;

        public MultaCrudFactory() : base()
        {
            mapper = new MultaMapper();
            dao = SqlDao.GetInstance();
        }

        public override void Create(BaseEntity entity)
        {
            var multa = (Multa)entity;
            var sqlOperation = mapper.GetCreateStatement(multa);

            dao.ExecuteProcedure(sqlOperation);
        }

        public override void Delete(BaseEntity entity)
        {
            var multa = (Multa)entity;
            dao.ExecuteProcedure(mapper.GetDeleteStatement(multa));
        }

        public override T Retrieve<T>(BaseEntity entity)
        {
            throw new NotImplementedException();
        }

        public override List<T> RetrieveAll<T>()
        {
            var lstMultas = new List<T>();

            var lstResult = dao.ExecuteQueryProcedure(mapper.GetRetriveAllStatement());
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                var objs = mapper.BuildObjects(lstResult);
                foreach (var c in objs)
                {
                    lstMultas.Add((T)Convert.ChangeType(c, typeof(T)));
                }
            }

            return lstMultas;
        }

        public override void Update(BaseEntity entity)
        {
            var multa = (Multa)entity;
            dao.ExecuteProcedure(mapper.GetUpdateStatement(multa));
        }
    }
}
