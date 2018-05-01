using System.Collections.Generic;
using DataAccess.Dao;
using Entities;

namespace DataAccess.Mapper
{
    public class QuejaMapper : EntityMapper, ISqlStaments, IObjectMapper
    {
        public const string DB_COL_ID = "QUEJAS_ID";
        public const string DB_COL_DETALLE = "DETALLE_QUEJA";
        public const string DB_COL_RUTA = "RUTA";
        public const string DB_COL_CHOFER = "CHOFER";
        public const string DB_COL_PLACA = "PLACA";
        public const string DB_COL_HORA = "HORA";
        public const string DB_COL_ESTADO = "ESTADO";
        
        public SqlOperation GetCreateStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "CRE_QUEJA_PR" };

            var q = (Queja)entity;
            operation.AddVarcharParam(DB_COL_DETALLE, q.DetalleQueja);
            operation.AddIntParam(DB_COL_RUTA, q.Ruta);
            operation.AddVarcharParam(DB_COL_CHOFER, q.Chofer);
            operation.AddVarcharParam(DB_COL_PLACA, q.Placa);
            operation.AddDateTimeParam(DB_COL_HORA, q.Hora);
            operation.AddVarcharParam(DB_COL_ESTADO, "Activo");

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
            var queja = new Queja
            {
                Id = GetIntValue(row, DB_COL_ID),
                DetalleQueja = GetStringValue(row, DB_COL_DETALLE),
                Ruta = GetIntValue(row, DB_COL_RUTA),
                Chofer = GetStringValue(row, DB_COL_CHOFER),
                Placa = GetStringValue(row, DB_COL_PLACA),
                Hora = GetDateValue(row, DB_COL_HORA),
                Estado = GetStringValue(row, DB_COL_ESTADO)
                
            };

            return queja;
        }

        public SqlOperation GetRetriveStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "RET_QUEJAS_BY_ID_PR" };

            var queja = (Queja)entity;
            operation.AddIntParam(DB_COL_ID, queja.Id);

            return operation;
        }

        public SqlOperation GetRetriveAllStatement()
        {
            return new SqlOperation { ProcedureName = "RET_ALL_QUEJA_PR" };
        }

        public SqlOperation GetRetriveQuejasActivasStatement()
        {
            return new SqlOperation { ProcedureName = "RET_QUEJAS_ACTIVA_PR" };
        }

        public SqlOperation GetRetriveQuejasActivasByRutaStatement(int ruta)
        {
            var operation = new SqlOperation { ProcedureName = "RET_QUEJAS_ACTIVA_BY_RUTA_PR" };
                        
            operation.AddIntParam(DB_COL_RUTA, ruta);

            return operation;
        }

        public SqlOperation GetUpdateStatement(BaseEntity entity)
        {
            throw new System.NotImplementedException();
        }

        public SqlOperation GetDeleteStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "DEL_QUEJA_PR" };

            var queja = (Queja)entity;
            operation.AddIntParam(DB_COL_ID, queja.Id);

            return operation;
        }
    }
}
