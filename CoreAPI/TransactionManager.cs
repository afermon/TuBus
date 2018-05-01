using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Crud;
using Entities;
using Entities.Messaging;
using Exceptions;
using Resources.Messaging;

namespace CoreAPI
{
    public class TransactionManager : BaseManager
    {
        private readonly TransactionCrudFactory _crudFactory;

        public TransactionManager()
        {
            _crudFactory = new TransactionCrudFactory();
        }

        public void CreateUserTransaction(Transaccion transaction, bool isFromBus = false)
        {
            try
            {
                var cardManager = new TarjetaManager();
                var card = cardManager.GeTarjetaByUniquecode(transaction.CardUniqueCode);

                if (card == null)
                    throw new BusinessException(301);

                if(transaction.Type == null)
                    throw new BusinessException(302);

                var userManager =  new UsuarioManager();

                var user = userManager.Retrieve(new Usuario {Email = card.Usuario.Identificacion});

                if (transaction.Type.Equals("Recarga", StringComparison.CurrentCultureIgnoreCase))
                {
                    card.SaldoDisponible = card.SaldoDisponible + transaction.Charge;
                    cardManager.UpdateSaldoTarjeta(card);
                }
                else
                {
                    if (isFromBus)
                    {
                        transaction.Charge = card.Convenio != null && card.Convenio.CedulaJuridica != -1 ? 
                                            transaction.Charge - (transaction.Charge * (card.Convenio.DescuentoTarifa / 100)):
                                            transaction.Charge - (transaction.Charge * (card.TipoTarjeta.DiscountPercentage / 100));
                    }

                    card.SaldoDisponible = card.SaldoDisponible - transaction.Charge;

                    if (card.SaldoDisponible < 0)
                        throw new BusinessException(304);

                    cardManager.UpdateSaldoTarjeta(card);

                    if (card.SaldoDisponible <= user.SmsNotificationsMin)
                    {
                        var smsNotification = new SendSms();
                        smsNotification.SendSmsMessage(new SmsMessage
                        {
                            DestinationNumber = user.Telefono.ToString(),
                            Message = "Su cuenta tiene poco saldo disponible, por favor recargue."
                        });
                    }
                }

                var email = new SendEmail();

                email.SendEmailInvoice(new EmailMessage
                {
                    To = user.Email,
                    Url = transaction.Charge.ToString(),
                    Message = $"{transaction.Type} - {transaction.Description} "
                });

                _crudFactory.Create(transaction);
            }
            catch (Exception e)
            {
                ExceptionManager.GetInstance().Process(e);
            }
        }


        public void CreateFirstTransaction(Transaccion transaction)
        {
            try
            {
                _crudFactory.Create(transaction);
            }
            catch (Exception e)
            {
                ExceptionManager.GetInstance().Process(e);
            }
        }

        public List<Transaccion> GetTrtansactionsByUser(Transaccion transaccion)
        {
            try
            {
                var terminalManager = new TerminalManager();
                var listTransactions = _crudFactory.RetrieveAllByUser<Transaccion>(transaccion);

                listTransactions.ForEach(x =>
                {
                    if (x.Terminal.Id != -1)
                        x.Terminal = terminalManager.RetrieveById(x.Terminal);
                    else
                        x.Terminal.TerminalName = "No hay terminal asociada";

                });

                return listTransactions;
            }
            catch (Exception e)
            {
                ExceptionManager.GetInstance().Process(e);
            }

            return new List<Transaccion>();
        }

        public List<Transaccion> GetTransactionsByTerminal(Transaccion transaccion)
        {
            try
            {
                return _crudFactory.RetrieveAllByTerminal<Transaccion>(transaccion);
            }
            catch (Exception e)
            {
                ExceptionManager.GetInstance().Process(e);
            }

            return new List<Transaccion>();
        }

        public List<Transaccion> GetAllTransactions()
        {
            try
            {
                var terminalManager = new TerminalManager();
                var listTransactions = _crudFactory.RetrieveAll<Transaccion>();
                var lineaManager =  new LineaManager();
                listTransactions.ForEach(x =>
                {
                    if (x.Terminal.Id != -1)
                        x.Terminal = terminalManager.RetrieveById(x.Terminal);
                    else
                        x.Terminal.TerminalName = "No hay terminal asociada";

                    if (x.LineaId != -1)
                    {
                        x.Linea = lineaManager.GetLineById(new Linea { LineaId = x.LineaId });
                        x.Usuario.Email = $"Empresa {x.Linea.Empresa.NombreEmpresa} - Linea {x.Linea.NombreLinea}";
                    }
                });

                return listTransactions;
            }
            catch (Exception e)
            {
                ExceptionManager.GetInstance().Process(e);
            }

            return new List<Transaccion>();
        }

        public void RealizarPagoParqueoBus(Transaccion transaction)
        {
            var email = new SendEmail();

            email.SendEmailInvoice(new EmailMessage
            {
                To = transaction.Usuario.Email,
                Url = transaction.Charge.ToString(),
                Message = $"{transaction.Type} - {transaction.Description} "
            });

            _crudFactory.Create(transaction);
        }
    }
}
