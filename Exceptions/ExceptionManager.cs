using Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Reflection;
using System.Xml.Serialization;

namespace Exceptions
{
    public class ExceptionManager
    {
        private string PATH;

        private static ExceptionManager _instance;

        private static Dictionary<int, ApplicationMessage> messages = new Dictionary<int, ApplicationMessage>();

        private ExceptionManager()
        {
            PATH = ConfigurationManager.AppSettings["ExceptionLogPath"];
            LoadMessages();
        }

        public static ExceptionManager GetInstance()
        {
            return _instance ?? (_instance = new ExceptionManager());
        }

        public void Process(Exception ex)
        {

            var bex = new BusinessException();

            if (ex.GetType() == typeof(BusinessException))
            {
                bex = (BusinessException)ex;
            }
            else
            {
                bex = new BusinessException(1, ex);
            }

            ProcessBussinesException(bex);

        }

        private void ProcessBussinesException(BusinessException bex)
        {
            var today = DateTime.Now.ToString("yyyyMMdd");
            var logName = PATH + today + "_" + "log.txt";

            bex.AppMessage = GetMessage(bex);

            var message = bex.Message + " \n" + bex.StackTrace + " \n";

            if (bex.InnerException != null)
                message += bex.InnerException.Message + "\n" + bex.InnerException.StackTrace;

            using (var w = File.AppendText(logName))
                Log(bex.AppMessage.Message, message, w);

            throw bex;
        }

        public ApplicationMessage GetMessage(BusinessException bex)
        {

            var appMessage = new ApplicationMessage {Message = "Message not found!"};

            if (messages.ContainsKey(bex.ExceptionId))
                appMessage = messages[bex.ExceptionId];

            return appMessage;

        }

        private static void LoadMessages()
        {
            var path = Path.GetDirectoryName((new Uri(Assembly.GetExecutingAssembly().CodeBase)).LocalPath);

            if (path == null) return;

            var applicationMessageLibraryFile = Path.Combine(path, ConfigurationManager.AppSettings["ApplicationMessageLibraryFile"]);

            var xmlSerializer = new XmlSerializer(typeof(List<ApplicationMessage>));

            List<ApplicationMessage> lstMessages;

            using (var fileStream = new FileStream(applicationMessageLibraryFile, FileMode.Open))
                lstMessages = (List<ApplicationMessage>) xmlSerializer.Deserialize(fileStream);

            foreach (var appMessage in lstMessages)
                messages.Add(appMessage.Id, appMessage);
        }

        private void Log(string appMessage, string logMessage, TextWriter w)
        {
            w.Write("\r\nLog Entry : ");
            w.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(),
                DateTime.Now.ToLongDateString());
            w.WriteLine("  :{0}", appMessage);
            w.WriteLine("  :{0}", logMessage);
            w.WriteLine("-------------------------------");
        }

    }
}
