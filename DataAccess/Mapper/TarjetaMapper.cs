using System.Collections.Generic;
using DataAccess.Dao;
using Entities;

namespace DataAccess.Mapper
{
    public class TarjetaMapper : EntityMapper, ISqlStaments, IObjectMapper
    {
        private const string DB_COL_EMAIL = "EMAIL";
        private const string DB_COL_CODIGO_UNICO = "CODIGO_UNICO";
        private const string DB_COL_SALDO_DISPONIBLE = "SALDO_DISPONIBLE";
        private const string DB_COL_TERMINAL_ID = "TERMINAL_ID";
        private const string DB_COL_USUARIO = "USUARIO";
        private const string DB_COL_TIPOTARJETA_ID = "TIPOTARJETA_ID";
        private const string DB_COL_CONVENIO_ID = "CONVENIO_ID";
        private const string DB_COL_ESTADO_TARJETA_ID = "ESTADO_TARJETA_ID";

        public SqlOperation GetCreateStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "CRE_TARJETA" };

            var tarjeta = (Tarjeta)entity;
            operation.AddVarcharParam(DB_COL_CODIGO_UNICO, tarjeta.CodigoUnico);
            operation.AddDecimalParam(DB_COL_SALDO_DISPONIBLE, tarjeta.SaldoDisponible);
            operation.AddIntParam(DB_COL_TERMINAL_ID, tarjeta.Terminal.Id);
            operation.AddVarcharParam(DB_COL_USUARIO, tarjeta.Usuario.Email);
            operation.AddIntParam(DB_COL_TIPOTARJETA_ID, tarjeta.TipoTarjeta.TipoTarjetaId);

            if (tarjeta.Convenio != null && tarjeta.Convenio.CedulaJuridica != 0 && tarjeta.Convenio.CedulaJuridica != -1)
                operation.AddIntParam(DB_COL_CONVENIO_ID, tarjeta.Convenio.CedulaJuridica);

            operation.AddIntParam(DB_COL_ESTADO_TARJETA_ID, tarjeta.EstadoTarjeta.EstadoTarjetaId);

            return operation;
        }

        public SqlOperation GetRetriveStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "RET_TARJETA_BY_ID" };

            var tarjeta = (Tarjeta)entity;
            operation.AddVarcharParam(DB_COL_CODIGO_UNICO, tarjeta.CodigoUnico);

            return operation;
        }

        public SqlOperation GetRetriveAllStatement()
        {
            return new SqlOperation { ProcedureName = "RET_LIST_TARJETA" };
        }

        public SqlOperation GetRetriveAllByUser(BaseEntity entity)
        {
            var tarjeta = (Tarjeta)entity;
            var operation = new SqlOperation { ProcedureName = "RET_LIST_TARJETAS_BY_USER" };
            operation.AddVarcharParam(DB_COL_USUARIO, tarjeta.Usuario.Email);

            return operation;
        }

        public SqlOperation UpdateTarjetaSaldo(BaseEntity entity)
        {
            var tarjeta = (Tarjeta)entity;
            var operation = new SqlOperation { ProcedureName = "UPD_SALDO_TARJETA" };
            operation.AddVarcharParam(DB_COL_CODIGO_UNICO, tarjeta.CodigoUnico);
            operation.AddDecimalParam(DB_COL_SALDO_DISPONIBLE, tarjeta.SaldoDisponible);

            return operation;
        }

        public SqlOperation UpdateEstadoTarjeta(BaseEntity entity)
        {
            var tarjeta = (Tarjeta)entity;
            var operation = new SqlOperation { ProcedureName = "UPD_TARJETA_ESTADO_PR" };
            operation.AddVarcharParam(DB_COL_CODIGO_UNICO, tarjeta.CodigoUnico);
            operation.AddIntParam(DB_COL_ESTADO_TARJETA_ID, tarjeta.EstadoTarjeta.EstadoTarjetaId);

            return operation;
        }

        public SqlOperation GetSolicitudReposicion()
        {
           return new SqlOperation { ProcedureName = "RET_TARJETAS_REPOSICION_PR" };
        }

        public SqlOperation GetUpdateStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "UPD_TARJETA" };

            var tarjeta = (Tarjeta)entity;
            operation.AddVarcharParam(DB_COL_CODIGO_UNICO, tarjeta.CodigoUnico);
            operation.AddDecimalParam(DB_COL_SALDO_DISPONIBLE, tarjeta.SaldoDisponible);
            operation.AddIntParam(DB_COL_TERMINAL_ID, tarjeta.Terminal.Id);
            operation.AddVarcharParam(DB_COL_USUARIO, tarjeta.Usuario.Email);
            operation.AddIntParam(DB_COL_TIPOTARJETA_ID, tarjeta.TipoTarjeta.TipoTarjetaId);

            if (tarjeta.Convenio != null && tarjeta.Convenio.CedulaJuridica != 0 && tarjeta.Convenio.CedulaJuridica != -1)
                operation.AddIntParam(DB_COL_CONVENIO_ID, tarjeta.Convenio.CedulaJuridica);

            operation.AddIntParam(DB_COL_ESTADO_TARJETA_ID, tarjeta.EstadoTarjeta.EstadoTarjetaId);

            return operation;
        }

        public SqlOperation GetDeleteStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "DEL_TARJETA" };

            var tarjeta = (Tarjeta)entity;
            operation.AddVarcharParam(DB_COL_CODIGO_UNICO, tarjeta.CodigoUnico);
            return operation;
        }

        public List<BaseEntity> BuildObjects(List<Dictionary<string, object>> lstRows)
        {
            var lstResults = new List<BaseEntity>();

            foreach (var row in lstRows)
            {
                var tarjeta = BuildObject(row);
                lstResults.Add(tarjeta);
            }

            return lstResults;
        }

        public BaseEntity BuildObject(Dictionary<string, object> row)
        {
            return new Tarjeta
            {
                CodigoUnico = GetStringValue(row, DB_COL_CODIGO_UNICO),
                SaldoDisponible = GetDecimalValue(row, DB_COL_SALDO_DISPONIBLE),
                Terminal = new Terminal{ Id = GetIntValue(row, DB_COL_TERMINAL_ID) },
                Usuario = new Usuario { Identificacion = GetStringValue(row, DB_COL_USUARIO) },
                TipoTarjeta = new TipoTarjeta { TipoTarjetaId = GetIntValue(row, DB_COL_TIPOTARJETA_ID) },
                Convenio = new Convenio { CedulaJuridica = GetIntValue(row, DB_COL_CONVENIO_ID) },
                EstadoTarjeta = new EstadoTarjeta{ EstadoTarjetaId = GetIntValue(row, DB_COL_ESTADO_TARJETA_ID) }
            };
        }
    }
}
