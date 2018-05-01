using System;
using System.Collections.Generic;
using Entities;
using DataAccess.Mapper;
using DataAccess.Dao;

namespace DataAccess.Crud
{
    public class RecorridoCrudFactory : CrudFactory
    {
        private readonly RecorridoMapper _mapper;

        public RecorridoCrudFactory() : base()
        {
            _mapper = new RecorridoMapper();
            dao = SqlDao.GetInstance();
        }

        public override void Create(BaseEntity entity)
        {
            throw new NotSupportedException();
        }

        public int CreateId(BaseEntity entity)
        {
            var sqlOperation = _mapper.GetCreateStatement(entity);
            var lstResult = dao.ExecuteQueryProcedure(sqlOperation);

            if (lstResult.Count > 0)
            {
                var dic = lstResult[0];
                return Convert.ToInt32(dic["RECORRIDO_ID"]);
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
            var list = new List<T>();
            var lstResult = dao.ExecuteQueryProcedure(_mapper.GetRetriveAllStatement());

            if (lstResult.Count > 0)
            {
                var objs = _mapper.BuildObjects(lstResult);
                foreach (var c in objs)
                    list.Add((T)Convert.ChangeType(c, typeof(T)));
            }

            return list;
        }

        public List<T> RetrieveTardias<T>()
        {
            var list = new List<T>();
            var lstResult = dao.ExecuteQueryProcedure(_mapper.GetRetriveTardiaMesStatement());

            if (lstResult.Count > 0)
            {
                var objs = _mapper.BuildObjects(lstResult);
                foreach (var c in objs)
                    list.Add((T)Convert.ChangeType(c, typeof(T)));
            }

            return list;
        }


        public override void Update(BaseEntity entity)
        {
            dao.ExecuteProcedure(_mapper.GetUpdateStatement(entity));
        }

        public void UpdateSalida(BaseEntity entity)
        {
            dao.ExecuteProcedure(_mapper.GetUpdateSalidaStatement(entity));
        }

        public void UpdateLlegada(BaseEntity entity)
        {
            dao.ExecuteProcedure(_mapper.GetUpdateLlegadaStatement(entity));
        }

        public void UpdatePasajeros(BaseEntity entity)
        {
            dao.ExecuteProcedure(_mapper.GetUpdatePasajerosStatement(entity));
        }

        public override void Delete(BaseEntity entity)
        {
            dao.ExecuteProcedure(_mapper.GetDeleteStatement(entity));
        }
    }
}
