using System;
using System.Collections.Generic;
using DataAccess.Dao;
using DataAccess.Mapper;
using Entities;

namespace DataAccess.Crud
{
    public class TarifaCrudFactory : CrudFactory
    {
        private readonly TarifaMapper _mapper;

        public TarifaCrudFactory()
        {
            _mapper = new TarifaMapper();
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

            if (lstResult.Count <= 0) return default(T);

            var dic = lstResult[0];
            var objs = _mapper.BuildObject(dic);

            return (T)Convert.ChangeType(objs, typeof(T));

        }

        public override List<T> RetrieveAll<T>()
        {
            var lstTarifa = new List<T>();
            var lstResult = dao.ExecuteQueryProcedure(_mapper.GetRetriveAllStatement());

            if (lstResult.Count <= 0) return lstTarifa;

            var objs = _mapper.BuildObjects(lstResult);
            foreach (var c in objs)
                lstTarifa.Add((T)Convert.ChangeType(c, typeof(T)));

            return lstTarifa;
        }

        public override void Update(BaseEntity entity)
        {
            dao.ExecuteProcedure(_mapper.GetUpdateStatement(entity));
        }

        public override void Delete(BaseEntity entity)
        {
            dao.ExecuteProcedure(_mapper.GetDeleteStatement(entity));
        }

        public List<T> RetrieveEmpresarios<T>()
        {
            var list = new List<T>();
            var lstResult = dao.ExecuteQueryProcedure(_mapper.GetRetriveOperadoresStatement());

            if (lstResult.Count <= 0) return list;

            var objs = _mapper.BuildObjects(lstResult);
            foreach (var c in objs)
                list.Add((T)Convert.ChangeType(c, typeof(T)));

            return list;
        }
    }
}