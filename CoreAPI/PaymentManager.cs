using System;
using Entities;
using Exceptions;
using Stripe;

namespace CoreAPI
{
   public class PaymentManager : BaseManager
    {
        public PaymentManager()
        {
            // Set stripe test api Key
            StripeConfiguration.SetApiKey("sk_test_X48s7d5FuB8WDghERT4jBpeX");
        }

        public string NewPayment(Payment newPayment)
        {
            try
            {
                var transactionManager = new TransactionManager();
                var cardManager = new TarjetaManager();

                var card = cardManager.GeTarjetaByUniquecode(newPayment.UserCardUnicode);

                if (card == null)
                    throw new BusinessException(301);

                var chargeOptions = new StripeChargeCreateOptions()
                {
                    Amount = newPayment.AmoutCrc,
                    Currency = "usd",
                    Description = $"{newPayment.Description} - {DateTime.Now.ToLongTimeString()}",
                    SourceTokenOrExistingSourceId = newPayment.CardToken // obtained with Stripe.js               
                };
                var chargeService = new StripeChargeService();
                var charge = chargeService.Create(chargeOptions);
                
                transactionManager.CreateUserTransaction(new Transaccion
                {
                    CardUniqueCode = newPayment.UserCardUnicode,
                    Charge = newPayment.AmoutCrc,
                    Description = newPayment.Description,
                    Terminal = card.Terminal,
                    Type = "Recarga",
                    Status = "Activo"
                });
                return charge.StripeResponse.ObjectJson;
            }
            catch (Exception e)
            {
                ExceptionManager.GetInstance().Process(e);
            }

            return string.Empty;
        }

        public string EmpresaNewPayment(Payment newPayment)
        {
            try
            {
                var transactionManager = new TransactionManager();
                var lineaManager = new LineaManager();

                var linea = lineaManager.GetLineById(new Linea{LineaId = newPayment.LineaId});

                if (linea == null)
                    throw new BusinessException(306);

                var chargeOptions = new StripeChargeCreateOptions()
                {
                    Amount = newPayment.AmoutCrc,
                    Currency = "usd",
                    Description = $"{newPayment.Description} - {DateTime.Now.ToLongTimeString()}",
                    SourceTokenOrExistingSourceId = newPayment.CardToken // obtained with Stripe.js               
                };
                var chargeService = new StripeChargeService();
                var charge = chargeService.Create(chargeOptions);

                transactionManager.RealizarPagoParqueoBus(new Transaccion
                {
                    LineaId = linea.LineaId,
                    Usuario = new Usuario { Email = newPayment.UserMail},
                    Charge = newPayment.AmoutCrc,
                    Description = newPayment.Description,
                    Terminal = linea.Terminal,
                    Type = "Pago",
                    Status = "Activo"
                });

                var pagosManager = new PagosPendientesManager();
                pagosManager.UpdatePagosPorEmpresa(new PagoPendiente { IdPago = newPayment.IdPago});

                return charge.StripeResponse.ObjectJson;
            }
            catch (Exception e)
            {
                ExceptionManager.GetInstance().Process(e);
            }

            return string.Empty;
        }


    }
}
