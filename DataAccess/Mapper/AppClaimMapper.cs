using System;
using System.Collections.Generic;
using DataAccess.Dao;
using Entities;

namespace DataAccess.Mapper
{
    public class AppClaimMapper : EntityMapper, ISqlStaments, IObjectMapper
    {
        private const string DB_COL_VISTA_ID = "VISTA_ID";
        private const string DB_COL_ROLE_ID = "ROLE_ID";
        private const string DB_COL_APP_CLAIM_ID = "APP_CLAIM_ID";
        private const string DB_COL_TYPE = "TYPE";
        private const string DB_COL_CONTROLLER = "API_CONTROLLER";
        private const string DB_COL_CONTROLLER_ACTION = "API_CONTROLLER_ACTION";

        public SqlOperation GetCreateStatement(BaseEntity entity)
        {
            throw new NotImplementedException();
        }

        public SqlOperation GetDeleteStatement(BaseEntity entity)
        {
            throw new NotImplementedException();
        }

        public SqlOperation GetRetriveAllStatement()
        {
            throw new NotImplementedException();
        }

        public SqlOperation GetRetriveStatement(BaseEntity entity)
        {
            throw new NotImplementedException();
        }

        public SqlOperation GetRetriveByVistaStatement(string vistaId)
        {
            var operation = new SqlOperation { ProcedureName = "RET_APP_CLAIM_VISTA_PR" };

            operation.AddVarcharParam(DB_COL_VISTA_ID, vistaId);

            return operation;
        }

        public SqlOperation GetRetriveByRoleStatement(string roleId)
        {
            var operation = new SqlOperation { ProcedureName = "RET_APP_CLAIM_ROLE_PR" };

            operation.AddVarcharParam(DB_COL_ROLE_ID, roleId);

            return operation;
        }

        public SqlOperation GetUpdateStatement(BaseEntity entity)
        {
            throw new NotImplementedException();
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
            var claim = new AppClaim
            {
                AppClaimId = GetStringValue(row, DB_COL_APP_CLAIM_ID),
                Type = GetStringValue(row, DB_COL_TYPE),
                ApiController = GetStringValue(row, DB_COL_CONTROLLER),
                ApiContollerAction = GetStringValue(row, DB_COL_CONTROLLER_ACTION)
            };

            return claim;
        }
    }
}