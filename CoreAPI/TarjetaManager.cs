using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Crud;
using Entities;
using Entities.Messaging;
using Exceptions;
using Resources.Messaging;

namespace CoreAPI
{
    public class TarjetaManager : BaseManager
    {
        private readonly TarjetaCrudFactory _crudFactory;

        public TarjetaManager()
        {
            _crudFactory = new TarjetaCrudFactory();
        }

        public void Create(Tarjeta card)
        {
            try
            {
                var getCarsd = GeTarjetaByUniquecode(card.CodigoUnico);

                if (getCarsd != null)
                    throw new BusinessException(300);

                var config = new ConfiguracionManager();
                card.EstadoTarjeta = new EstadoTarjeta { EstadoTarjetaId = 1 };
                var terminalConfig = config.RetrieveConfiguracionTerminal(card.Terminal.Id);
                card.SaldoDisponible = Convert.ToInt32(terminalConfig.MontoInicialTarjeta);
                var transactionManager = new TransactionManager();

                _crudFactory.Create(card);

                transactionManager.CreateFirstTransaction(new Transaccion
                {
                    CardUniqueCode = card.CodigoUnico,
                    Charge = Convert.ToInt32(card.SaldoDisponible),
                    Description = "Monto Inicial " + card.NombreTarjeta,
                    Type = "Venta Tarjeta",
                    Status = "Activo",
                    Terminal = card.Terminal
                });
            }
            catch (Exception e)
            {
                ExceptionManager.GetInstance().Process(e);
            }
        }

        public bool InitializeCard(Tarjeta card)
        {
            card.CodigoUnico = Guid.NewGuid().ToString();
            var queryString = $"email={card.Usuario.Email}&user={card.Usuario.Nombre}&uniqueCode={card.CodigoUnico}&type={card.TipoTarjeta.TipoTarjetaId}&terminalid={card.Terminal.Id}";
            queryString = card.Convenio != null
                ? queryString + $"&agreement={card.Convenio.CedulaJuridica}"
                : queryString;

            var email = new SendEmail();
            try
            {
                email.SendEmailMessage(new EmailMessage
                {
                    To = card.Usuario.Email,
                    Url = queryString,
                    UserName = card.Usuario.Nombre
                });
            }
            catch (Exception e)
            {
                ExceptionManager.GetInstance().Process(e);
            }

            return true;
        }

        public Tarjeta GeTarjetaByUniquecode(string uniqueCode)
        {
            try
            {
                var terminal = new TerminalManager();
                var tipoTarjeta = new TipoTarjetaManager();
                var card = _crudFactory.Retrieve<Tarjeta>(new Tarjeta { CodigoUnico = uniqueCode });

                if (card != null)
                {
                    card.Terminal = terminal.RetrieveById(card.Terminal);
                    card.TipoTarjeta = tipoTarjeta.GetCardTypeById(card.TipoTarjeta);
                    if (card.Convenio != null && card.Convenio.CedulaJuridica != -1)
                    {
                        var convenioManger = new ConvenioManager();
                        card.Convenio = convenioManger.GetById(card.Convenio);
                    }
                }

                return card;
            }
            catch (Exception e)
            {
                ExceptionManager.GetInstance().Process(e);
            }

            return null;
        }

        public List<Tarjeta> GetTarjetas()
        {
            try
            {
                var terminal = new TerminalManager();
                var tipoTarjeta = new TipoTarjetaManager();
                var convenioManger = new ConvenioManager();
                var listTarjetas = _crudFactory.RetrieveAll<Tarjeta>();
                listTarjetas.ForEach(x =>
                {
                    x.TipoTarjeta = tipoTarjeta.GetCardTypeById(x.TipoTarjeta);
                    x.Terminal = terminal.RetrieveById(x.Terminal);
                    if (x.Convenio != null && x.Convenio.CedulaJuridica != -1)
                    {
                        x.Convenio = convenioManger.GetById(x.Convenio);
                    }
                });

                return listTarjetas;
            }
            catch (Exception e)
            {
                ExceptionManager.GetInstance().Process(e);
            }

            return new List<Tarjeta>();
        }

        public List<Tarjeta> GeTarjetasByUser(string userEmail)
        {
            try
            {
                var terminal = new TerminalManager();
                var tipoTarjeta = new TipoTarjetaManager();
                var listTarjetas = _crudFactory.RetrieveAllByUser<Tarjeta>(new Tarjeta { Usuario = new Usuario { Email = userEmail } });
                listTarjetas.ForEach(x =>
                {
                    x.TipoTarjeta = tipoTarjeta.GetCardTypeById(x.TipoTarjeta);
                    x.Terminal = terminal.RetrieveById(x.Terminal);
                    x.NombreTarjeta = $"Tarjeta - {x.Terminal.TerminalName}";
                });

                return listTarjetas;
            }
            catch (Exception e)
            {
                ExceptionManager.GetInstance().Process(e);
            }
            return new List<Tarjeta>();
        }

