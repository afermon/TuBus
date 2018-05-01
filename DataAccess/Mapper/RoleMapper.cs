using System;
using System.Collections.Generic;
using DataAccess.Dao;
using Entities;

namespace DataAccess.Mapper
{
    public class RoleMapper : EntityMapper, ISqlStaments, IObjectMapper
    {
        private const string DB_COL_ROLE_ID = "ROLE_ID";
        private const string DB_COL_DESCRIPCION = "DESCRIPCION";
        private const string DB_COL_HOMEPAGE = "HOMEPAGE";
        private const string DB_COL_ESTADO = "ESTADO";

        public SqlOperation GetCreateStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "CRE_ROLE_PR" };

            var r = (Role)entity;
            operation.AddVarcharParam(DB_COL_ROLE_ID, r.RoleId);
            operation.AddVarcharParam(DB_COL_DESCRIPCION, r.Descripcion);
            operation.AddVarcharParam(DB_COL_HOMEPAGE, r.Homepage);

            return operation;
        }

        public SqlOperation GetDeleteStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "DEL_ROLE_PR" };

            var r = (Role)entity;
            operation.AddVarcharParam(DB_COL_ROLE_ID, r.RoleId);
            return operation;
        }

        public SqlOperation GetRetriveAllStatement()
        {
            var operation = new SqlOperation { ProcedureName = "RET_ALL_ROLE_PR" };
            return operation;
        }

        public SqlOperation GetRetriveStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "RET_ROLE_PR" };

            var r = (Role)entity;
            operation.AddVarcharParam(DB_COL_ROLE_ID, r.RoleId);

            return operation;
        }

        public SqlOperation GetRetriveRolVistaStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "RET_ROLE_VISTA_PR" };

            var r = (Role)entity;
            operation.AddVarcharParam(DB_COL_ROLE_ID, r.RoleId);

            return operation;
        }

        public SqlOperation GetUpdateStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "UPD_ROLE_PR" };
            var rol = (Role)entity;

            operation.AddVarcharParam(DB_COL_ROLE_ID, rol.RoleId);
            operation.AddVarcharParam(DB_COL_DESCRIPCION, rol.Descripcion);
            operation.AddVarcharParam(DB_COL_HOMEPAGE, rol.Homepage);

            return operation;
        }

        public SqlOperation GetUpdateEstadoStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "UPD_ROLE_ESTADO_PR" };
            var rol = (Role)entity;

            operation.AddVarcharParam(DB_COL_ROLE_ID, rol.RoleId);

            return operation;
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
            var role = new Role
            {
                RoleId = GetStringValue(row, DB_COL_ROLE_ID),
                Descripcion = GetStringValue(row, DB_COL_DESCRIPCION),
                Homepage = GetStringValue(row, DB_COL_HOMEPAGE),
                Estado = GetStringValue(row, DB_COL_ESTADO)
            };

            return role;
        }
    }
}