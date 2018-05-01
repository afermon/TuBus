using System;
using System.Collections.Generic;
using Entities;
using DataAccess.Mapper;
using DataAccess.Dao;

namespace DataAccess.Crud
{
    public class RequisitoCrudFactory : CrudFactory
    {
        RequisitoMapper _mapper;

        public RequisitoCrudFactory()
        {
            _mapper = new RequisitoMapper();
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
            Dictionary<string, object> dic;

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
            var listaRequisitos = new List<T>();
            var resultados = dao.ExecuteQueryProcedure(_mapper.GetRetriveAllStatement());

            if (resultados.Count > 0)
            {
                var objs = _mapper.BuildObjects(resultados);

                foreach (var c in objs)
                    listaRequisitos.Add((T)Convert.ChangeType(c, typeof(T)));
            }

            return listaRequisitos;
        }

        public List<T> RetriveAllByPlacaStatement<T>(BaseEntity entity)
        {
            var listaRequisitos = new List<T>();
            var resultados = dao.ExecuteQueryProcedure(_mapper.GetRetriveAllByPlacaStatement(entity));

            if (resultados.Count > 0)
            {
                var objs = _mapper.BuildObjects(resultados);

                foreach (var c in objs)
                    listaRequisitos.Add((T)Convert.ChangeType(c, typeof(T)));
            }

            return listaRequisitos;
        }

        public override void Update(BaseEntity entity)
        {
            var requisito = (Requisito)entity;

            dao.ExecuteProcedure(_mapper.GetUpdateStatement(requisito));
        }

        public override void Delete(BaseEntity entity)
        {
            dao.ExecuteProcedure(_mapper.GetDeleteStatement(entity));
        }

        public T RetrieveByName<T>(BaseEntity entity)
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
    }
}
