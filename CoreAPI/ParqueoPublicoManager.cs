using DataAccess.Crud;
using Entities;
using Exceptions;
using System;

namespace CoreAPI
{
    public class ParqueoPublicoManager : BaseManager
    {
        private readonly ParqueoPublicoCrudFactory _crudParqueo;
        private readonly ConfiguracionTerminalCrudFactory _crudConfiguracion;
        private readonly TerminalCrudFactory _crudTerminal;
        private readonly TarjetaCrudFactory _crudTarjeta;

        public ParqueoPublicoManager()
        {
            _crudParqueo = new ParqueoPublicoCrudFactory();
            _crudConfiguracion = new ConfiguracionTerminalCrudFactory();
            _crudTerminal = new TerminalCrudFactory();
            _crudTarjeta = new TarjetaCrudFactory();
        }

        public RegistroParqueo RetrieveByTerminalId(int terminal)
        {
            var parqueo = new RegistroParqueo();
            var terminalResult = _crudTerminal.Retrieve<Terminal>(new Terminal { Id = terminal });
            var configuracionResult = _crudConfiguracion.Retrieve<ConfiguracionTerminal>(new ConfiguracionTerminal { TerminalId = terminal });
            var parqueoResult = _crudParqueo.RetrieveEspaciosDisponibles<RegistroParqueo>(new RegistroParqueo { TerminalId = terminal });

            parqueo.TerminalId = terminal;
            parqueo.CostoParqueoDia = string.Format("¢{0}", configuracionResult.CostoParqueoDia.ToString("N2"));
            parqueo.CostoParqueoHora = string.Format("¢{0}", configuracionResult.CostoParqueoHora.ToString("N2"));
            parqueo.EspaciosTotales = terminalResult.EspaciosParqueo;
            parqueo.EspaciosDisponibles = parqueoResult.EspaciosDisponibles;

            return parqueo;
        }

        public RegistroParqueo RetrieveByTarjetaId(int terminal, string tarjeta)
        {
            var parqueo = new RegistroParqueo();
            var parqueoResult = _crudParqueo.Retrieve<RegistroParqueo>(new RegistroParqueo { TarjetaId = tarjeta });

            parqueo.TerminalId = terminal;
            parqueo.TarjetaId = tarjeta;
            parqueo.EspaciosDisponibles = parqueoResult.EspaciosDisponibles;
            parqueo.HoraInicio = parqueoResult.HoraInicio;
            parqueo.HoraFin = parqueoResult.HoraFin;
            parqueo.CostoTotal = parqueoResult.CostoTotal;

            return parqueo;
        }

        public void CreateIngreso(RegistroParqueo parqueo)
        {
            try
            {
                var tarjeta = _crudTarjeta.Retrieve<Tarjeta>(new Tarjeta { CodigoUnico = parqueo.TarjetaId });

                if (tarjeta == null || tarjeta.EstadoTarjeta.EstadoTarjetaId > 1)
                    throw new BusinessException(407);

                else if (tarjeta.Terminal.Id != parqueo.TerminalId)
                    throw new BusinessException(408);

                var parqueoResultado = _crudParqueo.RetrieveEspaciosDisponibles<RegistroParqueo>(parqueo);

                if (parqueoResultado.EspaciosDisponibles <= 0)
                    throw new BusinessException(401);

                parqueoResultado = _crudParqueo.Retrieve<RegistroParqueo>(parqueo);

                if (parqueoResultado != null)
                    throw new BusinessException(406);

                parqueo.HoraInicio = DateTime.Now;

                _crudParqueo.Create(parqueo);
            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
            }
        }

        public void CreateSalida(RegistroParqueo parqueo)
        {
            try
            {
                var parqueoResultado = _crudParqueo.Retrieve<RegistroParqueo>(parqueo);

                if (parqueoResultado == null)
                    throw new BusinessException(402);

                var configuracionResult = _crudConfiguracion.Retrieve<ConfiguracionTerminal>(new ConfiguracionTerminal { TerminalId = parqueo.TerminalId });

                parqueo.HoraInicio = parqueoResultado.HoraInicio;
                parqueo.HoraFin = DateTime.Now;
                parqueo.TerminalId = parqueo.TerminalId;

                var cantidadDeHoras = parqueo.HoraFin - parqueoResultado.HoraInicio;

                if (cantidadDeHoras.Hours < 24)
                    parqueo.CostoTotal = cantidadDeHoras.Hours * Convert.ToInt32(configuracionResult.CostoParqueoHora);
                if (cantidadDeHoras.Hours < 1)
                    parqueo.CostoTotal = Convert.ToInt32(configuracionResult.CostoParqueoHora);
                else if (cantidadDeHoras.Days > 1)
                    parqueo.CostoTotal = cantidadDeHoras.Days * Convert.ToInt32(configuracionResult.CostoParqueoDia);
                else if (cantidadDeHoras.Days == 1)
                    parqueo.CostoTotal = Convert.ToInt32(configuracionResult.CostoParqueoDia);

                _crudParqueo.Update(parqueo);

                var transactionManager = new TransactionManager();
                transactionManager.CreateUserTransaction(new Transaccion
                {
                    CardUniqueCode = parqueo.TarjetaId,
                    Charge = parqueo.CostoTotal,
                    Description = "Pago de Parqueo Público",
                    Type = "Cobro",
                    Status = "Activo",
                    Terminal = new Terminal { Id = parqueo.TerminalId }
                });
            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
            }
        }
    }
}
