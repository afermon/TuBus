using System;
using System.Collections.Generic;
using DataAccess.Dao;
using DataAccess.Mapper;
using Entities;

namespace DataAccess.Crud
{
    public class ConvenioCrudFactory : CrudFactory
    {
        private ConvenioMapper _mapper;

        public ConvenioCrudFactory()
        {
            _mapper = new ConvenioMapper();
            dao = SqlDao.GetInstance();
        }

        public override void Create(BaseEntity entity)
        {
            var convenio = (Convenio)entity;
            var sqlOperation = _mapper.GetCreateStatement(convenio);
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
            var convenios = new List<T>();

            var lstResult = dao.ExecuteQueryProcedure(_mapper.GetRetriveAllStatement());
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                var objs = _mapper.BuildObjects(lstResult);
                foreach (var c in objs)
                {
                    convenios.Add((T)Convert.ChangeType(c, typeof(T)));
                }
            }

            return convenios;
        }

        public List<T> RetrieveByTerminalId<T>(BaseEntity entity)
        {
            var convenios = new List<T>();

            var lstResult = dao.ExecuteQueryProcedure(_mapper.GetRetriveByTerminalId(entity));
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                var objs = _mapper.BuildObjects(lstResult);
                foreach (var c in objs)
                {
                    convenios.Add((T)Convert.ChangeType(c, typeof(T)));
                }
            }

            return convenios;
        }

        public override void Update(BaseEntity entity)
        {
            var convenio = (Convenio)entity;
            RemoveOldConvenios(convenio);
            dao.ExecuteProcedure(_mapper.GetUpdateStatement(convenio));
            CreateRelationIds(convenio);
        }

        public override void Delete(BaseEntity entity)
        {
            var convenio = (Convenio)entity;
            dao.ExecuteProcedure(_mapper.GetDeleteStatement(convenio));
        }

        public void CreateRelationIds(BaseEntity entity)
        {
            var convenio = (Convenio)entity;
            convenio.TerminalesId.ForEach(id =>
            {
                var sqlOperation = _mapper.GetCreateMultipleStatement(convenio, id);
                dao.ExecuteProcedure(sqlOperation);
            });
        }

        public void RemoveOldConvenios(BaseEntity entity)
        {
            var sqlOperation = _mapper.GetDeleteConveniosStatement(entity);
            dao.ExecuteProcedure(sqlOperation);
        }
    }
}
