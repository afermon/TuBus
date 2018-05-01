using System;
using System.Collections.Generic;
using DataAccess.Dao;
using DataAccess.Mapper;
using Entities;

namespace DataAccess.Crud
{
    public class TarjetaCrudFactory : CrudFactory
    {
        private TarjetaMapper _mapper;

        public TarjetaCrudFactory()
        {
            _mapper = new TarjetaMapper();
            dao = SqlDao.GetInstance();
        }

        public override void Create(BaseEntity entity)
        {
            var tarjeta = (Tarjeta)entity;
            var sqlOperation = _mapper.GetCreateStatement(tarjeta);
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

        public List<T> RetrieveAllByUser<T>(BaseEntity card)
        {
            var listCards = new List<T>();

            var lstResult = dao.ExecuteQueryProcedure(_mapper.GetRetriveAllByUser(card));
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
            var tarjeta = (Tarjeta)entity;
            dao.ExecuteProcedure(_mapper.GetUpdateStatement(tarjeta));
        }

        public override void Delete(BaseEntity entity)
        {
            var tarjeta = (Tarjeta)entity;
            dao.ExecuteProcedure(_mapper.GetDeleteStatement(tarjeta));
        }

        public void UpdateSaldo(BaseEntity entity)
        {
            var tarjeta = (Tarjeta)entity;
            dao.ExecuteProcedure(_mapper.UpdateTarjetaSaldo(tarjeta));
        }

        public void UpdateEstado(BaseEntity entity)
        {
            var tarjeta = (Tarjeta)entity;
            dao.ExecuteProcedure(_mapper.UpdateEstadoTarjeta(tarjeta));
        }

        public List<T> RetrieveRequestedCards<T>()
        {
            var listCards = new List<T>();

            var lstResult = dao.ExecuteQueryProcedure(_mapper.GetSolicitudReposicion());
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
    }
}
