using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Dao;
using Entities;

namespace DataAccess.Mapper
{
    public class PagosPendientesMapper : EntityMapper, ISqlStaments, IObjectMapper
    {
        private const string DB_COL_PAGO_ID = "ID_PAGO";
        private const string DB_COL_LINEA_ID = "LINEA_ID";
        private const string DB_COL_MONTO = "MONTO";
        private const string DB_COL_ESTADO = "ESTADO_PAGO";
        private const string DB_COL_CEDULA_JURIDICA = "CEDULA_JURIDICA";
        private const string DB_COL_FECHA = "FECHA";
        private const string DB_COL_DESCRIPCION = "DESCRIPCION";


        public SqlOperation GetCreateStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "CRE_PAGOPENDIENTE_PR" };

            var pagoPendiente = (PagoPendiente)entity;
            operation.AddIntParam(DB_COL_MONTO, pagoPendiente.Monto);
            operation.AddIntParam(DB_COL_LINEA_ID, pagoPendiente.LineaId);
            operation.AddIntParam(DB_COL_CEDULA_JURIDICA, pagoPendiente.EmpresaId);

            return operation;
        }

        public SqlOperation GetRetriveStatement(BaseEntity entity)
        {
            throw new NotImplementedException();
        }

        public SqlOperation GetRetriveAllStatement()
        {
            return new SqlOperation { ProcedureName = "RET_ALL_TRANSACTIONS_PR" };
        }

        public SqlOperation GetUpdateStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "UPD_PAGOPENDIENTE_PR" };

            var pagoPendiente = (PagoPendiente)entity;
            operation.AddIntParam(DB_COL_PAGO_ID, pagoPendiente.IdPago);

            return operation;
        }

        public SqlOperation GetDeleteStatement(BaseEntity entity)
        {
            throw new NotImplementedException();
        }

       public SqlOperation GetRetriveAllByEmpresa(BaseEntity entity)
        {
            var pagoPendiente = (PagoPendiente)entity;
            var operation = new SqlOperation { ProcedureName = "RET_ALL_PAGOS_BY_EMPRESA" };
            operation.AddIntParam(DB_COL_CEDULA_JURIDICA, pagoPendiente.EmpresaId);

            return operation;
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
            return new PagoPendiente
            {
                IdPago = GetIntValue(row, DB_COL_PAGO_ID),
                LineaId = GetIntValue(row, DB_COL_LINEA_ID),
                Descripcion = GetStringValue(row,DB_COL_DESCRIPCION),
                EmpresaId = GetIntValue(row,DB_COL_CEDULA_JURIDICA),
                EstadoPago = GetStringValue(row, DB_COL_ESTADO),
                Fecha = GetDateValue(row, DB_COL_FECHA).ToShortDateString(),
                Monto = GetIntValue(row,DB_COL_MONTO)
            };
        }
    }
}
