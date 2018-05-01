using System;
using System.Collections.Generic;
using DataAccess.Dao;
using Entities;

namespace DataAccess.Mapper
{
    public class UsuarioMapper : EntityMapper, ISqlStaments, IObjectMapper
    {
        private const string DB_COL_EMAIL = "EMAIL";
        private const string DB_COL_SALT = "PASSWORD_SALT";
        private const string DB_COL_HASH = "PASSWORD_HASH";
        private const string DB_COL_PASSWORD_LAST_SET = "PASSWORD_LAST_SET";
        private const string DB_COL_IDENTIFICACION = "IDENTIFICACION";
        private const string DB_COL_NOMBRE = "NOMBRE";
        private const string DB_COL_APELLIDOS = "APELLIDOS";
        private const string DB_COL_NACIMIENTO = "FECHA_NACIMIENTO";
        private const string DB_COL_EDAD = "EDAD";
        private const string DB_COL_ESTADO = "ESTADO";
        private const string DB_COL_ROLE_ID = "ROLE_ID"; 
        private const string DB_COL_NOTIFY_USER_AT = "NOTIFY_USER_AT"; 
        private const string DB_COL_PHONE = "PHONE";
        private const string DB_COL_TERMINAL = "TERMINAL_ID";
        private const string DB_COL_RESET_TOKEN = "RESET_TOKEN";


        public SqlOperation GetCreateStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "CRE_USUARIO_PR" };

            var u = (Usuario)entity;
            operation.AddVarcharParam(DB_COL_EMAIL, u.Email);
            operation.AddVarcharParam(DB_COL_SALT, u.PasswordSalt);
            operation.AddVarcharParam(DB_COL_HASH, u.PasswordHash);
            operation.AddDateTimeParam(DB_COL_PASSWORD_LAST_SET, u.PasswordLastSet);
            operation.AddVarcharParam(DB_COL_IDENTIFICACION, u.Identificacion);
            operation.AddVarcharParam(DB_COL_NOMBRE, u.Nombre);
            operation.AddVarcharParam(DB_COL_APELLIDOS, u.Apellidos);
            operation.AddDateParam(DB_COL_NACIMIENTO, u.FechaNacimiento);
            operation.AddVarcharParam(DB_COL_ESTADO, u.Estado);
            operation.AddVarcharParam(DB_COL_ROLE_ID, u.RoleId);
            operation.AddDecimalParam(DB_COL_NOTIFY_USER_AT, u.SmsNotificationsMin ?? 100);
            operation.AddIntParam(DB_COL_PHONE, u.Telefono);

            if (u.TerminalId != null && u.TerminalId != -1)
                operation.AddIntParam(DB_COL_TERMINAL, u.TerminalId ?? 0);

            return operation;
        }

        public SqlOperation GetDeleteStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "DEL_USUARIO_PR" };

            var u = (Usuario)entity;
            operation.AddVarcharParam(DB_COL_EMAIL, u.Email);
            return operation;
        }

        public SqlOperation GetRetriveAllStatement()
        {
            var operation = new SqlOperation { ProcedureName = "RET_ALL_USUARIO_PR" };
            return operation;
        }

        public SqlOperation GetRetriveStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "RET_USUARIO_PR" };

            var u = (Usuario)entity;
            operation.AddVarcharParam(DB_COL_EMAIL, u.Email);

            return operation;
        }

        public SqlOperation GetUpdateStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "UPD_USUARIO_PR" };

            var u = (Usuario)entity;
            operation.AddVarcharParam(DB_COL_EMAIL, u.Email);
            operation.AddVarcharParam(DB_COL_IDENTIFICACION, u.Identificacion);
            operation.AddVarcharParam(DB_COL_NOMBRE, u.Nombre);
            operation.AddVarcharParam(DB_COL_APELLIDOS, u.Apellidos);
            operation.AddDateParam(DB_COL_NACIMIENTO, u.FechaNacimiento);
            operation.AddVarcharParam(DB_COL_ESTADO, u.Estado);
            operation.AddVarcharParam(DB_COL_ROLE_ID, u.RoleId);
            operation.AddDecimalParam(DB_COL_NOTIFY_USER_AT, u.SmsNotificationsMin ?? 100);
            operation.AddIntParam(DB_COL_PHONE, u.Telefono);
            if (u.TerminalId != null)
                operation.AddIntParam(DB_COL_TERMINAL, u.TerminalId ?? 0);

            return operation;
        }

        public SqlOperation GetUpdatePasswordStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "UPD_USUARIO_PASSWORD_PR" };

            var u = (Usuario)entity;
            operation.AddVarcharParam(DB_COL_EMAIL, u.Email);
            operation.AddVarcharParam(DB_COL_SALT, u.PasswordSalt);
            operation.AddVarcharParam(DB_COL_HASH, u.PasswordHash);
            operation.AddDateTimeParam(DB_COL_PASSWORD_LAST_SET, u.PasswordLastSet);

            return operation;
        }

        public SqlOperation GetUpdateResetTokenStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "UPD_USUARIO_RESET_PR" };

            var u = (Usuario)entity;
            operation.AddVarcharParam(DB_COL_EMAIL, u.Email);
            operation.AddVarcharParam(DB_COL_RESET_TOKEN, u.ResetToken);

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
            var usuario = new Usuario
            {
                Email = GetStringValue(row, DB_COL_EMAIL),
                PasswordLastSet = GetDateValue(row, DB_COL_PASSWORD_LAST_SET),
                Identificacion = GetStringValue(row, DB_COL_IDENTIFICACION),
                Nombre = GetStringValue(row, DB_COL_NOMBRE),
                Apellidos = GetStringValue(row, DB_COL_APELLIDOS),
                FechaNacimiento = GetDateValue(row, DB_COL_NACIMIENTO),
                Edad = GetIntValue(row, DB_COL_EDAD),
                Estado = GetStringValue(row, DB_COL_ESTADO),
                RoleId = GetStringValue(row, DB_COL_ROLE_ID),
                SmsNotificationsMin = GetDecimalValue(row,DB_COL_NOTIFY_USER_AT),
                Telefono = GetIntValue(row, DB_COL_PHONE),
                TerminalId = GetIntValue(row, DB_COL_TERMINAL)
            };

            return usuario;
        }

        public BaseEntity BuildObjectSecure(Dictionary<string, object> row)
        {
            var usuario = new Usuario
            {
                Email = GetStringValue(row, DB_COL_EMAIL),
                PasswordSalt = GetStringValue(row, DB_COL_SALT),
                PasswordHash = GetStringValue(row, DB_COL_HASH),
                PasswordLastSet = GetDateValue(row, DB_COL_PASSWORD_LAST_SET),
                Identificacion = GetStringValue(row, DB_COL_IDENTIFICACION),
                Nombre = GetStringValue(row, DB_COL_NOMBRE),
                Apellidos = GetStringValue(row, DB_COL_APELLIDOS),
                FechaNacimiento = GetDateValue(row, DB_COL_NACIMIENTO),
                Edad = GetIntValue(row, DB_COL_EDAD),
                Estado = GetStringValue(row, DB_COL_ESTADO),
                RoleId = GetStringValue(row, DB_COL_ROLE_ID),
                SmsNotificationsMin = GetDecimalValue(row, DB_COL_NOTIFY_USER_AT),
                Telefono = GetIntValue(row, DB_COL_PHONE),
                TerminalId = GetIntValue(row, DB_COL_TERMINAL),
                ResetToken = GetStringValue(row, DB_COL_RESET_TOKEN)
            };
            return usuario;
        }
    }
}