using System;
using System.Collections.Generic;
using DataAccess.Dao;
using Entities;

namespace DataAccess.Mapper
{
    class TarifaMapper : EntityMapper, ISqlStaments, IObjectMapper
    {
        public const string DB_COL_ROUTE_ID = "ROUTE_ID";
        public const string DB_COL_ROUTE_NAME = "ROUTE_NAME";
        public const string DB_COL_OPERATOR = "OPERATOR";
        public const string DB_COL_REGULAR_FARE = "REGULAR_FARE";


        public SqlOperation GetCreateStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "CRE_TARIFA_PR" };

            var t = (Tarifa)entity;
            operation.AddIntParam(DB_COL_ROUTE_ID, t.RouteId);
            operation.AddVarcharParam(DB_COL_ROUTE_NAME, t.RouteName);
            operation.AddVarcharParam(DB_COL_OPERATOR, t.Operator);
            operation.AddDoubleParam(DB_COL_REGULAR_FARE, t.RegularFare);

            return operation;
        }

        public SqlOperation GetDeleteStatement(BaseEntity entity)
        {
            throw new NotSupportedException();
        }

        public SqlOperation GetRetriveAllStatement()
        {
            return new SqlOperation { ProcedureName = "RET_ALL_TARIFA_PR" };
        }

        public SqlOperation GetRetriveStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "RET_TARIFA_PR" };

            var t = (Tarifa)entity;
            operation.AddIntParam(DB_COL_ROUTE_ID, t.RouteId);

            return operation;
        }

        public SqlOperation GetUpdateStatement(BaseEntity entity)
        {
            throw new NotSupportedException();
        }

        public BaseEntity BuildObject(Dictionary<string, object> row)
        {
            var tarifa = new Tarifa
            {
                RouteId = GetIntValue(row, DB_COL_ROUTE_ID),
                RouteName = GetStringValue(row, DB_COL_ROUTE_NAME),
                Operator = GetStringValue(row, DB_COL_OPERATOR),
                RegularFare = GetDoubleValue(row, DB_COL_REGULAR_FARE)
            };

            return tarifa;
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

        public SqlOperation GetRetriveOperadoresStatement()
        {
            return new SqlOperation { ProcedureName = "RET_TARIFA_OPERADORES_PR" };
        }
    }
}
