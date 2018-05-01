
using System;
using System.Collections.Generic;
using DataAccess.Dao;
using Entities;

namespace DataAccess.Mapper
{
    public class HistorialContrasenaMapper : EntityMapper, ISqlStaments, IObjectMapper
    {
        private const string DB_COL_FECHA = "PASSWORD_SET";
        private const string DB_COL_EMAIL = "EMAIL";
        private const string DB_COL_HASH = "PASSWORD_HASH";
        private const string DB_COL_COUNT = "COUNT";

        public SqlOperation GetCreateStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "CRE_HISTORIAL_CONTRASENA_PR" };

            var h = (HistorialContrasena)entity;
            operation.AddDateTimeParam(DB_COL_FECHA, h.Fecha);
            operation.AddVarcharParam(DB_COL_EMAIL, h.Email);
            operation.AddVarcharParam(DB_COL_HASH, h.PasswordHash);

            return operation;
        }

        public SqlOperation GetDeleteStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "DEL_HISTORIAL_CONTRASENA_PR" };

            var h = (HistorialContrasena)entity;
            operation.AddVarcharParam(DB_COL_EMAIL, h.Email);
            operation.AddIntParam(DB_COL_COUNT, h.Count);

            return operation;
        }

        public SqlOperation GetRetriveAllStatement()
        {
            throw new NotSupportedException();
        }

        public SqlOperation GetRetriveStatement(BaseEntity entity)
        {
            var h = (HistorialContrasena)entity;
            var operation = new SqlOperation { ProcedureName = "RET_HISTORIAL_CONTRASENA_PR" };

            operation.AddVarcharParam(DB_COL_EMAIL, h.Email);
            operation.AddIntParam(DB_COL_COUNT, h.Count);
            operation.AddVarcharParam(DB_COL_HASH, h.PasswordHash);

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
            var historialContrasena = new HistorialContrasena
            {
                Fecha = GetDateValue(row, DB_COL_FECHA),
                Email = GetStringValue(row, DB_COL_EMAIL),
                PasswordHash = GetStringValue(row, DB_COL_HASH)
            };

            return historialContrasena;
        }
    }
}