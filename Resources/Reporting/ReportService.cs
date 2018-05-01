using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Channels;
using Resources.TuBus_SSRS_Service;

namespace Resources.Reporting
{
    public class ReportService
    {
        public string RunReport(string reportName, Dictionary<string, string> dicParams)
        {
            string reportsPath = "TUBUS_REPORTS";
            var format = "";
            var result = "";
            try
            {
                string encoding;
                string mimeType;
                string extension;
                Warning[] warnings = null;
                string[] streamIDs = null;

                string historyID = null;
                byte[] reportResult = null;
                var devInfo = @"<DeviceInfo><Toolbar>False</Toolbar></DeviceInfo>";

                var rs = new ReportExecutionServiceSoapClient(GetDefaultBinding(), new EndpointAddress(@"http://localhost:8282/ReportServer/ReportExecution2005.asmx"));
                rs.ClientCredentials.Windows.AllowedImpersonationLevel = System.Security.Principal.TokenImpersonationLevel.Impersonation;
                rs.ClientCredentials.Windows.ClientCredential = System.Net.CredentialCache.DefaultNetworkCredentials;

                // Render arguments                                                        
                var execInfo = new ExecutionInfo();
                var execHeader = new ExecutionHeader();
                var trustedHeader = new TrustedUserHeader();
                var serverInfo = new ServerInfoHeader();

                var path2 = "/" + reportsPath + "/" + reportName;
                rs.LoadReport(trustedHeader, path2, historyID, out serverInfo, out execInfo);

                // Genera los parametros apartir de un diccionario
                var parameters = new ParameterValue[dicParams.Count];
                var index = 0;

                foreach (var param in dicParams)
                {
                    var paramValue = new ParameterValue
                    {

                        Name = param.Key,
                        Value = param.Value
                    };
                    parameters[index] = paramValue;
                    index++;
                }

                execHeader.ExecutionID = execInfo.ExecutionID;
                rs.SetExecutionParameters(execHeader, trustedHeader, parameters, "en-us", out execInfo);

                //Invocacion del reporte
                format = "PDF";

                rs.Render(execHeader, trustedHeader, format, devInfo, out reportResult, out extension, out mimeType, out encoding, out warnings, out streamIDs);

                result = Convert.ToBase64String(reportResult);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        internal static Binding GetDefaultBinding()
        {
            BasicHttpBinding binding = new BasicHttpBinding();
            binding.MaxBufferPoolSize = 0;
            binding.MaxReceivedMessageSize = 5242880;
            binding.HostNameComparisonMode = HostNameComparisonMode.StrongWildcard;
            binding.TextEncoding = System.Text.Encoding.UTF8;
            binding.MessageEncoding = WSMessageEncoding.Text;
            binding.TransferMode = TransferMode.Buffered;
            binding.UseDefaultWebProxy = true;
            binding.ReaderQuotas = new System.Xml.XmlDictionaryReaderQuotas
            {
                MaxArrayLength = 2147483647,
                MaxBytesPerRead = 2147483647,
                MaxDepth = 2147483647,
                MaxNameTableCharCount = 2147483647,
                MaxStringContentLength = 2147483647
            };
            binding.Security.Mode = BasicHttpSecurityMode.TransportCredentialOnly;
            binding.Security.Transport.ClientCredentialType = HttpClientCredentialType.Ntlm;
            return binding;
        }
    }
}
