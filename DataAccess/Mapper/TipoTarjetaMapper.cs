using System.Collections.Generic;
using DataAccess.Dao;
using Entities;

namespace DataAccess.Mapper
{
    public class TipoTarjetaMapper : EntityMapper, ISqlStaments, IObjectMapper
    {
        private const string DB_COL_TIPOTARJETA_ID = "TIPOTARJETA_ID";
        private const string DB_COL_NOMBRE_TARJETA = "NOMBRE_TARJETA";
        private const string DB_COL_STATUS = "STATUS";
        private const string DB_COL_DISCOUNT = "DESCUENTO_TARIFA";

        public SqlOperation GetCreateStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "CRE_TIPO_TARJETA" };

            var tipoTarjeta = (TipoTarjeta)entity;
            operation.AddVarcharParam(DB_COL_NOMBRE_TARJETA, tipoTarjeta.Nombre);
            operation.AddIntParam(DB_COL_DISCOUNT, tipoTarjeta.DiscountPercentage);

            return operation;
        }

        public SqlOperation GetRetriveStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "RET_TIPO_TARJETA_BY_ID" };

            var tipoTarjeta = (TipoTarjeta)entity;
            operation.AddIntParam(DB_COL_TIPOTARJETA_ID, tipoTarjeta.TipoTarjetaId);
            operation.AddVarcharParam(DB_COL_NOMBRE_TARJETA, tipoTarjeta.Nombre);

            return operation;
        }

        public SqlOperation GetRetriveAllStatement()
        {
            return new SqlOperation { ProcedureName = "RET_LIST_TIPO_TARJETA" };
        }

        public SqlOperation GetUpdateStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "UPD_TIPO_TARJETA" };

            var tipoTarjeta = (TipoTarjeta)entity;
            operation.AddIntParam(DB_COL_TIPOTARJETA_ID, tipoTarjeta.TipoTarjetaId);
            operation.AddVarcharParam(DB_COL_NOMBRE_TARJETA, tipoTarjeta.Nombre);
            operation.AddIntParam(DB_COL_DISCOUNT, tipoTarjeta.DiscountPercentage);
            
            return operation;
        }

        public SqlOperation GetDeleteStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "DEL_TIPO_TARJETA" };

            var tipoTarjeta = (TipoTarjeta)entity;
            operation.AddIntParam(DB_COL_TIPOTARJETA_ID, tipoTarjeta.TipoTarjetaId);

            return operation;
        }

        public List<BaseEntity> BuildObjects(List<Dictionary<string, object>> lstRows)
        {
            var lstResults = new List<BaseEntity>();

            foreach (var row in lstRows)
            {
                var tipoTarjeta = BuildObject(row);
                lstResults.Add(tipoTarjeta);
            }

            return lstResults;
        }

        public BaseEntity BuildObject(Dictionary<string, object> row)
        {
           return new TipoTarjeta
            {
                TipoTarjetaId = GetIntValue(row, DB_COL_TIPOTARJETA_ID),
                Nombre = GetStringValue(row, DB_COL_NOMBRE_TARJETA),
                Estado = GetBoolValue(row,DB_COL_STATUS) ? "Activo" : "Inactivo",
                DiscountPercentage = GetIntValue(row,DB_COL_DISCOUNT)
            };
        }
    }
}
