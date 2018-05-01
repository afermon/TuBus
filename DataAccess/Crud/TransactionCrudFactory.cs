using System;
using System.Collections.Generic;
using DataAccess.Dao;
using DataAccess.Mapper;
using Entities;

namespace DataAccess.Crud
{
    public class TransactionCrudFactory : CrudFactory
    {
        private TransactionMapper _mapper;

        public TransactionCrudFactory()
        {
            _mapper = new TransactionMapper();
            dao = SqlDao.GetInstance();
        }

        public override void Create(BaseEntity entity)
        {
            var transaccion = (Transaccion)entity;
            var sqlOperation = _mapper.GetCreateStatement(transaccion);
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
            var listTransactions = new List<T>();

            var lstResult = dao.ExecuteQueryProcedure(_mapper.GetRetriveAllStatement());
            if (lstResult.Count > 0)
            {
                var objs = _mapper.BuildObjects(lstResult);
                foreach (var c in objs)
                {
                    listTransactions.Add((T)Convert.ChangeType(c, typeof(T)));
                }
            }

            return listTransactions;
        }

        public List<T> RetrieveAllByUser<T>(BaseEntity card)
        {
            var listTransactions = new List<T>();

            var lstResult = dao.ExecuteQueryProcedure(_mapper.GetRetriveAllByUser(card));
            if (lstResult.Count > 0)
            {
                var objs = _mapper.BuildObjects(lstResult);
                foreach (var c in objs)
                {
                    listTransactions.Add((T)Convert.ChangeType(c, typeof(T)));
                }
            }

            return listTransactions;
        }

        public List<T> RetrieveAllByTerminal<T>(BaseEntity card)
        {
            var listTransactions = new List<T>();

            var lstResult = dao.ExecuteQueryProcedure(_mapper.GetRetriveAllByTerminal(card));
            if (lstResult.Count > 0)
            {
                var objs = _mapper.BuildObjects(lstResult);
                foreach (var c in objs)
                {
                    listTransactions.Add((T)Convert.ChangeType(c, typeof(T)));
                }
            }

            return listTransactions;
        }
        public override void Update(BaseEntity entity)
        {
            var transaction = (Tarjeta)entity;
            dao.ExecuteProcedure(_mapper.GetUpdateStatement(transaction));
        }

        public override void Delete(BaseEntity entity)
        {
            throw new System.NotImplementedException();
        }
    }
}
