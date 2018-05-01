using System;
using System.Collections.Generic;
using DataAccess.Dao;
using Entities;

namespace DataAccess.Mapper
{
    public class RequisitoMapper : EntityMapper, ISqlStaments, IObjectMapper
    {
        private const string DB_COL_PERMISO = "PERMISO";
        private const string DB_COL_PERMISOS_ID = "PERMISOS_ID";
        private const string DB_COL_PLACA = "PLACA";
        private const string DB_COL_ESTADO = "ESTADO";

        public SqlOperation GetCreateStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "CRE_REQUISITO_PR" };

            var requisito = (Requisito)entity;

            operation.AddVarcharParam(DB_COL_PERMISO, requisito.Permiso);
            operation.AddVarcharParam(DB_COL_ESTADO, requisito.Estado);
            operation.AddVarcharParam(DB_COL_PLACA, requisito.Placa);

            return operation;
        }

        public SqlOperation GetRetriveStatement(BaseEntity entity)
        {
            throw new NotImplementedException();
        }

        public SqlOperation GetRetriveAllByPlacaStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "RET_REQUISITOS_POR_PLACA_PR" };
            var requisito = (Requisito)entity;

            operation.AddVarcharParam(DB_COL_PLACA, requisito.Placa);

            return operation;
        }

        public SqlOperation GetRetriveAllStatement()
        {
            return new SqlOperation { ProcedureName = "RET_ALL_REQUISITO_PR" };
        }

        public SqlOperation GetUpdateStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "UPD_REQUISITO_PR" };

            var requisito = (Requisito)entity;

            operation.AddVarcharParam(DB_COL_PERMISO, requisito.Permiso);
            operation.AddVarcharParam(DB_COL_PLACA, requisito.Placa);
            operation.AddVarcharParam(DB_COL_ESTADO, requisito.Estado);

            return operation;
        }

        public SqlOperation GetDeleteStatement(BaseEntity entity)
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
            var requisito = new Requisito
            {
                Id = GetIntValue(row, DB_COL_PERMISOS_ID),
                Permiso = GetStringValue(row, DB_COL_PERMISO),
                Placa = GetStringValue(row, DB_COL_PLACA),
                Estado = GetStringValue(row, DB_COL_ESTADO)
            };

            return requisito;
        }
    }
}
