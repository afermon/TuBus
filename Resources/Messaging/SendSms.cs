using Entities.Messaging;
using Exceptions;
using System;
using System.Configuration;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace Resources.Messaging
{
    public class SendSms
    {
        public bool SendSmsMessage(SmsMessage sms)
        {
            try
            {
                var accountSid = ConfigurationManager.AppSettings["Account"];
                var token = ConfigurationManager.AppSettings["Token"];
                var sourcePhone = ConfigurationManager.AppSettings["Phone"];

                TwilioClient.Init(accountSid, token);
                MessageResource.Create(to: new PhoneNumber($"+506{sms.DestinationNumber}"),
                    from: new PhoneNumber(sms.SourceNumber ?? sourcePhone), body: sms.Message);

            }
            catch (Exception e)
            {
                ExceptionManager.GetInstance().Process(e);
                return false;
            }
            return true;
        }
    }
}
