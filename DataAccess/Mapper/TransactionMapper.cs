using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Dao;
using Entities;

namespace DataAccess.Mapper
{
    class TransactionMapper : EntityMapper, ISqlStaments, IObjectMapper
    {
        private const string DB_COL_TRANSACTION_ID = "TRANSACCION_ID";
        private const string DB_COL_MONTO = "MONTO";
        private const string DB_COL_TIPO = "TIPO";
        private const string DB_COL_CONCEPTO = "CONCEPTO";
        private const string DB_COL_ESTADO = "ESTADO";
        private const string DB_COL_TARJETA_ID = "TARJETA_ID";
        private const string DB_COL_LINEA_ID = "LINEA_ID";
        private const string DB_COL_TERMINAL_ID = "TERMINAL_ID";
        private const string DB_COL_EMAIL = "EMAIL";
        private const string DB_COL_FECHA = "FECHA";


        public SqlOperation GetCreateStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "CRE_TRANSACCION_PR" };

            var transaccion = (Transaccion)entity;
            operation.AddIntParam(DB_COL_MONTO, transaccion.Charge);
            operation.AddVarcharParam(DB_COL_TIPO, transaccion.Type);
            operation.AddVarcharParam(DB_COL_CONCEPTO, transaccion.Description);
            operation.AddVarcharParam(DB_COL_ESTADO, transaccion.Status);

            if (transaccion.CardUniqueCode != null) 
                operation.AddVarcharParam(DB_COL_TARJETA_ID, transaccion.CardUniqueCode);

            if (transaccion.LineaId != 0  && transaccion.LineaId != -1)
                operation.AddIntParam(DB_COL_LINEA_ID, transaccion.LineaId);

            if (transaccion.Terminal != null && transaccion.Terminal.Id != 0 && transaccion.Terminal.Id != -1)
                operation.AddIntParam(DB_COL_TERMINAL_ID, transaccion.Terminal.Id);

            return operation;
        }

        public SqlOperation GetRetriveStatement(BaseEntity entity)
        {
            throw new NotImplementedException();
        }

        public SqlOperation GetRetriveAllStatement()
        {
            return new SqlOperation{ProcedureName = "RET_ALL_TRANSACTIONS_PR" };
        }

        public SqlOperation GetUpdateStatement(BaseEntity entity)
        {
            throw new NotImplementedException();
        }

        public SqlOperation GetDeleteStatement(BaseEntity entity)
        {
            throw new NotImplementedException();
        }

        public SqlOperation GetRetriveAllByUser(BaseEntity entity)
        {
            var transaccion = (Transaccion)entity;
            var operation = new SqlOperation { ProcedureName = "RET_ALL_TRANSACTIONS_BY_USER" };
            operation.AddVarcharParam(DB_COL_EMAIL, transaccion.Usuario.Email);

            return operation;
        }

        public SqlOperation GetRetriveAllByTerminal(BaseEntity entity)
        {
            var transaccion = (Transaccion)entity;
            var operation = new SqlOperation { ProcedureName = "RET_ALL_TRANSACTIONS_BY_TERMID" };
            operation.AddIntParam(DB_COL_TERMINAL_ID, transaccion.Terminal.Id);

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
            return new Transaccion
            {
                TransactionId = GetIntValue(row, DB_COL_TRANSACTION_ID),
                Charge = GetIntValue(row, DB_COL_MONTO),
                Type = GetStringValue(row, DB_COL_TIPO),
                Description = GetStringValue(row, DB_COL_CONCEPTO),
                Status = GetStringValue(row, DB_COL_ESTADO),
                CardUniqueCode = GetStringValue(row, DB_COL_TARJETA_ID),
                LineaId = GetIntValue(row, DB_COL_LINEA_ID),
                Terminal = new Terminal { Id = GetIntValue(row, DB_COL_TERMINAL_ID)},
                Usuario = new Usuario { Email = GetStringValue(row, DB_COL_EMAIL)},
                Fecha  = GetDateValue(row, DB_COL_FECHA).ToShortDateString()
            };
        }
    }
}
