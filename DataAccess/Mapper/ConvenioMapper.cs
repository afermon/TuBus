using System.Collections.Generic;
using DataAccess.Dao;
using Entities;

namespace DataAccess.Mapper
{
    public class ConvenioMapper : EntityMapper, ISqlStaments, IObjectMapper
    {
        private const string DB_COL_CEDULA_JURIDICA = "CEDULA_JURIDICA";
        private const string DB_COL_NOMBRE_INSTITUCION = "NOMBRE_INSTITUCION";
        private const string DB_COL_DESCUENTO = "DESCUENTO_TARIFA";
        private const string DB_COL_ESTADO = "ESTADO";
        private const string DB_COL_TERMINAL_ID = "TERMINAL_ID";
        private const string DB_COL_EMAIL_ENCARGADO = "EMAIL_ENCARGADO";

        public SqlOperation GetCreateStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "CRE_CONVENIO_PR" };

            var convenio = (Convenio)entity;
            operation.AddIntParam(DB_COL_CEDULA_JURIDICA, convenio.CedulaJuridica);
            operation.AddVarcharParam(DB_COL_NOMBRE_INSTITUCION, convenio.NombreInstitucion);
            operation.AddIntParam(DB_COL_DESCUENTO, convenio.DescuentoTarifa);
            operation.AddVarcharParam(DB_COL_EMAIL_ENCARGADO, convenio.EmailEncargado);
            return operation;
        }

        public SqlOperation GetCreateMultipleStatement(BaseEntity entity, int id)
        {
            var operation = new SqlOperation { ProcedureName = "CRE_CONVENIO_TERMINAL_PR" };

            var convenio = (Convenio)entity;
            operation.AddIntParam(DB_COL_CEDULA_JURIDICA, convenio.CedulaJuridica);
            operation.AddIntParam(DB_COL_TERMINAL_ID, id);
            return operation;
        }

        public SqlOperation GetRetriveStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "RET_CONVENIO_BY_ID" };

            var convenio = (Convenio)entity;
            operation.AddIntParam(DB_COL_CEDULA_JURIDICA, convenio.CedulaJuridica);

            return operation;
        }

        public SqlOperation GetRetriveByTerminalId(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "RET_CONVENIO_BY_TERMINAL_ID" };

            var convenio = (Convenio)entity;
            operation.AddIntParam(DB_COL_TERMINAL_ID, convenio.Terminal.Id);

            return operation;
        }

        public SqlOperation GetRetriveAllStatement()
        {
            return new SqlOperation { ProcedureName = "RET_ALL_CONVENIO_PR" };
        }

        public SqlOperation GetUpdateStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "UPD_CONVENIO_PR" };

            var convenio = (Convenio)entity;
            operation.AddIntParam(DB_COL_CEDULA_JURIDICA, convenio.CedulaJuridica);
            operation.AddVarcharParam(DB_COL_NOMBRE_INSTITUCION, convenio.NombreInstitucion);
            operation.AddIntParam(DB_COL_DESCUENTO, convenio.DescuentoTarifa);
            operation.AddVarcharParam(DB_COL_EMAIL_ENCARGADO, convenio.EmailEncargado);

            return operation;
        }
        
        public SqlOperation GetDeleteStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "DEL_CONVENIO_PR" };

            var convenio = (Convenio)entity;
            operation.AddIntParam(DB_COL_CEDULA_JURIDICA, convenio.CedulaJuridica);

            return operation;
        }

        public SqlOperation GetDeleteConveniosStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "UPD_CONVENIO_DEL" };

            var convenio = (Convenio)entity;
            operation.AddIntParam(DB_COL_CEDULA_JURIDICA, convenio.CedulaJuridica);

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
            return new Convenio
            {
                CedulaJuridica = GetIntValue(row, DB_COL_CEDULA_JURIDICA),
                DescuentoTarifa = GetIntValue(row, DB_COL_DESCUENTO),
                Estado = GetStringValue(row, DB_COL_ESTADO),
                NombreInstitucion = GetStringValue(row, DB_COL_NOMBRE_INSTITUCION),
                EmailEncargado = GetStringValue(row, DB_COL_EMAIL_ENCARGADO),
                Empresas = new List<Empresa>()
            };
        }
    }
}
