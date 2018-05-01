using System;
using DataAccess.Dao;
using Entities;
using System.Collections.Generic;

namespace DataAccess.Mapper
{
    public class ChoferMapper : EntityMapper, ISqlStaments, IObjectMapper
    {
        private const string DB_COL_CEDULA = "CEDULA";
        private const string DB_COL_NOMBRE = "NOMBRE";
        private const string DB_COL_APELLIDOS = "APELLIDOS";
        private const string DB_COL_TELEFONO = "TELEFONO";
        private const string DB_COL_CORREO = "CORREO";
        private const string DB_COL_FECHA_NACIMIENTO = "FECHA_NACIMIENTO";
        private const string DB_COL_EDAD = "EDAD";
        private const string DB_COL_NUMERO_LICENCIA = "NUMERO_LICENCIA";
        private const string DB_COL_FECHA_EXPIRACION = "FECHA_EXPIRACION";
        private const string DB_COL_EMPRESA = "EMPRESA";
        private const string DB_COL_ESTADO = "ESTADO";
        private const string DB_PARAM_NAME = "RUTA_ID";

        public SqlOperation GetCreateStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "CRE_CHOFER_PR" };

            var c = (Chofer)entity;
            operation.AddVarcharParam(DB_COL_CEDULA, c.Cedula);
            operation.AddVarcharParam(DB_COL_NOMBRE, c.Nombre);
            operation.AddVarcharParam(DB_COL_APELLIDOS, c.Apellidos);
            operation.AddVarcharParam(DB_COL_TELEFONO, c.Telefono);
            operation.AddVarcharParam(DB_COL_CORREO, c.Correo);
            operation.AddDateParam(DB_COL_FECHA_NACIMIENTO, c.FechaNacimiento);
            operation.AddIntParam(DB_COL_EDAD, c.Edad);
            operation.AddVarcharParam(DB_COL_NUMERO_LICENCIA, c.NumeroLicencia);
            operation.AddDateParam(DB_COL_FECHA_EXPIRACION, c.FechaExpiracion);
            operation.AddIntParam(DB_COL_EMPRESA, c.Empresa);
            operation.AddVarcharParam(DB_COL_ESTADO, "Activo");

            return operation;
        }

        public SqlOperation GetDeleteStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "DEL_CHOFER_PR" };

            var chofer = (Chofer)entity;
            operation.AddVarcharParam(DB_COL_CEDULA, chofer.Cedula);

            return operation;
        }

        public SqlOperation GetRetriveAllStatement()
        {
            return new SqlOperation { ProcedureName = "RET_ALL_CHOFER_PR" };
        }

        public SqlOperation GetRetriveStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "RET_CHOFER_BY_CEDULA_PR" };

            var chofer = (Chofer)entity;
            operation.AddVarcharParam(DB_COL_CEDULA, chofer.Cedula);

            return operation;
        }

        public SqlOperation GetRetriveByRutaStatement(int ruta)
        {
            var operation = new SqlOperation { ProcedureName = "RET_CHOFER_BY_RUTA_PR" };

            operation.AddIntParam(DB_PARAM_NAME, ruta);

            return operation;
        }

        public SqlOperation GetUpdateStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "UPD_CHOFER_PR" };

            var c = (Chofer)entity;
            operation.AddVarcharParam(DB_COL_CEDULA, c.Cedula);
            operation.AddVarcharParam(DB_COL_NOMBRE, c.Nombre);
            operation.AddVarcharParam(DB_COL_APELLIDOS, c.Apellidos);
            operation.AddVarcharParam(DB_COL_TELEFONO, c.Telefono);
            operation.AddVarcharParam(DB_COL_CORREO, c.Correo);
            operation.AddDateParam(DB_COL_FECHA_NACIMIENTO, c.FechaNacimiento);
            operation.AddIntParam(DB_COL_EDAD, c.Edad);
            operation.AddVarcharParam(DB_COL_NUMERO_LICENCIA, c.NumeroLicencia);
            operation.AddDateParam(DB_COL_FECHA_EXPIRACION, c.FechaExpiracion);
            operation.AddIntParam(DB_COL_EMPRESA, c.Empresa);
            operation.AddVarcharParam(DB_COL_ESTADO, c.Estado);

            return operation;
        }

        public BaseEntity BuildObject(Dictionary<string, object> row)
        {
            var chofer = new Chofer
            {
                Cedula = GetStringValue(row, DB_COL_CEDULA),
                Nombre = GetStringValue(row, DB_COL_NOMBRE),
                Apellidos = GetStringValue(row, DB_COL_APELLIDOS),
                Telefono = GetStringValue(row, DB_COL_TELEFONO),
                Correo = GetStringValue(row, DB_COL_CORREO), 
                FechaNacimiento = GetDateValue(row, DB_COL_FECHA_NACIMIENTO),
                Edad = GetIntValue(row, DB_COL_EDAD),
                NumeroLicencia = GetStringValue(row, DB_COL_NUMERO_LICENCIA),
                FechaExpiracion = GetDateValue(row, DB_COL_FECHA_EXPIRACION),
                Empresa = GetIntValue(row, DB_COL_EMPRESA),
                Estado = GetStringValue(row, DB_COL_ESTADO)
            };

            return chofer;
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
