using System.Collections.Generic;
using DataAccess.Dao;
using Entities;

namespace DataAccess.Mapper
{
    public class ConfiguracionTerminalMapper : EntityMapper, ISqlStaments, IObjectMapper
    {
        private const string DB_COL_TERMINAL_ID = "TERMINAL_ID";
        private const string DB_COL_CANT_QUEJAS_SANCION = "CANTIDAD_QUEJAS_SANCION";
        private const string DB_COL_COSTO_PARQUEO_HORA = "COSTO_PARQUEO_HORA";
        private const string DB_COL_COSTO_PARQUEO_DIA = "COSTO_PARQUEO_DIA";
        private const string DB_COL_MONTO_INICIAL_TARJETA = "MONTO_INICIAL_TARJETA";
        private const string DB_COL_COSTO_PARQUEO_BUS_MES = "COSTO_PARQUEO_BUS_MES"; 
        private const string DB_COL_CANT_TARDIA_SANCION = "CANTIDAD_TARDIAS_SANCION";
        private const string DB_COL_CANT_MINUTOS_TARDIA = "CANTIDAD_MINUTOS_TARDIA";

        public SqlOperation GetCreateStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "CRE_CONFIGURACION_TERMINAL_PR" };

            var c = (ConfiguracionTerminal)entity;
            operation.AddIntParam(DB_COL_TERMINAL_ID, c.TerminalId);
            operation.AddIntParam(DB_COL_CANT_QUEJAS_SANCION, c.CantidadQuejasSancion);
            operation.AddDoubleParam(DB_COL_COSTO_PARQUEO_DIA, c.CostoParqueoDia);
            operation.AddDoubleParam(DB_COL_COSTO_PARQUEO_HORA, c.CostoParqueoHora);
            operation.AddDoubleParam(DB_COL_MONTO_INICIAL_TARJETA, c.MontoInicialTarjeta);
            operation.AddDoubleParam(DB_COL_COSTO_PARQUEO_BUS_MES, c.CostoParqueoBusMes);
            operation.AddIntParam(DB_COL_CANT_MINUTOS_TARDIA, c.CantidadMinutosTardia);
            operation.AddIntParam(DB_COL_CANT_TARDIA_SANCION, c.CantidadTardiasSancion);

            return operation;
        }

        public SqlOperation GetRetriveStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "RET_CONFIGURACION_TERMINAL_PR" };

            var c = (ConfiguracionTerminal)entity;
            operation.AddIntParam(DB_COL_TERMINAL_ID, c.TerminalId);

            return operation;
        }

        public SqlOperation GetRetriveAllStatement()
        {
            var operation = new SqlOperation { ProcedureName = "RET_ALL_CONFIGURACION_TERMINAL_PR" };
            return operation;
        }

        public SqlOperation GetUpdateStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "UPD_CONFIGURACION_TERMINAL_PR" };

            var c = (ConfiguracionTerminal)entity;
            operation.AddIntParam(DB_COL_TERMINAL_ID, c.TerminalId);
            operation.AddIntParam(DB_COL_CANT_QUEJAS_SANCION, c.CantidadQuejasSancion);
            operation.AddDoubleParam(DB_COL_COSTO_PARQUEO_DIA, c.CostoParqueoDia);
            operation.AddDoubleParam(DB_COL_COSTO_PARQUEO_HORA, c.CostoParqueoHora);
            operation.AddDoubleParam(DB_COL_MONTO_INICIAL_TARJETA, c.MontoInicialTarjeta);
            operation.AddDoubleParam(DB_COL_COSTO_PARQUEO_BUS_MES, c.CostoParqueoBusMes);
            operation.AddIntParam(DB_COL_CANT_MINUTOS_TARDIA, c.CantidadMinutosTardia);
            operation.AddIntParam(DB_COL_CANT_TARDIA_SANCION, c.CantidadTardiasSancion);

            return operation;
        }

        public SqlOperation GetDeleteStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "DEL_CONFIGURACION_TERMINAL_PR" };

            var c = (ConfiguracionTerminal)entity;
            operation.AddIntParam(DB_COL_TERMINAL_ID, c.TerminalId);
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
            var listItem = new ConfiguracionTerminal
            {
                TerminalId = GetIntValue(row, DB_COL_TERMINAL_ID),
                CantidadQuejasSancion = GetIntValue(row, DB_COL_CANT_QUEJAS_SANCION),
                CostoParqueoDia = GetDoubleValue(row, DB_COL_COSTO_PARQUEO_DIA),
                CostoParqueoHora = GetDoubleValue(row, DB_COL_COSTO_PARQUEO_HORA),
                MontoInicialTarjeta = GetDoubleValue(row, DB_COL_MONTO_INICIAL_TARJETA),
                CostoParqueoBusMes = GetDoubleValue(row, DB_COL_COSTO_PARQUEO_BUS_MES),
                CantidadMinutosTardia = GetIntValue(row, DB_COL_CANT_MINUTOS_TARDIA),
                CantidadTardiasSancion = GetIntValue(row, DB_COL_CANT_TARDIA_SANCION)
            };

            return listItem;
        }
    }
}