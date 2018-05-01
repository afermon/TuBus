using System;
using DataAccess.Dao;
using Entities;
using System.Collections.Generic;

namespace DataAccess.Mapper
{
    public class SancionMapper : EntityMapper, ISqlStaments, IObjectMapper
    {
        private const string DB_COL_ID = "SANCION_ID";
        private const string DB_COL_DESCRIPCION = "DESCRIPCION";
        private const string DB_COL_MULTA = "MULTA";
        private const string DB_COL_ESTADO = "ESTADO";
        private const string DB_COL_TERMINAL_ID = "TERMINAL_ID";
        private const string DB_COL_FECHA = "FECHA";
        private const string DB_COL_SUSPENCION = "SUSPENCION";
        private const string DB_COL_FECHA_REACTIVACION = "FECHA_REACTIVACION";
        private const string DB_COL_EMPRESA = "EMPRESA";
        private const string DB_COL_NOMBRE_EMPRESA = "NOMBRE_EMPRESA";

        public SqlOperation GetCreateStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "CRE_SANCION_PR" };

            var s = (Sancion)entity;
            operation.AddVarcharParam(DB_COL_DESCRIPCION, s.Descripcion);
            operation.AddIntParam(DB_COL_MULTA, s.Multa);
            operation.AddVarcharParam(DB_COL_ESTADO, "Activo");
            operation.AddIntParam(DB_COL_TERMINAL_ID, s.TerminalId);
            operation.AddDateTimeParam(DB_COL_FECHA, s.Fecha);
            operation.AddVarcharParam(DB_COL_SUSPENCION, s.Suspencion);
            operation.AddDateTimeParam(DB_COL_FECHA_REACTIVACION, s.FechaReactivacion);
            operation.AddIntParam(DB_COL_EMPRESA, s.Empresa);

            return operation;
        }

        public SqlOperation GetDeleteStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "DEL_SANCION_PR" };

            var sancion = (Sancion)entity;
            operation.AddIntParam(DB_COL_ID, sancion.Id);

            return operation;
        }

        public SqlOperation GetRetriveAllStatement()
        {
            return new SqlOperation { ProcedureName = "RET_ALL_SANCION_PR" };
        }

        public SqlOperation GetRetriveAllStatementActivas()
        {
            return new SqlOperation { ProcedureName = "RET_ALL_SANCION_ACTIVAS_PR" };
        }

        public SqlOperation GetRetriveStatement(BaseEntity entity)
        {
            throw new NotImplementedException();
        }

        public SqlOperation GetUpdateStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "UPD_SANCION_PR" };

            var s = (Sancion)entity;

            operation.AddIntParam(DB_COL_ID, s.Id);
            operation.AddVarcharParam(DB_COL_DESCRIPCION, s.Descripcion);
            operation.AddIntParam(DB_COL_MULTA, s.Multa);
            operation.AddVarcharParam(DB_COL_ESTADO, s.Estado);
            operation.AddIntParam(DB_COL_TERMINAL_ID, s.TerminalId);
            operation.AddDateTimeParam(DB_COL_FECHA, s.Fecha);
            operation.AddVarcharParam(DB_COL_SUSPENCION, s.Suspencion);
            operation.AddDateTimeParam(DB_COL_FECHA_REACTIVACION, s.FechaReactivacion);
            operation.AddIntParam(DB_COL_EMPRESA, s.Empresa);

            return operation;
        }

        public BaseEntity BuildObject(Dictionary<string, object> row)
        {
            var sancion = new Sancion
            {
                Id = GetIntValue(row, DB_COL_ID),
                Descripcion = GetStringValue(row, DB_COL_DESCRIPCION),
                Multa = GetIntValue(row, DB_COL_MULTA),
                Estado = GetStringValue(row, DB_COL_ESTADO),
                TerminalId = GetIntValue(row, DB_COL_TERMINAL_ID),
                Fecha = GetDateValue(row, DB_COL_FECHA),
                Suspencion = GetStringValue(row, DB_COL_SUSPENCION),
                FechaReactivacion = GetDateValue(row, DB_COL_FECHA_REACTIVACION),
                Empresa = GetIntValue(row, DB_COL_EMPRESA),
                NombreEmpresa = GetStringValue(row, DB_COL_NOMBRE_EMPRESA)
            };

            return sancion;
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
    }
}
