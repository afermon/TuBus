using System;
using System.Collections.Generic;
using DataAccess.Dao;
using Entities;

namespace DataAccess.Mapper
{
    public class RecorridoMapper : EntityMapper, ISqlStaments, IObjectMapper
    {
        public const string DB_COL_ID = "RECORRIDO_ID";
        public const string DB_COL_HORA_SALIDA = "HORA_SALIDA";
        public const string DB_COL_HORA_LLEGADA = "HORA_LLEGADA";
        public const string DB_COL_BUS_PLACA = "BUS_PLACA";
        public const string DB_COL_CHOFER_CEDULA = "CHOFER_CEDULA";
        public const string DB_COL_RUTA_ID = "RUTA_ID";
        public const string DB_COL_CANTIDAD_PASAJEROS = "CANTIDAD_PASAJEROS";
        public const string DB_COL_HORARIO = "HORARIO";
        public const string DB_COL_TARDIA = "TARDIA";

        public SqlOperation GetCreateStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "CRE_RECORRIDO_PR" };

            var r = (Recorrido)entity;
            operation.AddVarcharParam(DB_COL_BUS_PLACA, r.BusPlaca);
            operation.AddVarcharParam(DB_COL_CHOFER_CEDULA, r.ChoferCedula);
            operation.AddIntParam(DB_COL_RUTA_ID, r.RutaId);
            operation.AddTimeSpamParam(DB_COL_HORARIO, r.Horario);
            return operation;
        }

        public SqlOperation GetDeleteStatement(BaseEntity entity)
        {
            throw new NotSupportedException();
        }

        public SqlOperation GetRetriveAllStatement()
        {
            var operation = new SqlOperation { ProcedureName = "RET_ALL_RECORRIDO_PR" };
            return operation;
        }

        public SqlOperation GetRetriveTardiaMesStatement()
        {
            var operation = new SqlOperation { ProcedureName = "RET_RECORRIDO_TARDIA_MES_PR" };
            return operation;
        }

        public SqlOperation GetRetriveStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "RET_RECORRIDO_PR" };

            var r = (Recorrido)entity;
            operation.AddIntParam(DB_COL_ID, r.RecorridoId);

            return operation;
        }

        public SqlOperation GetUpdateStatement(BaseEntity entity)
        {
            throw new NotSupportedException();
        }

        public SqlOperation GetUpdateSalidaStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "UPD_RECORRIDO_SALIDA_PR" };
            var r = (Recorrido)entity;
            operation.AddIntParam(DB_COL_ID, r.RecorridoId);
            operation.AddDateTimeParam(DB_COL_HORA_SALIDA, r.HoraSalida );
            operation.AddIntParam(DB_COL_TARDIA, r.MinutosTarde);
            return operation;
        }

        public SqlOperation GetUpdateLlegadaStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "UPD_RECORRIDO_LLEGADA_PR" };
            var r = (Recorrido)entity;
            operation.AddIntParam(DB_COL_ID, r.RecorridoId);
            operation.AddDateTimeParam(DB_COL_HORA_LLEGADA, r.HoraLlegada);
            return operation;
        }

        public SqlOperation GetUpdatePasajerosStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "UPD_RECORRIDO_PASAJEROS_PR" };
            var r = (Recorrido)entity;
            operation.AddIntParam(DB_COL_ID, r.RecorridoId);
            operation.AddIntParam(DB_COL_CANTIDAD_PASAJEROS, r.CantidadPasajeros);
            return operation;
        }

        public BaseEntity BuildObject(Dictionary<string, object> row)
        {
            var ruta = new Recorrido
            {
                RecorridoId = GetIntValue(row, DB_COL_ID),
                RutaId = GetIntValue(row, DB_COL_RUTA_ID),
                ChoferCedula = GetStringValue(row, DB_COL_CHOFER_CEDULA),
                BusPlaca = GetStringValue(row, DB_COL_BUS_PLACA),
                HoraSalida = GetDateValue(row, DB_COL_HORA_SALIDA),
                HoraLlegada = GetDateValue(row, DB_COL_HORA_SALIDA),
                CantidadPasajeros = GetIntValue(row, DB_COL_CANTIDAD_PASAJEROS),
                Horario = GetTimeValue(row, DB_COL_HORARIO),
                MinutosTarde = GetIntValue(row, DB_COL_TARDIA)
            };

            return ruta;
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
