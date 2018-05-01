using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Dao;
using Entities;

namespace DataAccess.Mapper
{
    public class ReportMapper : EntityMapper
    {
        private const string DB_COL_GANANCIAS = "GANANCIAS";
        private const string DB_COL_FECHA = "FECHA";
        private const string DB_COL_NOMBRE = "NOMBRE";
        private const string DB_COL_CANTIDAD = "CANTIDAD";
        private const string DB_COL_EMAIL = "EMAIL";

        public SqlOperation GetRetriveAllBenefitsStatement()
        {
            return new SqlOperation { ProcedureName = "RET_ALL_GANANCIAS_ALL_TERMINALES" };
        }

        public SqlOperation GetRetriveStatement()
        {
            return new SqlOperation { ProcedureName = "RET_ALL_COUNT_TIPO_TRANSACTION" };
        }

        public SqlOperation GetRetriveTipoTarjetasStatement()
        {
            return new SqlOperation { ProcedureName = "RET_GANACIAS_ALL_TIPOS_TARJETAS" };
        }

        public SqlOperation GetRetriveMovement(string entity)
        {
            var operation = new SqlOperation { ProcedureName = "RET_MOVIMIENTOS_USUARIO" };
            operation.AddVarcharParam(DB_COL_EMAIL, entity);

            return operation;
        }

        public List<BaseEntity> BuildObjects(List<Dictionary<string, object>> lstRows, bool getOnlyTwo = false, bool pasajeroPbj = false)
        {
            var lstResults = new List<BaseEntity>();

            foreach (var row in lstRows)
            {
                var item = !pasajeroPbj ? BuildObject(row, getOnlyTwo) : BuildPasajeroObject(row);
                lstResults.Add(item);
            }

            return lstResults;
        }

        public BaseEntity BuildObject(Dictionary<string, object> row, bool getOnlyTwo)
        {
            var ganacias = !getOnlyTwo ? Convert.ToInt32(GetDoubleValue(row, DB_COL_GANANCIAS)) : -1;
            var requisito = new Report
            {
                Label = GetStringValue(row, DB_COL_NOMBRE),
                Value = ganacias != -1 ? ganacias : GetIntValue(row, DB_COL_CANTIDAD),
                AdditionalValue = !getOnlyTwo ? GetStringValue(row, DB_COL_FECHA) : null
            };

            return requisito;
        }

        public BaseEntity BuildPasajeroObject(Dictionary<string, object> row)
        {
            var requisito = new Report
            {
                Label = GetStringValue(row, DB_COL_NOMBRE),
                Value =  GetIntValue(row, DB_COL_CANTIDAD),
                AdditionalValue =  GetStringValue(row, DB_COL_FECHA) 
            };

            return requisito;
        }
    }
}
