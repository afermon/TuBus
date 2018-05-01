using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApp.Controllers
{
    public class ReportController : Controller
    {
        public ActionResult Loading()
        {
            return View("vLoading");
        }
        
        public ActionResult Index()
        {
            return View("vReporteGanaciasTerminales");
        }

        public ActionResult ReporteTipoTransations()
        {
            return View("vReportePorTipoGanacia");
        }

        public ActionResult ReporteTipoTarjeta()
        {
            return View("vReporteTipoTarjeta"); 
        }
    }
}