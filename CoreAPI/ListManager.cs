using System;
using System.Collections.Generic;
using DataAccess.Crud;
using Entities;
using Exceptions;

namespace CoreAPI
{
    public class ListManager : BaseManager
    {
        private readonly ListCrudFactory _crudList;

        public ListManager()
        {
            _crudList = new ListCrudFactory();
        }

        public List<ListItem> RetrieveListById(string listId)
        {
            var list = _crudList.RetrieveById<ListItem>(listId.ToUpper());

            try
            {
                if (list.Count > 0)
                    return list;
                throw new BusinessException(200);
            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
            }
            return list;
        }

    }
}