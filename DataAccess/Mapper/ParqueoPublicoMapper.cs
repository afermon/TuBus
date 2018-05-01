using System;
using System.Collections.Generic;
using DataAccess.Dao;
using Entities;

namespace DataAccess.Mapper
{
    public class ParqueoPublicoMapper : EntityMapper, ISqlStaments, IObjectMapper
    {
        private const string DB_COL_REGISTRO_ID = "REGISTRO_PARQUEO_ID";
        private const string DB_COL_TERMINAL_ID = "TERMINAL_ID";
        private const string DB_COL_COSTO_TOTAL = "COSTO_TOTAL";
        private const string DB_COL_HORA_INICIO = "HORA_INICIO";
        private const string DB_COL_HORA_FINAL = "HORA_FIN";
        private const string DB_COL_TARJETA_ID = "TARJETA_ID";
        private const string DB_COL_ESPACIOS_DISPONIBLES = "ESPACIOSDISPONIBLES";
        private const string DB_COL_ESPACIOS_TOTALES = "ESPACIO_TOTALES";
        private const string DB_COL_COSTO_PARQUEO_DIA = "COSTO_PARQUEO_DIA";
        private const string DB_COL_COSTO_PARQUEO_HORA = "COSTO_PARQUEO_HORA";

        public SqlOperation GetCreateStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "CRE_REGISTRO_PARQUEO_PR" };

            var r = (RegistroParqueo)entity;
            operation.AddIntParam(DB_COL_TERMINAL_ID, r.TerminalId);
            operation.AddDateTimeParam(DB_COL_HORA_INICIO, r.HoraInicio);
            operation.AddVarcharParam(DB_COL_TARJETA_ID, r.TarjetaId);

            return operation;
        }

        public SqlOperation GetRetriveStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "RET_REGISTRO_PARQUEO_ACTIVO_PR" };
            var registro = (RegistroParqueo)entity;

            operation.AddVarcharParam(DB_COL_TARJETA_ID, registro.TarjetaId);
            operation.AddIntParam(DB_COL_TERMINAL_ID, registro.TerminalId);

            return operation;
        }

        public SqlOperation GetEspaciosDisponiblesStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "RET_ESPACIOS_DISPONIBLES_PR" };
            var registro = (RegistroParqueo)entity;

            operation.AddIntParam(DB_COL_TERMINAL_ID, registro.TerminalId);

            return operation;
        }

        public SqlOperation GetRetriveAllStatement()
        {
            return new SqlOperation { ProcedureName = "RET_ALL_REGISTRO_PARQUEO_PR" };
        }

        public SqlOperation GetUpdateStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "CRE_SALIDA_PARQUEO_PR" };

            var r = (RegistroParqueo)entity;
            operation.AddVarcharParam(DB_COL_TARJETA_ID, r.TarjetaId);
            operation.AddIntParam(DB_COL_TERMINAL_ID, r.TerminalId);
            operation.AddDateTimeParam(DB_COL_HORA_FINAL, r.HoraFin);
            operation.AddDecimalParam(DB_COL_COSTO_TOTAL, r.CostoTotal);

            return operation;
        }

        public SqlOperation GetDeleteStatement(BaseEntity entity)
        {
            throw new NotImplementedException();
        }

        public List<BaseEntity> BuildObjects(List<Dictionary<string, object>> lstRows)
        {
            throw new NotImplementedException();
        }

        public BaseEntity BuildObject(Dictionary<string, object> row)
        {
            var parqueo = new RegistroParqueo
            {
                EspaciosDisponibles = GetIntValue(row, DB_COL_ESPACIOS_DISPONIBLES)
            };

            return parqueo;
        }

        public BaseEntity BuildCompleteObject(Dictionary<string, object> row)
        {
            var parqueo = new RegistroParqueo
            {
                RegistroId = GetIntValue(row, DB_COL_REGISTRO_ID),
                TerminalId = GetIntValue(row, DB_COL_TERMINAL_ID),
                CostoTotal = GetIntValue(row, DB_COL_COSTO_TOTAL),
                HoraInicio = GetDateValue(row, DB_COL_HORA_INICIO),
                HoraFin = GetDateValue(row, DB_COL_HORA_FINAL),
                TarjetaId = GetStringValue(row, DB_COL_TARJETA_ID)
            };

            return parqueo;
        }
    }
}
