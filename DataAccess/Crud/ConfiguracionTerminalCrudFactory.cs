using System;
using System.Collections.Generic;
using DataAccess.Dao;
using DataAccess.Mapper;
using Entities;

namespace DataAccess.Crud
{
    public class ConfiguracionTerminalCrudFactory : CrudFactory
    {
        ConfiguracionTerminalMapper _mapper;

        public ConfiguracionTerminalCrudFactory() : base()
        {
            _mapper = new ConfiguracionTerminalMapper();
            dao = SqlDao.GetInstance();
        }

        public override void Create(BaseEntity entity)
        {
            var configuracionTerminal = (ConfiguracionTerminal)entity;
            var sqlOperation = _mapper.GetCreateStatement(configuracionTerminal);
            dao.ExecuteProcedure(sqlOperation);
        }

        public override void Delete(BaseEntity entity)
        {
            var configuracionTerminal = (ConfiguracionTerminal)entity;
            dao.ExecuteProcedure(_mapper.GetDeleteStatement(configuracionTerminal));
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
            var lstConfig = new List<T>();

            var lstResult = dao.ExecuteQueryProcedure(_mapper.GetRetriveAllStatement());
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                var objs = _mapper.BuildObjects(lstResult);
                foreach (var c in objs)
                {
                    lstConfig.Add((T)Convert.ChangeType(c, typeof(T)));
                }
            }

            return lstConfig;
        }

        public override void Update(BaseEntity entity)
        {
            var configuracionTerminal = (ConfiguracionTerminal)entity;
            dao.ExecuteProcedure(_mapper.GetUpdateStatement(configuracionTerminal));
        }
    }
}