using System;
using System.Collections.Generic;
using DataAccess.Dao;
using DataAccess.Mapper;
using Entities;

namespace DataAccess.Crud
{
    public class ReportCrudFactory : CrudFactory
    {
    private ReportMapper _mapper;

    public ReportCrudFactory()
    {
        _mapper = new ReportMapper();
        dao = SqlDao.GetInstance();
    }

        public List<T> RetriveAllBenefits<T>()
        {
            var retriveAllBenefits = new List<T>();

            var lstResult = dao.ExecuteQueryProcedure(_mapper.GetRetriveAllBenefitsStatement());
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                var objs = _mapper.BuildObjects(lstResult);
                foreach (var c in objs)
                {
                    retriveAllBenefits.Add((T)Convert.ChangeType(c, typeof(T)));
                }
            }

            return retriveAllBenefits;
        }

        public List<T> RetriveMoviminetosUsuario<T>(string email)
        {
            var retriveAllBenefits = new List<T>();

            var lstResult = dao.ExecuteQueryProcedure(_mapper.GetRetriveMovement(email));
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                var objs = _mapper.BuildObjects(lstResult,false , true);
                foreach (var c in objs)
                {
                    retriveAllBenefits.Add((T)Convert.ChangeType(c, typeof(T)));
                }
            }

            return retriveAllBenefits;
        }
        public List<T> RetriveAllBenefitsByCard<T>()
        {
            var allBenefitsByCard = new List<T>();

            var lstResult = dao.ExecuteQueryProcedure(_mapper.GetRetriveTipoTarjetasStatement());
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                var objs = _mapper.BuildObjects(lstResult, true);
                foreach (var c in objs)
                {
                    allBenefitsByCard.Add((T)Convert.ChangeType(c, typeof(T)));
                }
            }

            return allBenefitsByCard;
        }

        public List<T> RetriveAllTransactionsByType<T>()
        {
            var retriveAllTransactionsByType = new List<T>();

            var lstResult = dao.ExecuteQueryProcedure(_mapper.GetRetriveStatement());
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                var objs = _mapper.BuildObjects(lstResult, true);
                foreach (var c in objs)
                {
                    retriveAllTransactionsByType.Add((T)Convert.ChangeType(c, typeof(T)));
                }
            }

            return retriveAllTransactionsByType;
        }

        public override void Create(BaseEntity entity)
        {
            throw new System.NotImplementedException();
        }

        public override T Retrieve<T>(BaseEntity entity)
        {
            throw new System.NotImplementedException();
        }

        public override List<T> RetrieveAll<T>()
        {
            throw new System.NotImplementedException();
        }

        public override void Update(BaseEntity entity)
        {
            throw new System.NotImplementedException();
        }

        public override void Delete(BaseEntity entity)
        {
            throw new System.NotImplementedException();
        }
    }
}
