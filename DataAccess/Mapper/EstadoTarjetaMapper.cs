using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Dao;
using Entities;

namespace DataAccess.Mapper
{
    public class EstadoTarjetaMapper : EntityMapper, ISqlStaments, IObjectMapper
    {
        private const string DB_COL_ID = "TBL_ESTADO_TARJETA_ID";
        private const string DB_COL_NOMBRE = "NOMBRE_ESTADO";
        public SqlOperation GetCreateStatement(BaseEntity entity)
        {
            throw new NotImplementedException();
        }

        public SqlOperation GetRetriveStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "RET_ESTADO_BY_ID" };

            var estadoTarjeta = (EstadoTarjeta)entity;
            operation.AddIntParam(DB_COL_ID, estadoTarjeta.EstadoTarjetaId);

            return operation;
        }

        public SqlOperation GetRetriveAllStatement()
        {
            return new SqlOperation { ProcedureName = "RET_ALL_ESTADOS_TARJETA" };
        }

        public SqlOperation GetUpdateStatement(BaseEntity entity)
        {
            throw new NotImplementedException();
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
                var tarjeta = BuildObject(row);
                lstResults.Add(tarjeta);
            }

            return lstResults;
        }

        public BaseEntity BuildObject(Dictionary<string, object> row)
        {
            return new EstadoTarjeta
            {
                EstadoTarjetaId = GetIntValue(row, DB_COL_ID),
                NombreEstado = GetStringValue(row, DB_COL_NOMBRE)
            };
        }
    }
}
