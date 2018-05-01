using System;
using System.Collections.Generic;
using Entities;
using DataAccess.Mapper;
using DataAccess.Dao;

namespace DataAccess.Crud
{
    public class RutaCrudFactory : CrudFactory
    {
        private RutaMapper _mapper;
        private EmpresaMapper mapperEmpresa;

        public RutaCrudFactory() : base()
        {
            _mapper = new RutaMapper();
            dao = SqlDao.GetInstance();
        }

        public override void Create(BaseEntity entity)
        {
            var sqlOperation = _mapper.GetCreateStatement(entity);
            var cod = dao.ExecuteQueryProcedure(sqlOperation);
        }

        public int CreateId(BaseEntity entity)
        {
            var sqlOperation = _mapper.GetCreateStatement(entity);
            var lstResult = dao.ExecuteQueryProcedure(sqlOperation);

            if (lstResult.Count > 0)
            {
                var dic = lstResult[0];
                return Convert.ToInt32(dic["RUTA_ID"]);
            }
            return -1;
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

        public List<T> RetrieveByTerminal<T>(BaseEntity entity)
        {
            var lstRoles = new List<T>();
            var lstResult = dao.ExecuteQueryProcedure(_mapper.GetRetriveByTerminalStatement(entity));

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
    }
}
