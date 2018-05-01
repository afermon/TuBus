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
    public class EmpresaCrudFactory : CrudFactory
    {
        private EmpresaMapper _mapper;

        public EmpresaCrudFactory()
        {
            _mapper = new EmpresaMapper();
            dao = SqlDao.GetInstance();
        }
        public override void Create(BaseEntity entity)
        {
            var empresa = (Empresa)entity;
            var sqlOperation = _mapper.GetCreateStatement(empresa);

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
            var lineas = new List<T>();

            var lstResult = dao.ExecuteQueryProcedure(_mapper.GetRetriveAllStatement());
            if (lstResult.Count > 0)
            {
                var objs = _mapper.BuildObjects(lstResult);
                foreach (var c in objs)
                {
                    lineas.Add((T)Convert.ChangeType(c, typeof(T)));
                }
            }

            return lineas;
        }

        public List<T> RetrieveAllPendientes<T>()
        {
            var lineas = new List<T>();

            var lstResult = dao.ExecuteQueryProcedure(_mapper.GetRetriveAllPendientesStatement());
            if (lstResult.Count > 0)
            {
                var objs = _mapper.BuildObjects(lstResult);
                foreach (var c in objs)
                {
                    lineas.Add((T)Convert.ChangeType(c, typeof(T)));
                }
            }

            return lineas;
        }

        public override void Update(BaseEntity entity)
        {
            dao.ExecuteProcedure(_mapper.GetUpdateStatement(entity));
        }

        public override void Delete(BaseEntity entity)
        {
            dao.ExecuteProcedure(_mapper.GetDeleteStatement(entity));
        }

        public void AprobarEmpresaStatement(BaseEntity entity)
        {
            dao.ExecuteProcedure(_mapper.GetAprobarEmpresaStatement(entity));
        }

        public T RetrieveByEmpresario<T>(BaseEntity entity)
        {
            var lstResult = dao.ExecuteQueryProcedure(_mapper.GetRetriveByEmpresarioStatement(entity));
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                dic = lstResult[0];
                var objs = _mapper.BuildObject(dic);
                return (T)Convert.ChangeType(objs, typeof(T));
            }

            return default(T);
        }

        public List<T> RetrieveEmpresaByRuta<T>(BaseEntity entity)
        {

            var lstEmpresa = new List<T>();
            var lstResult = dao.ExecuteQueryProcedure(_mapper.GetRetrieveEmpresaByRuta(entity));

            if (lstResult.Count > 0)
            {
                var objs = _mapper.BuildObjects(lstResult);
                foreach (var c in objs)
                    lstEmpresa.Add((T)Convert.ChangeType(c, typeof(T)));
            }

            return lstEmpresa;
        }
    }
}
