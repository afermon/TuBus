using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Dao;
using Entities;

namespace DataAccess.Mapper
{
    public class LineaMapper : EntityMapper, ISqlStaments, IObjectMapper
    {
        private const string DB_COL_LINEA_ID = "LINEA_ID";
        private const string DB_COL_NOMBRE_LINEA = "NOMBRE_LINEA";
        private const string DB_COL_ESTADO = "ESTADO";
        private const string DB_COL_TERMINAL_ID = "TERMINAL_ID";
        private const string DB_COL_EMPRESA = "EMPRESA_ID";
        private const string DB_COL_ESPACIOS = "ESPACIOS_PARQUEO";

        public SqlOperation GetCreateStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "CRE_LINEA_PR" };

            var linea = (Linea)entity;
            operation.AddVarcharParam(DB_COL_NOMBRE_LINEA, linea.NombreLinea);
            operation.AddIntParam(DB_COL_TERMINAL_ID, linea.Terminal.Id);
            operation.AddIntParam(DB_COL_EMPRESA, linea.Empresa.CedulaJuridica);
            operation.AddIntParam(DB_COL_ESPACIOS, linea.EspaciosParqueo);

            return operation;
        }

        public SqlOperation GetRetriveStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "RET_LINEA_BY_ID" };

            var linea = (Linea)entity;
            operation.AddIntParam(DB_COL_LINEA_ID, linea.LineaId);

            return operation;
        }

        public SqlOperation GetRetriveAllStatement()
        {
            return new SqlOperation { ProcedureName = "RET_ALL_LINEA_PR" };
        }

        public SqlOperation GetAllSpaces(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "RET_ESPACIO_DISPONIBLE_PARQUEO" };
            var linea = (Linea)entity;
            operation.AddIntParam(DB_COL_TERMINAL_ID, linea.Terminal.Id);
            return operation;
        }
        
        public SqlOperation GetUpdateStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "UPD_LINEA_PR" };

            var linea = (Linea)entity;
            operation.AddIntParam(DB_COL_LINEA_ID, linea.LineaId);
            operation.AddVarcharParam(DB_COL_NOMBRE_LINEA, linea.NombreLinea);
            operation.AddIntParam(DB_COL_TERMINAL_ID, linea.Terminal.Id);
            operation.AddIntParam(DB_COL_EMPRESA, linea.Empresa.CedulaJuridica);
            operation.AddIntParam(DB_COL_ESPACIOS, linea.EspaciosParqueo);

            return operation;
        }

        public SqlOperation GetDeleteStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "DEL_LINEA_PR" };

            var linea = (Linea)entity;
            operation.AddIntParam(DB_COL_LINEA_ID, linea.LineaId);

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
            return new Linea
            {
                LineaId = GetIntValue(row, DB_COL_LINEA_ID),
                NombreLinea = GetStringValue(row, DB_COL_NOMBRE_LINEA),
                Estado = GetStringValue(row, DB_COL_ESTADO),
                Terminal = new Terminal {Id = GetIntValue(row, DB_COL_TERMINAL_ID)},
                Empresa = new Empresa {CedulaJuridica = GetIntValue(row, DB_COL_EMPRESA)},
                EspaciosParqueo = GetIntValue(row, DB_COL_ESPACIOS)
            };
        }

        public BaseEntity BuildSpaces(Dictionary<string, object> row)
        {
            return new Linea
            {
                EspaciosParqueo = GetIntValue(row, DB_COL_ESPACIOS)
            };
        }
    }
}
