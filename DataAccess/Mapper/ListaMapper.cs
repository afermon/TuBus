using System;
using System.Collections.Generic;
using DataAccess.Dao;
using Entities;

namespace DataAccess.Mapper
{
    public class ListaMapper : EntityMapper, ISqlStaments, IObjectMapper
    {
        private const string DB_COL_ID = "LIST_ID";
        private const string DB_COL_VALUE = "VALUE";
        private const string DB_COL_DESCRIPTION = "DESCRIPTION";


        public SqlOperation GetCreateStatement(BaseEntity entity)
        {
            throw new NotSupportedException();
        }

        public SqlOperation GetDeleteStatement(BaseEntity entity)
        {
            throw new NotSupportedException();
        }

        public SqlOperation GetRetriveAllStatement()
        {
            throw new NotSupportedException();
        }

        public SqlOperation GetRetriveStatement(BaseEntity entity)
        {
            throw new NotSupportedException();
        }

        public SqlOperation GetRetriveByIdStatement(string listId)
        {
            var operation = new SqlOperation { ProcedureName = "RET_LIST_ID_PR" };

            operation.AddVarcharParam(DB_COL_ID, listId);

            return operation;
        }

        public SqlOperation GetUpdateStatement(BaseEntity entity)
        {
            throw new NotSupportedException();
        }

        public List<BaseEntity> BuildObjects(List<Dictionary<string, object>> lstRows)
        {
            var lstResults = new List<BaseEntity>();

            foreach (var row in lstRows)
            {
                var item = BuildObject(row);
                lstResults.Add(item);
            }

            return lstResults;
        }

        public BaseEntity BuildObject(Dictionary<string, object> row)
        {
            var listItem = new ListItem
            {
                ListId = GetStringValue(row, DB_COL_ID),
                Value = GetStringValue(row, DB_COL_VALUE),
                Description = GetStringValue(row, DB_COL_DESCRIPTION)
            };

            return listItem;
        }
    }
}