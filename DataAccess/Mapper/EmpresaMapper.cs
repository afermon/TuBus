using System;
using System.Collections.Generic;
using DataAccess.Dao;
using Entities;

namespace DataAccess.Mapper
{
    public class EmpresaMapper : EntityMapper, ISqlStaments, IObjectMapper
    {
        private const string DB_COL_EMPRESA_ID = "CEDULA_JURIDICA";
        private const string DB_COL_NOMBRE_EMPRESA = "NOMBRE_EMPRESA";
        private const string DB_COL_ESTADO = "ESTADO";
        private const string DB_COL_EMAIL_ENCARGADO = "EMAIL_ENCARGADO";
        private const string DB_COL_TELEFONO = "TELEFONO";
        private const string DB_PARAM_NAME = "RUTA_ID";

        public SqlOperation GetCreateStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "CRE_EMPRESA_PR" };

            var empresa = (Empresa)entity;

            operation.AddIntParam(DB_COL_EMPRESA_ID, empresa.CedulaJuridica);
            operation.AddVarcharParam(DB_COL_NOMBRE_EMPRESA, empresa.NombreEmpresa);
            operation.AddVarcharParam(DB_COL_EMAIL_ENCARGADO, empresa.EmailEncargado);
            operation.AddVarcharParam(DB_COL_TELEFONO, empresa.Telefono);

            return operation;
        }

        public SqlOperation GetRetriveStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "RET_EMPRESA_BY_ID" };

            var empresa = (Empresa)entity;
            operation.AddIntParam(DB_COL_EMPRESA_ID, empresa.CedulaJuridica);

            return operation;
        }

        public SqlOperation GetRetriveAllStatement()
        {
            return new SqlOperation { ProcedureName = "RET_ALL_EMPRESA_PR" };
        }

        public SqlOperation GetRetriveAllPendientesStatement()
        {
            return new SqlOperation { ProcedureName = "RET_ALL_EMPRESAS_PENDIENTES_PR" };
        }

        public SqlOperation GetUpdateStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "UPD_EMPRESA_PR" };
            var empresa = (Empresa)entity;

            operation.AddVarcharParam(DB_COL_NOMBRE_EMPRESA, empresa.NombreEmpresa);
            operation.AddVarcharParam(DB_COL_EMAIL_ENCARGADO, empresa.EmailEncargado);
            operation.AddVarcharParam(DB_COL_TELEFONO, empresa.Telefono);

            return operation;
        }

        public SqlOperation GetDeleteStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "UPD_EMPRESA_ESTADO" };
            var empresa = (Empresa)entity;

            operation.AddVarcharParam(DB_COL_NOMBRE_EMPRESA, empresa.NombreEmpresa);

            return operation;
        }

        public SqlOperation GetAprobarEmpresaStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "UPD_EMPRESA_PENDIENTE_PR" };
            var empresa = (Empresa)entity;

            operation.AddVarcharParam(DB_COL_NOMBRE_EMPRESA, empresa.NombreEmpresa);
            operation.AddVarcharParam(DB_COL_ESTADO, empresa.Estado);

            return operation;
        }

        public SqlOperation GetRetriveByEmpresarioStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "RET_EMPRESA_BY_EMPRESARIO_PR" };

            var empresa = (Empresa)entity;
            operation.AddVarcharParam(DB_COL_EMAIL_ENCARGADO, empresa.EmailEncargado);

            return operation;
        }

        public SqlOperation GetRetrieveEmpresaByRuta(BaseEntity entity)
        {

            var operation = new SqlOperation { ProcedureName = "RET_RUTA_EMPRESA_PR" };
            var r = (Ruta)entity;

            operation.AddIntParam(DB_PARAM_NAME, r.Id);

            return operation;
        }

        public List<BaseEntity> BuildObjects(List<Dictionary<string, object>> lstRows)
        {
            var resultados = new List<BaseEntity>();

            foreach (var row in lstRows)
            {
                var empresa = BuildObject(row);
                resultados.Add(empresa);
            }

            return resultados;
        }

        public BaseEntity BuildObject(Dictionary<string, object> row)
        {
            return new Empresa
            {
                CedulaJuridica = GetIntValue(row, DB_COL_EMPRESA_ID),
                NombreEmpresa = GetStringValue(row, DB_COL_NOMBRE_EMPRESA),
                Estado = GetStringValue(row, DB_COL_ESTADO),
                EmailEncargado = GetStringValue(row, DB_COL_EMAIL_ENCARGADO),
                Telefono = GetStringValue(row, DB_COL_TELEFONO),
            };
        }
    }
}
