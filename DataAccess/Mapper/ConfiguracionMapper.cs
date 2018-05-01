using System;
using System.Collections.Generic;
using DataAccess.Dao;
using Entities;

namespace DataAccess.Mapper
{
    public class ConfiguracionMapper : EntityMapper, ISqlStaments, IObjectMapper
    {
        private const string DB_COL_ID = "CONFIG_ID";
        private const string DB_COL_NUMBER = "NUMBER_VALUE";
        private const string DB_COL_STRING = "STRING_VALUE";

        public SqlOperation GetCreateStatement(BaseEntity entity)
        {
            throw new NotSupportedException();
        }

        public SqlOperation GetRetriveStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "RET_CONFIGURACION_PR" };

            var c = (ConfiguracionItem)entity;
            operation.AddVarcharParam(DB_COL_ID, c.Id);

            return operation;
        }

        public SqlOperation GetRetriveAllStatement()
        {
            var operation = new SqlOperation { ProcedureName = "RET_ALL_CONFIGURACION_PR" };
            return operation;
        }

        public SqlOperation GetUpdateStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "UPD_CONFIGURACION_PR" };

            var c = (ConfiguracionItem)entity;
            operation.AddVarcharParam(DB_COL_ID, c.Id);
            operation.AddDoubleParam(DB_COL_NUMBER, c.NumberValue);
            operation.AddVarcharParam(DB_COL_STRING, c.StringValue);

            return operation;
        }

        public SqlOperation GetDeleteStatement(BaseEntity entity)
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
            var listItem = new ConfiguracionItem
            {
                Id = GetStringValue(row, DB_COL_ID),
                NumberValue = GetDoubleValue(row, DB_COL_NUMBER),
                StringValue = GetStringValue(row, DB_COL_STRING)
            };

            return listItem;
        }
    }
}