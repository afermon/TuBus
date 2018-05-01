using System;
using System.Collections.Generic;
using DataAccess.Dao;
using DataAccess.Mapper;
using Entities;

namespace DataAccess.Crud
{
    public class LineaCrudFactory : CrudFactory
    {
        private LineaMapper _mapper;

        public LineaCrudFactory()
        {
            _mapper = new LineaMapper();
            dao = SqlDao.GetInstance();
        }

        public override void Create(BaseEntity entity)
        {
            var sqlOperation = _mapper.GetCreateStatement(entity);
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

        public Linea GetSpaces(BaseEntity entity)
        {
            var lstResult = dao.ExecuteQueryProcedure(_mapper.GetAllSpaces(entity));
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                dic = lstResult[0];
                var objs = _mapper.BuildSpaces(dic);
                return (Linea)Convert.ChangeType(objs, typeof(Linea));
            }

            return new Linea();
        }
        public override List<T> RetrieveAll<T>()
        {
            var listCards = new List<T>();

            var lstResult = dao.ExecuteQueryProcedure(_mapper.GetRetriveAllStatement());
            if (lstResult.Count > 0)
            {
                var objs = _mapper.BuildObjects(lstResult);
                foreach (var c in objs)
                {
                    listCards.Add((T)Convert.ChangeType(c, typeof(T)));
                }
            }

            return listCards;
        }

       public override void Update(BaseEntity entity)
        {
            dao.ExecuteProcedure(_mapper.GetUpdateStatement(entity));
        }

        public override void Delete(BaseEntity entity)
        {
            dao.ExecuteProcedure(_mapper.GetDeleteStatement(entity));
        }
    }
}
