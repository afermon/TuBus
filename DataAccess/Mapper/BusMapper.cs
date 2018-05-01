using System;
using System.Collections.Generic;
using DataAccess.Dao;
using Entities;

namespace DataAccess.Mapper
{
    public class BusMapper : EntityMapper, ISqlStaments, IObjectMapper
    {
        private const string DB_COL_PLACA = "PLACA";
        private const string DB_COL_CAPACIDAD_SENTADO = "CAPACIDAD_SENTADO";
        private const string DB_COL_CAPACIDAD_DEPIE = "CAPACIDAD_DEPIE";
        private const string DB_COL_ASIENTO_DISCAPACITADO = "ASIENTO_DISCAPACITADO";
        private const string DB_COL_ESTADO = "ESTADO";
        private const string DB_COL_EMPRESA = "EMPRESA";
        private const string DB_COL_NOMBRE_EMPRESA = "NOMBRE_EMPRESA";

        public SqlOperation GetCreateStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "CRE_BUS_PR" };

            var bus = (Bus)entity;

            operation.AddVarcharParam(DB_COL_PLACA, bus.Id);
            operation.AddIntParam(DB_COL_CAPACIDAD_SENTADO, bus.CapacidadSentado);
            operation.AddIntParam(DB_COL_CAPACIDAD_DEPIE, bus.CapacidadDePie);
            operation.AddIntParam(DB_COL_ASIENTO_DISCAPACITADO, bus.AsientoDiscapacitado);
            operation.AddIntParam(DB_COL_EMPRESA, bus.EmpresaId);

            return operation;
        }

        public SqlOperation GetRetriveStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "RET_BUS_POR_PLACA_PR" };
            var bus = (Bus)entity;

            operation.AddVarcharParam(DB_COL_PLACA, bus.Id);

            return operation;
        }

        public SqlOperation GetRetriveAllStatement()
        {
            return new SqlOperation { ProcedureName = "RET_ALL_BUS_PR" };
        }

        public SqlOperation GetUpdateStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "UPD_BUS_PR" };
            var bus = (Bus)entity;

            operation.AddVarcharParam(DB_COL_PLACA, bus.Id);
            operation.AddIntParam(DB_COL_CAPACIDAD_SENTADO, bus.CapacidadSentado);
            operation.AddIntParam(DB_COL_CAPACIDAD_DEPIE, bus.CapacidadDePie);
            operation.AddIntParam(DB_COL_ASIENTO_DISCAPACITADO, bus.AsientoDiscapacitado);

            return operation;
        }

        public SqlOperation GetUpdateEstadoStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "UPD_BUS_ESTADO_PR" };
            var bus = (Bus)entity;

            operation.AddVarcharParam(DB_COL_PLACA, bus.Id);

            return operation;
        }

        public SqlOperation GetDeleteStatement(BaseEntity entity)
        {
            throw new NotImplementedException();
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
            var parqueo = new Bus
            {
                Id = GetStringValue(row, DB_COL_PLACA),
                CapacidadSentado = GetIntValue(row, DB_COL_CAPACIDAD_SENTADO),
                CapacidadDePie = GetIntValue(row, DB_COL_CAPACIDAD_DEPIE),
                AsientoDiscapacitado = GetIntValue(row, DB_COL_ASIENTO_DISCAPACITADO),
                Estado = GetStringValue(row, DB_COL_ESTADO),
                Empresa = GetStringValue(row, DB_COL_NOMBRE_EMPRESA),
                EmpresaId = GetIntValue(row, DB_COL_EMPRESA)
            };

            return parqueo;
        }
    }
}
