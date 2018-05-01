using System;
using System.Collections.Generic;
using Entities;
using DataAccess.Mapper;
using DataAccess.Dao;

namespace DataAccess.Crud
{
    public class TerminalCrudFactory : CrudFactory
    {
        TerminalMapper mapper;

        public TerminalCrudFactory() : base()
        {
            mapper = new TerminalMapper();
            dao = SqlDao.GetInstance();
        }

        public override void Create(BaseEntity entity)
        {
            var terminal = (Terminal)entity;
            var sqlOperation = mapper.GetCreateStatement(terminal);

            dao.ExecuteProcedure(sqlOperation);
        }

        public override void Delete(BaseEntity entity)
        {
            var terminal = (Terminal)entity;
            dao.ExecuteProcedure(mapper.GetDeleteStatement(terminal));
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

        public T RetrieveByName<T>(BaseEntity entity)
        {
            var lstResult = dao.ExecuteQueryProcedure(mapper.GetRetriveByNameStatement(entity));
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
            var lstTerminales = new List<T>();

            var lstResult = dao.ExecuteQueryProcedure(mapper.GetRetriveAllStatement());
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                var objs = mapper.BuildObjects(lstResult);
                foreach (var c in objs)
                {
                    lstTerminales.Add((T)Convert.ChangeType(c, typeof(T)));
                }
            }

            return lstTerminales;
        }

        public override void Update(BaseEntity entity)
        {
            var terminal = (Terminal)entity;
            dao.ExecuteProcedure(mapper.GetUpdateStatement(terminal));
        }
    }
}
