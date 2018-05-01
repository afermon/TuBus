using System;
using System.Collections.Generic;
using System.IO;
using DataAccess.Crud;
using Entities;
using Exceptions;
using Resources.Reporting;

namespace CoreAPI
{
    public class ReportManager : BaseManager
    {
        private readonly ReportService _reportService;
        private readonly ReportCrudFactory _crudFactory;
        public ReportManager()
        {
            _reportService = new ReportService();
            _crudFactory = new ReportCrudFactory();
        }

        public MemoryStream ReporteTest( string role)
        {
            byte[] report = null;

            try
            {
                var parameters = new Dictionary<string, string>();
                parameters.Add("P_TEST", role);
                // Aqui se agregan los parametros para el reporte > Ej: Terminal id o empresa 
                
                var result = _reportService.RunReport("RP_TEST_PARAMETROS", parameters); //Nombre del reporte

                report = Convert.FromBase64String(result);
            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
            }

            return new MemoryStream(report);
        }

        public MemoryStream GetReportAllGanancias()
        {
            byte[] report = null;

            try
            {
                var parameters = new Dictionary<string, string>();
                
                var result = _reportService.RunReport("GANACIAS_ALL_TERMINALES", parameters); //Nombre del reporte

                report = Convert.FromBase64String(result);
            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
            }

            return new MemoryStream(report);
        }

        public MemoryStream GetReportAllTransactiontipo()
        {
            byte[] report = null;

            try
            {
                var parameters = new Dictionary<string, string>();

                var result = _reportService.RunReport("NUMERO_TRANSACCIONES_POR_TIPO", parameters); //Nombre del reporte

                report = Convert.FromBase64String(result);
            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
            }

            return new MemoryStream(report);
        }

        public MemoryStream GetReportAllTipoTarjeta()
        {
            byte[] report = null;

            try
            {
                var parameters = new Dictionary<string, string>();

                var result = _reportService.RunReport("NUMERO_TRANSACTION_TIPO_TARJETA", parameters); //Nombre del reporte

                report = Convert.FromBase64String(result);
            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
            }

            return new MemoryStream(report);
        }

        public List<Report> ObtenerTodasGanaciasGenerales()
        {
            try
            {
                return _crudFactory.RetriveAllBenefits<Report>();
            }
            catch (Exception e)
            {
                ExceptionManager.GetInstance().Process(e);
            }
            return new List<Report>();
        }

        public List<Report> ObtenerTodasTransaccionesPorTipo()
        {
            try
            {
                return _crudFactory.RetriveAllTransactionsByType<Report>();
            }
            catch (Exception e)
            {
                ExceptionManager.GetInstance().Process(e);
            }
            return new List<Report>();
        }

        public List<Report> ObtenerTodasPorTipoTarjeta()
        {
            try
            {
                return _crudFactory.RetriveAllBenefitsByCard<Report>();
            }
            catch (Exception e)
            {
                ExceptionManager.GetInstance().Process(e);
            }
            return new List<Report>();
        }

        public List<Report> ObtenerTodosMovimientos(string email)
        {
            try
            {
                return _crudFactory.RetriveMoviminetosUsuario<Report>(email);
            }
            catch (Exception e)
            {
                ExceptionManager.GetInstance().Process(e);
            }
            return new List<Report>();
        }

    }
}
