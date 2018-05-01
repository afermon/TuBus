using System;
using System.Collections.Generic;
using DataAccess.Dao;
using DataAccess.Mapper;
using Entities;

namespace DataAccess.Crud
{
    public class VistaCrudFactory : CrudFactory
    {
        VistaMapper _mapper;

        public VistaCrudFactory() : base()
        {
            _mapper = new VistaMapper();
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
            var lstRoles = new List<T>();
            var lstResult = dao.ExecuteQueryProcedure(_mapper.GetRetriveAllStatement());

            if (lstResult.Count > 0)
            {
                var objs = _mapper.BuildObjects(lstResult);
                foreach (var c in objs)
                    lstRoles.Add((T)Convert.ChangeType(c, typeof(T)));
            }

            return lstRoles;
        }

        public override void Update(BaseEntity entity)
        {
            dao.ExecuteProcedure(_mapper.GetUpdateStatement(entity));
        }

        public override void Delete(BaseEntity entity)
        {
            dao.ExecuteProcedure(_mapper.GetDeleteStatement(entity));
        }

        public List<T> RetrieveByRole<T>(string roleId)
        {
            var lstItems = new List<T>();

            var lstResult = dao.ExecuteQueryProcedure(_mapper.GetRetriveByRoleStatement(roleId));
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                var objs = _mapper.BuildObjects(lstResult);
                foreach (var c in objs)
                {
                    lstItems.Add((T)Convert.ChangeType(c, typeof(T)));
                }
            }

            return lstItems;
        }

        public T RetrieveByRoleAndView<T>(BaseEntity entity)
        {
            var lstResult = dao.ExecuteQueryProcedure(_mapper.GetRetriveByRoleAndViewStatement(entity));
            Dictionary<string, object> dic;

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