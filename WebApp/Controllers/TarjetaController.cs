using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApp.Controllers
{
    public class TarjetaController : Controller
    {
        // Solicitar Tarjeta
        public ActionResult Index()
        {
            return View("vTarjetasGeneral");
        }

        public ActionResult SolicitarTarjeta()
        {
            return View("vSolicitarTarjeta");
        }

        public ActionResult TarjetasPasajero()
        {
            return View("vTarjetasPasajero");
        }
        
        public ActionResult ReposicionTarjetas()
        {
            return View("vReposicionTarjetas");
        }

    }
}
