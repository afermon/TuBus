using System;
using DataAccess.Dao;
using Entities;
using System.Collections.Generic;


namespace DataAccess.Mapper
{
    public class TerminalMapper : EntityMapper, ISqlStaments, IObjectMapper
    {
        private const string DB_COL_ID = "TERMINAL_ID";
        private const string DB_COL_NOMBRE = "TERMINAL_NAME";
        private const string DB_COL_LATITUD = "LATITUDE";
        private const string DB_COL_LONGITUD = "LONGITUDE";
        private const string DB_COL_CANTIDAD_LINEAS = "CANTIDAD_LINEAS";
        private const string DB_COL_ESTADO = "ESTADO";
        private const string DB_COL_ESPACIOS_PARQUEO = "ESPACIOS_PARQUEO";
        private const string DB_COL_ESPACIOS_PARQUEO_BUS = "ESPACIOS_PARQUEO_BUS";


        public SqlOperation GetCreateStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "CRE_TERMINAL_PR" };

            var t = (Terminal)entity;
            operation.AddVarcharParam(DB_COL_NOMBRE, t.TerminalName);
            operation.AddDecimalParam(DB_COL_LATITUD, t.Latitude);
            operation.AddDecimalParam(DB_COL_LONGITUD, t.Longitude);
            operation.AddIntParam(DB_COL_CANTIDAD_LINEAS, t.CantidadLineas);
            operation.AddVarcharParam(DB_COL_ESTADO, "Activo");
            operation.AddIntParam(DB_COL_ESPACIOS_PARQUEO, t.EspaciosParqueo);
            operation.AddIntParam(DB_COL_ESPACIOS_PARQUEO_BUS, t.EspaciosParqueoBus);

            return operation;
        }


        public SqlOperation GetRetriveStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "RET_TERMINAL_BY_ID_PR" };

            var terminal = (Terminal)entity;
            operation.AddIntParam(DB_COL_ID, terminal.Id);

            return operation;
        }

        public SqlOperation GetRetriveByNameStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "RET_TERMINAL_BY_NAME_PR" };

            var terminal = (Terminal)entity;
            operation.AddVarcharParam(DB_COL_NOMBRE, terminal.TerminalName);

            return operation;
        }

        public SqlOperation GetRetriveAllStatement()
        {
            return new SqlOperation { ProcedureName = "RET_ALL_TERMINAL_PR" };
        }

        public SqlOperation GetUpdateStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "UPD_TERMINAL_PR" };

            var t = (Terminal)entity;
            operation.AddIntParam(DB_COL_ID, t.Id);
            operation.AddVarcharParam(DB_COL_NOMBRE, t.TerminalName);
            operation.AddDecimalParam(DB_COL_LATITUD, t.Latitude);
            operation.AddDecimalParam(DB_COL_LONGITUD, t.Longitude);
            operation.AddIntParam(DB_COL_CANTIDAD_LINEAS, t.CantidadLineas);
            operation.AddVarcharParam(DB_COL_ESTADO, t.Estado);
            operation.AddIntParam(DB_COL_ESPACIOS_PARQUEO, t.EspaciosParqueo);
            operation.AddIntParam(DB_COL_ESPACIOS_PARQUEO_BUS, t.EspaciosParqueoBus);

            return operation;
        }

        public SqlOperation GetDeleteStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "DEL_TERMINAL_PR" };

            var terminal = (Terminal)entity;
            operation.AddIntParam(DB_COL_ID, terminal.Id);

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
            var terminal = new Terminal
            {
                Id = GetIntValue(row, DB_COL_ID),
                TerminalName = GetStringValue(row, DB_COL_NOMBRE),
                Latitude = GetDecimalValue(row, DB_COL_LATITUD),
                Longitude = GetDecimalValue(row, DB_COL_LONGITUD),
                CantidadLineas = GetIntValue(row, DB_COL_CANTIDAD_LINEAS),
                Estado = GetStringValue(row, DB_COL_ESTADO),
                EspaciosParqueo = GetIntValue(row, DB_COL_ESPACIOS_PARQUEO),
                EspaciosParqueoBus = GetIntValue(row, DB_COL_ESPACIOS_PARQUEO_BUS)
            };

            return terminal;
        }
    }
}
