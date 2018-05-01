using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Dao;
using Entities;

namespace DataAccess.Mapper
{
    public class SolicitudMapper : EntityMapper, ISqlStaments, IObjectMapper
    {
        private const string DB_COL_ID_SOLICITUD = "ID_SOLICITUD";
        private const string DB_COL_TERMINAL_ID = "TERMINAL_ID";
        private const string DB_COL_CEDULA_JURIDICA = "CEDULA_JURIDICA";
        private const string DB_COL_EMAILS = "EMAILS";
        private const string DB_COL_EMAIL_SOLICITANTE = "EMAIL_SOLICITANTE";
        private const string DB_COL_ESTADO = "ESTADO";

        public SqlOperation GetCreateStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "CRE_SOLICITUD_TARJETA" };

            var solicitud = (Solicitud)entity;
            operation.AddVarcharParam(DB_COL_ID_SOLICITUD, solicitud.SolicitudId);
            operation.AddIntParam(DB_COL_TERMINAL_ID, solicitud.Terminal.Id);
            operation.AddIntParam(DB_COL_CEDULA_JURIDICA, solicitud.Convenio.CedulaJuridica);
            operation.AddVarcharParam(DB_COL_EMAILS, solicitud.ListEmails);
            operation.AddVarcharParam(DB_COL_EMAIL_SOLICITANTE, solicitud.EmailSolicitante);

            return operation;
        }

        public SqlOperation GetRetriveStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "RET_SOLICITUD_BY_ID" };
            var solicitud = (Solicitud)entity;
            operation.AddVarcharParam(DB_COL_ID_SOLICITUD, solicitud.SolicitudId);
            return operation;
        }

        public SqlOperation GetRetriveAllStatement()
        {
            return new SqlOperation { ProcedureName = "RET_ALL_SOLICITUD" };
        }

        public SqlOperation GetUpdateStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "UPD_SOLICITUD" };

            var solicitud = (Solicitud)entity;
            operation.AddVarcharParam(DB_COL_ID_SOLICITUD, solicitud.SolicitudId);
            operation.AddVarcharParam(DB_COL_ESTADO, solicitud.Estado);

            return operation;
        }

        public SqlOperation GetDeleteStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "DEL_SOLICITUD" };

            var solicitud = (Solicitud)entity;
            operation.AddVarcharParam(DB_COL_ID_SOLICITUD, solicitud.SolicitudId);

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
            return new Solicitud
            {
                SolicitudId = GetStringValue(row, DB_COL_ID_SOLICITUD),
                Convenio = new Convenio { CedulaJuridica = GetIntValue(row, DB_COL_CEDULA_JURIDICA)},
                EmailSolicitante = GetStringValue(row, DB_COL_EMAIL_SOLICITANTE),
                Estado = GetStringValue(row, DB_COL_ESTADO),
                ListEmails = GetStringValue(row, DB_COL_EMAILS),
                NombreUsuario = GetStringValue(row, DB_COL_EMAIL_SOLICITANTE),
                Terminal = new Terminal { Id = GetIntValue(row, DB_COL_TERMINAL_ID)}
            };
        }
    }
}
