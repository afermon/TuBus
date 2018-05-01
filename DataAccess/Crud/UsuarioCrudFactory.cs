using System;
using System.Collections.Generic;
using DataAccess.Dao;
using DataAccess.Mapper;
using Entities;

namespace DataAccess.Crud
{
    public class UsuarioCrudFactory : CrudFactory
    {
        readonly UsuarioMapper _mapper;

        public UsuarioCrudFactory()
        {
            _mapper = new UsuarioMapper();
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
            var lstRoles = new List<T>();
            var lstResult = dao.ExecuteQueryProcedure(_mapper.GetRetriveAllStatement());

            if (lstResult.Count <= 0) return lstRoles;

            var objs = _mapper.BuildObjects(lstResult);
            foreach (var c in objs)
                lstRoles.Add((T)Convert.ChangeType(c, typeof(T)));

            return lstRoles;
        }

        public T RetrieveAuth<T>(BaseEntity entity)
        {
            var lstResult = dao.ExecuteQueryProcedure(_mapper.GetRetriveStatement(entity));

            if (lstResult.Count <= 0) return default(T);

            var dic = lstResult[0];
            var objs = _mapper.BuildObjectSecure(dic);

            return (T)Convert.ChangeType(objs, typeof(T));

        }

        public override void Update(BaseEntity entity)
        {
            dao.ExecuteProcedure(_mapper.GetUpdateStatement(entity));
        }

        public void UpdatePassword(BaseEntity entity)
        {
            dao.ExecuteProcedure(_mapper.GetUpdatePasswordStatement(entity));
        }

        public void UpdateResetToken(BaseEntity entity)
        {
            dao.ExecuteProcedure(_mapper.GetUpdateResetTokenStatement(entity));
        }

        public override void Delete(BaseEntity entity)
        {
            dao.ExecuteProcedure(_mapper.GetDeleteStatement(entity));
        }
    }
}