using System;
using System.Collections.Generic;
using DataAccess.Dao;
using DataAccess.Mapper;
using Entities;

namespace DataAccess.Crud
{
    public class ListCrudFactory : CrudFactory
    {
        ListaMapper _mapper;

        public ListCrudFactory() : base()
        {
            _mapper = new ListaMapper();
            dao = SqlDao.GetInstance();
        }

        public override void Create(BaseEntity entity)
        {
            throw new NotImplementedException();
        }

        public override T Retrieve<T>(BaseEntity entity)
        {
            throw new NotImplementedException();
        }

        public override List<T> RetrieveAll<T>()
        {
            throw new NotImplementedException();
        }

        public override void Update(BaseEntity entity)
        {
            throw new NotImplementedException();
        }

        public override void Delete(BaseEntity entity)
        {
            throw new NotImplementedException();
        }

        public List<T> RetrieveById<T>(string listId)
        {
            var lstItems = new List<T>();

            var lstResult = dao.ExecuteQueryProcedure(_mapper.GetRetriveByIdStatement(listId));
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
    }
}