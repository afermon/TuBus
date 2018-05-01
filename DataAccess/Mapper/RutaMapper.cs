using System;
using System.Collections.Generic;
using DataAccess.Dao;
using Entities;

namespace DataAccess.Mapper
{
    class RutaMapper : EntityMapper, ISqlStaments, IObjectMapper
    {
        public const string DB_COL_ID = "RUTA_ID";
        public const string DB_COL_RUTA_NAME = "RUTA_NAME";
        public const string DB_COL_RUTA_DESCRIPCION = "RUTA_DESCRIPCION";
        public const string DB_COL_JSON_ROUTE = "JSON_ROUTE";
        public const string DB_COL_LATITUDE_DESTINO = "LATITUDE_DESTINO";
        public const string DB_COL_LONGITUDE_DESTINO = "LONGITUDE_DESTINO";
        public const string DB_COL_DISTANCIA = "DISTANCIA";
        public const string DB_COL_TERMINAL_ID = "TERMINAL_ID";
        public const string DB_COL_LINEA_ID = "LINEA_ID";
        public const string DB_COL_TARIFA_ID = "TARIFA_ID";
        public const string DB_COL_COSTO_KM = "COSTO_KM";
        public const string DB_COL_COSTO_TOTAL = "COSTO_TOTAL";
        public const string DB_COL_ESTADO = "ESTADO";
        public const string DB_COL_EMPRESA = "EMPRESA_ID";

        public SqlOperation GetCreateStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "CRE_RUTA_PR" };

            var r = (Ruta)entity;
            operation.AddVarcharParam(DB_COL_RUTA_DESCRIPCION, r.RutaDescripcion);
            operation.AddVarcharParam(DB_COL_JSON_ROUTE, r.JsonRoute);
            operation.AddDoubleParam(DB_COL_LONGITUDE_DESTINO, r.DestinoLongitude);
            operation.AddDoubleParam(DB_COL_LATITUDE_DESTINO, r.DestinoLatitude);
            operation.AddDoubleParam(DB_COL_DISTANCIA, r.Distancia);
            operation.AddIntParam(DB_COL_LINEA_ID, r.LineaId);
            operation.AddIntParam(DB_COL_TERMINAL_ID, r.TerminalId);
            operation.AddIntParam(DB_COL_TARIFA_ID, r.TarifaId);
            operation.AddVarcharParam(DB_COL_ESTADO, r.Estado);

            return operation;
        }

        public SqlOperation GetDeleteStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "DEL_RUTA_PR" };

            var r = (Ruta)entity;
            operation.AddIntParam(DB_COL_ID, r.Id);
            return operation;
        }

        public SqlOperation GetRetriveAllStatement()
        {
            var operation = new SqlOperation { ProcedureName = "RET_ALL_RUTA_PR" };
            return operation;
        }

        public SqlOperation GetRetriveStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "RET_RUTA_PR" };

            var r = (Ruta)entity;
            operation.AddIntParam(DB_COL_ID, r.Id);

            return operation;
        }

        public SqlOperation GetUpdateStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "UPD_RUTA_PR" };
            var r = (Ruta)entity;
            operation.AddIntParam(DB_COL_ID, r.Id);
            operation.AddVarcharParam(DB_COL_RUTA_DESCRIPCION, r.RutaDescripcion);
            operation.AddVarcharParam(DB_COL_JSON_ROUTE, r.JsonRoute);
            operation.AddDoubleParam(DB_COL_LONGITUDE_DESTINO, r.DestinoLongitude);
            operation.AddDoubleParam(DB_COL_LATITUDE_DESTINO, r.DestinoLatitude);
            operation.AddDoubleParam(DB_COL_DISTANCIA, r.Distancia);
            operation.AddIntParam(DB_COL_LINEA_ID, r.LineaId);
            operation.AddIntParam(DB_COL_TERMINAL_ID, r.TerminalId);
            operation.AddIntParam(DB_COL_TARIFA_ID, r.TarifaId);
            operation.AddVarcharParam(DB_COL_ESTADO, r.Estado);

            return operation;
        }

        public SqlOperation GetRetriveByTerminalStatement(BaseEntity entity)
        {

            var operation = new SqlOperation { ProcedureName = "RET_RUTA_TERMINAL_PR" };
            var r = (Ruta)entity;

            operation.AddIntParam(DB_COL_TERMINAL_ID, r.TerminalId);

            return operation;
        }

        public BaseEntity BuildObject(Dictionary<string, object> row)
        {
            var ruta = new Ruta
            {
                Id = GetIntValue(row, DB_COL_ID),
                RutaName = GetStringValue(row, DB_COL_RUTA_NAME),
                RutaDescripcion = GetStringValue(row, DB_COL_RUTA_DESCRIPCION),
                JsonRoute = GetStringValue(row, DB_COL_JSON_ROUTE),
                DestinoLatitude = GetDoubleValue(row, DB_COL_LATITUDE_DESTINO),
                DestinoLongitude = GetDoubleValue(row, DB_COL_LONGITUDE_DESTINO),
                Distancia = GetDoubleValue(row, DB_COL_DISTANCIA),
                TerminalId = GetIntValue(row, DB_COL_TERMINAL_ID),
                LineaId = GetIntValue(row, DB_COL_LINEA_ID),
                TarifaId = GetIntValue(row, DB_COL_TARIFA_ID),
                CostoKm = Math.Round(GetDoubleValue(row, DB_COL_COSTO_KM),2),
                CostoTotal = GetDoubleValue(row, DB_COL_COSTO_TOTAL),
                Estado = GetStringValue(row, DB_COL_ESTADO),
                EmpresaId = GetIntValue(row, DB_COL_EMPRESA)
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
