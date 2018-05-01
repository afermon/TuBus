using System;
using DataAccess.Dao;
using Entities;
using System.Collections.Generic;

namespace DataAccess.Mapper
{
    public class MultaMapper : EntityMapper, ISqlStaments, IObjectMapper
    {
        private const string DB_COL_ID = "MULTA_ID";
        private const string DB_COL_EMPRESA = "EMPRESA";
        private const string DB_COL_MONTO = "MONTO";
        private const string DB_COL_FECHA = "FECHA";
        private const string DB_COL_DETALLE = "DETALLE";
        private const string DB_COL_ESTADO = "ESTADO";
        private const string DB_COL_NOMBRE_EMPRESA = "NOMBRE_EMPRESA";

        public SqlOperation GetCreateStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "CRE_MULTA_PR" };

            var m = (Multa)entity;
            operation.AddIntParam(DB_COL_EMPRESA, m.Empresa);
            operation.AddIntParam(DB_COL_MONTO, m.Monto);
            operation.AddDateParam(DB_COL_FECHA, m.Fecha);
            operation.AddVarcharParam(DB_COL_DETALLE, m.Detalle);
            operation.AddVarcharParam(DB_COL_ESTADO, "Pendiente");

            return operation;
        }
              

        public SqlOperation GetRetriveStatement(BaseEntity entity)
        {
            throw new NotImplementedException();
        }

        public SqlOperation GetRetriveAllStatement()
        {
            return new SqlOperation { ProcedureName = "RET_ALL_MULTA_PR" };
        }

        public SqlOperation GetUpdateStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "UPD_MULTA_PR" };

            var m = (Multa)entity;
            operation.AddIntParam(DB_COL_ID, m.Id);
            operation.AddIntParam(DB_COL_EMPRESA, m.Empresa);
            operation.AddIntParam(DB_COL_MONTO, m.Monto);
            operation.AddDateParam(DB_COL_FECHA, m.Fecha);
            operation.AddVarcharParam(DB_COL_DETALLE, m.Detalle);
            operation.AddVarcharParam(DB_COL_ESTADO, m.Estado);

            return operation;
        }

        public SqlOperation GetDeleteStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "DEL_MULTA_PR" };

            var multa = (Multa)entity;
            operation.AddIntParam(DB_COL_ID, multa.Id);

            return operation;
        }

        public BaseEntity BuildObject(Dictionary<string, object> row)
        {
            var multa = new Multa
            {
                Id = GetIntValue(row, DB_COL_ID),
                Empresa = GetIntValue(row, DB_COL_EMPRESA),
                Monto = GetIntValue(row, DB_COL_MONTO),
                Fecha = GetDateValue(row, DB_COL_FECHA),
                Detalle = GetStringValue(row, DB_COL_DETALLE),
                Estado = GetStringValue(row, DB_COL_ESTADO),
                NombreEmpresa = GetStringValue(row, DB_COL_NOMBRE_EMPRESA)
            };

            return multa;
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
