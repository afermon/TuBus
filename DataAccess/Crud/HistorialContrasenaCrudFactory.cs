using System;
using System.Collections.Generic;
using DataAccess.Dao;
using DataAccess.Mapper;
using Entities;

namespace DataAccess.Crud
{
    public class HistorialContrasenaCrudFactory : CrudFactory
    {
        private readonly HistorialContrasenaMapper _mapper;

        public HistorialContrasenaCrudFactory()
        {
            _mapper = new HistorialContrasenaMapper();
            dao = SqlDao.GetInstance();
        }

        public override void Create(BaseEntity entity)
        {
            var historialContrasena = (HistorialContrasena)entity;
            var sqlOperation = _mapper.GetCreateStatement(historialContrasena);
            dao.ExecuteProcedure(sqlOperation);
        }

        public override void Delete(BaseEntity entity)
        {
            var historialContrasena = (HistorialContrasena)entity;
            dao.ExecuteProcedure(_mapper.GetDeleteStatement(historialContrasena));
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
            var lstConfig = new List<T>();
            var lstResult = dao.ExecuteQueryProcedure(_mapper.GetRetriveAllStatement());
     
            if (lstResult.Count <= 0) return lstConfig;

            var objs = _mapper.BuildObjects(lstResult);
            foreach (var c in objs)
                lstConfig.Add((T)Convert.ChangeType(c, typeof(T)));

            return lstConfig;
        }

        public override void Update(BaseEntity entity)
        {
            var historialContrasena = (HistorialContrasena)entity;
            dao.ExecuteProcedure(_mapper.GetUpdateStatement(historialContrasena));
        }
    }
}