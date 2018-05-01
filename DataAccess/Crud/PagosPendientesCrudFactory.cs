using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Dao;
using DataAccess.Mapper;
using Entities;

namespace DataAccess.Crud
{
    public class PagosPendientesCrudFactory : CrudFactory
    {
        private PagosPendientesMapper _mapper;

        public PagosPendientesCrudFactory()
        {
            _mapper = new PagosPendientesMapper();
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

        
        public List<T> RetrieveAllByEmpresa<T>(BaseEntity pago)
        {
            var listTransactions = new List<T>();

            var lstResult = dao.ExecuteQueryProcedure(_mapper.GetRetriveAllByEmpresa(pago));
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
            dao.ExecuteProcedure(_mapper.GetUpdateStatement(entity));
        }

        public override void Delete(BaseEntity entity)
        {
            throw new System.NotImplementedException();
        }
    }
}
