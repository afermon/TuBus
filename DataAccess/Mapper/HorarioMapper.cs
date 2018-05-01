using System;
using System.Collections.Generic;
using DataAccess.Dao;
using Entities;

namespace DataAccess.Mapper
{
    class HorarioMapper : EntityMapper, ISqlStaments, IObjectMapper
    {
        public const string DB_COL_RUTA_ID = "RUTA_ID";
        public const string DB_COL_HORARIO_ID = "HORARIO_ID";
        public const string DB_COL_DIA_SEMANA = "DIA_SEMANA";
        public const string DB_COL_HORA_SALIDA = "HORA_SALIDA";

        public SqlOperation GetCreateStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "CRE_HORARIO_PR" };

            var h = (Horario)entity;
            operation.AddIntParam(DB_COL_RUTA_ID, h.RutaId);
            operation.AddTimeSpamParam(DB_COL_HORA_SALIDA, h.Hora);
            operation.AddIntParam(DB_COL_DIA_SEMANA, h.Dia);

            return operation;
        }

        public SqlOperation GetDeleteStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "DEL_HORARIO_PR" };

            var h = (Horario)entity;
            operation.AddIntParam(DB_COL_RUTA_ID, h.RutaId);

            return operation;
        }

        public SqlOperation GetRetriveAllStatement()
        {
            throw new NotSupportedException();
        }

        public SqlOperation GetRetriveStatement(BaseEntity entity)
        {
            throw new NotSupportedException();
        }

        public SqlOperation GetRetriveByRutaStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "RET_HORARIO_PR" };

            var h = (Horario)entity;
            operation.AddIntParam(DB_COL_RUTA_ID, h.RutaId);

            return operation;
        }

        public SqlOperation GetUpdateStatement(BaseEntity entity)
        {
            throw new NotSupportedException();
        }

        public BaseEntity BuildObject(Dictionary<string, object> row)
        {
            var horario = new Horario
            {
                HorarioId = GetIntValue(row, DB_COL_HORARIO_ID),
                RutaId = GetIntValue(row, DB_COL_RUTA_ID),
                Dia = GetIntValue(row, DB_COL_DIA_SEMANA),
                Hora = GetTimeValue(row, DB_COL_HORA_SALIDA)
            };

            return horario;
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
    }
}
