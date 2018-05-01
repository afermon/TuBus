using System;
using System.Collections.Generic;
using DataAccess.Dao;
using Entities;

namespace DataAccess.Mapper
{
    public class VistaMapper : EntityMapper, ISqlStaments, IObjectMapper
    {
        private const string DB_COL_ROLE_ID = "ROLE_ID";
        private const string DB_COL_VISTA_ID = "VISTA_ID";
        private const string DB_COL_VISTA_NOMBRE = "VISTA_NOMBRE";
        private const string DB_COL_DESCRIPCION = "DESCRIPCION";
        private const string DB_COL_URL = "URL";
        private const string DB_COL_ICON = "ICON";
        private const string DB_COL_APP_CONTROLLER = "APP_CONTROLLER";
        private const string DB_COL_APP_CONTROLLER_ACTION = "APP_CONTROLLER_ACTION";
        private const string DB_COL_MENU = "SHOW_MENU";

        public SqlOperation GetCreateStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "CRE_VISTA_ROL_PR" };

            var r = (Vista)entity;
            operation.AddVarcharParam(DB_COL_VISTA_ID, r.VistaId);
            operation.AddVarcharParam(DB_COL_ROLE_ID, r.RoleId);

            return operation;
        }

        public SqlOperation GetDeleteStatement(BaseEntity entity)
        {
            throw new NotSupportedException();
        }

        public SqlOperation GetRetriveAllStatement()
        {
            var operation = new SqlOperation { ProcedureName = "RET_ALL_VISTA_PR" };
            return operation;
        }

        public SqlOperation GetRetriveStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "RET_VISTA_PR" };

            var v = (Vista)entity;
            operation.AddVarcharParam(DB_COL_VISTA_ID, v.VistaId);

            return operation;
        }

        public SqlOperation GetRetriveByRoleStatement(string roleId)
        {
            var operation = new SqlOperation { ProcedureName = "RET_VISTA_ROLE_PR" };

            operation.AddVarcharParam(DB_COL_ROLE_ID, roleId);

            return operation;
        }

        public SqlOperation GetRetriveByRoleAndViewStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "RET_VISTA_POR_ROLE_VISTA_PR" };

            var r = (Vista)entity;

            operation.AddVarcharParam(DB_COL_ROLE_ID, r.RoleId);
            operation.AddVarcharParam(DB_COL_VISTA_ID, r.VistaId);

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
            var vista = new Vista
            {
                VistaId = GetStringValue(row, DB_COL_VISTA_ID),
                Nombre = GetStringValue(row, DB_COL_VISTA_NOMBRE),
                Descripcion = GetStringValue(row, DB_COL_DESCRIPCION),
                Url = GetStringValue(row,DB_COL_URL),
                Icon = GetStringValue(row, DB_COL_ICON),
                AppController = GetStringValue(row, DB_COL_APP_CONTROLLER),
                AppControllerAction = GetStringValue(row, DB_COL_APP_CONTROLLER_ACTION),
                ShowMenu = GetBoolValue(row, DB_COL_MENU)
            };

            return vista;
        }
    }
}