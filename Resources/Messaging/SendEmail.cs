using System;
using System.Configuration;
using Entities.Messaging;
using Newtonsoft.Json;
using SparkPost;

namespace Resources.Messaging
{
    public class SendEmail
    {   
        /// <summary>
        /// Send Mail
        /// </summary>
        /// <param name="email"> Object Email</param>
        public void SendEmailMessage(EmailMessage email)
        {
            var transmission = new SparkPost.Transmission {Content = {TemplateId = ConfigurationManager.AppSettings["activation-card"] }, Options = new Options{Transactional = true}};

            var recipient = new Recipient
            {
                Address = new Address {Email = email.To},
                SubstitutionData =
                {
                    ["Usuario"] = email.UserName,
                    ["UrlActivation"] = email.Url
                }
            };
            transmission.Recipients.Add(recipient);

            var client = new Client(ConfigurationManager.AppSettings["SparkPostApiKey"]);

            client.CustomSettings.SendingMode = SendingModes.Sync;
            var response = client.Transmissions.Send(transmission);

            var status =  response.Result.StatusCode;

        }

        public void SendResetEmail(EmailMessage email)
        {
            var transmission = new SparkPost.Transmission { Content = { TemplateId = ConfigurationManager.AppSettings["reset-password"] }, Options = new Options { Transactional = true } };

            var recipient = new Recipient
            {
                Address = new Address { Email = email.To },
                SubstitutionData =
                {
                    ["Usuario"] = email.UserName,
                    ["ResetToken"] = email.Url,
                    ["Email"] = email.To
                }
            };
            transmission.Recipients.Add(recipient);

            var client = new Client(ConfigurationManager.AppSettings["SparkPostApiKey"]);

            client.CustomSettings.SendingMode = SendingModes.Sync;
            var response = client.Transmissions.Send(transmission);

            var status = response.Result.StatusCode;
        }
        

        public void SendBussinesEmail(EmailMessage email)
        {
            var transmission = new Transmission { Content = { TemplateId = "bussiness-approval" }, Options = new Options { Transactional = true } };

            var recipient = new Recipient
            {
                Address = new Address { Email = email.To },
                SubstitutionData =
                {
                    ["Usuario"] = email.To,
                    ["Empresa"] = email.EmpresaNombre
                }
            };
            transmission.Recipients.Add(recipient);

            var client = new Client(ConfigurationManager.AppSettings["SparkPostApiKey"]);

            client.CustomSettings.SendingMode = SendingModes.Sync;
            var response = client.Transmissions.Send(transmission);

            var status = response.Result.StatusCode;
        }
        

        public void SendRechazoEmpresaEmail(EmailMessage email)
        {
            var transmission = new Transmission { Content = { TemplateId = "rechazo-solicitud-empresa" }, Options = new Options { Transactional = true } };

            var recipient = new Recipient
            {
                Address = new Address { Email = email.To },
                SubstitutionData =
                {
                    ["Usuario"] = email.To,
                    ["Empresa"] = email.EmpresaNombre,
                    ["RazonRechazo"] = email.Message
                }
            };
            transmission.Recipients.Add(recipient);

            var client = new Client(ConfigurationManager.AppSettings["SparkPostApiKey"]);

            client.CustomSettings.SendingMode = SendingModes.Sync;
            var response = client.Transmissions.Send(transmission);

            var status = response.Result.StatusCode;
        }
        

        public void SendAprobacionEmpresaEmail(EmailMessage email)
        {
            var transmission = new Transmission { Content = { TemplateId = ConfigurationManager.AppSettings["aprobacion-solicitud-empresa"] }, Options = new Options { Transactional = true } };

            var recipient = new Recipient
            {
                Address = new Address { Email = email.To },
                SubstitutionData =
                {
                    ["Usuario"] = email.To,
                    ["Empresa"] = email.EmpresaNombre
                }
            };
            transmission.Recipients.Add(recipient);

            var client = new Client(ConfigurationManager.AppSettings["SparkPostApiKey"]);

            client.CustomSettings.SendingMode = SendingModes.Sync;
            var response = client.Transmissions.Send(transmission);

            var status = response.Result.StatusCode;
        }

        public void SendEmailInvoice(EmailMessage email)
        {
            var transmission = new SparkPost.Transmission { Content = { TemplateId = "invoce-tu-bus" }, Options = new Options { Transactional = true } };

            var recipient = new Recipient
            {
                Address = new Address { Email = email.To },
                SubstitutionData =
                {
                    ["DateInvoce"] = DateTime.Now.ToLongDateString(),
                    ["Money"] = email.Url,
                    ["Description"] = email.Message
                }
            };
            transmission.Recipients.Add(recipient);

            var client = new Client(ConfigurationManager.AppSettings["SparkPostApiKey"]);

            client.CustomSettings.SendingMode = SendingModes.Sync;
            var response = client.Transmissions.Send(transmission);

            var status = response.Result.StatusCode;

        }

        public void SendNotificationMessage(EmailMessage email)
        {
            var transmission = new SparkPost.Transmission { Content = { TemplateId = ConfigurationManager.AppSettings["solicitud-tarjetas"] }, Options = new Options { Transactional = true } };

            var recipient = new Recipient
            {
                Address = new Address { Email = email.To },
                SubstitutionData =
                {
                    ["Usuario"] = email.UserName,
                    ["UrlActivation"] = email.Url
                }
            };
            transmission.Recipients.Add(recipient);

            var client = new Client(ConfigurationManager.AppSettings["SparkPostApiKey"]);

            client.CustomSettings.SendingMode = SendingModes.Sync;
            var response = client.Transmissions.Send(transmission);

            var status = response.Result.StatusCode;

        }
    }
}