        public List<Tarjeta> GetTarjetasByTerminalId(int idTerminal)
        {
            try
            {
                var terminal = new TerminalManager();
                var tipoTarjeta = new TipoTarjetaManager();
                var listTarjetas = _crudFactory.RetrieveAll<Tarjeta>();
                var finalList = listTarjetas.Where(t => t.Terminal.Id == idTerminal).ToList();
                finalList.ForEach(x =>
                {
                    x.TipoTarjeta = tipoTarjeta.GetCardTypeById(x.TipoTarjeta);
                    x.Terminal = terminal.RetrieveById(x.Terminal);

                });

                return finalList;
            }
            catch (Exception e)
            {
                ExceptionManager.GetInstance().Process(e);
            }
            return new List<Tarjeta>();
        }

        public void UpdateSaldoTarjeta(Tarjeta card)
        {
            try
            {
                _crudFactory.UpdateSaldo(card);
            }
            catch (Exception e)
            {
                ExceptionManager.GetInstance().Process(e);
            }
        }

        public void UpdateEstadoTarjeta(Tarjeta card)
        {
            try
            {
                _crudFactory.UpdateEstado(card);
            }
            catch (Exception e)
            {
                ExceptionManager.GetInstance().Process(e);
            }
        }

        public List<Tarjeta> GetRequestedRepositionCards()
        {
            try
            {
                var terminal = new TerminalManager();
                var tipoTarjeta = new TipoTarjetaManager();
                var estadoManager = new EstadoTarjetaManager();
                var listTarjetas = _crudFactory.RetrieveRequestedCards<Tarjeta>();
                listTarjetas.ForEach(x =>
                {
                    x.TipoTarjeta = tipoTarjeta.GetCardTypeById(x.TipoTarjeta);
                    x.Terminal = terminal.RetrieveById(x.Terminal);
                    x.EstadoTarjeta = estadoManager.GeTEstadoById(x.EstadoTarjeta.EstadoTarjetaId);

                });

                return listTarjetas;
            }
            catch (Exception e)
            {
                ExceptionManager.GetInstance().Process(e);
            }

            return new List<Tarjeta>();
        }

        public List<Tarjeta> GetRequestedRepositionCardsByTerm(int idTerminal)
        {
            try
            {
                var terminal = new TerminalManager();
                var estadoManager = new EstadoTarjetaManager();
                var tipoTarjeta = new TipoTarjetaManager();
                var listTarjetas = _crudFactory.RetrieveRequestedCards<Tarjeta>();
                var finalList = listTarjetas.Where(t => t.Terminal.Id == idTerminal).ToList();
                finalList.ForEach(x =>
                {
                    x.TipoTarjeta = tipoTarjeta.GetCardTypeById(x.TipoTarjeta);
                    x.Terminal = terminal.RetrieveById(x.Terminal);
                    x.EstadoTarjeta = estadoManager.GeTEstadoById(x.EstadoTarjeta.EstadoTarjetaId);

                });

                return finalList;
            }
            catch (Exception e)
            {
                ExceptionManager.GetInstance().Process(e);
            }
            return new List<Tarjeta>();
        }

        public void Delete(Tarjeta card)
        {
            try
            {
                _crudFactory.Delete(card);
            }
            catch (Exception e)
            {
                ExceptionManager.GetInstance().Process(e);
            }
        }

        public void AprobarReposicion(Tarjeta card)
        {
            try
            {
                var cardManager = new TarjetaManager();
                if (card.EstadoTarjeta.NombreEstado.Equals("Perdida", StringComparison.CurrentCultureIgnoreCase))
                {
                    var oldCard = card.CodigoUnico;
                    card.CodigoUnico = Guid.NewGuid().ToString();
                    card.NombreTarjeta = "Debido a perdida";
                    card.Usuario.Email = card.Usuario.Identificacion;
                    cardManager.Create(card);
                    cardManager.Delete(new Tarjeta { CodigoUnico = oldCard });
                }
                else
                {
                    var transactionManager = new TransactionManager();
                    var oldCard = card.CodigoUnico;
                    card.CodigoUnico = Guid.NewGuid().ToString();
                    card.NombreTarjeta = "Debido a robo";
                    card.Usuario.Email = card.Usuario.Identificacion;
                    var saldo = card.SaldoDisponible;
                    cardManager.Create(card);
                    cardManager.Delete(new Tarjeta { CodigoUnico = oldCard });
                    transactionManager.CreateUserTransaction(new Transaccion
                    {
                        CardUniqueCode = card.CodigoUnico,
                        Charge = Convert.ToInt32(saldo),
                        Description = "Traspaso de fondos",
                        Terminal = card.Terminal,
                        Type = "Recarga",
                        Status = "Activo",
                        Usuario = card.Usuario
                    });
                }

            }
            catch (Exception e)
            {
                ExceptionManager.GetInstance().Process(e);
                throw;
            }
        }
    }
}
