using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApp.Controllers
{
    public class TransactionController : Controller
    {
        // GET: Transaction
        public ActionResult Index()
        {
            return View("vTransaccionTarjeta");
        }

        public ActionResult AllTransactions()
        {
            return View("vTransaccionesGeneral");
        }

        public ActionResult TerminalTransactions()
        {
            return View("vTransaccionesTerminal");
        }

        public ActionResult UserTransactions()
        {
            return View("vTransaccionesPasajero");
        }

        public ActionResult ListaDePagos()
        {
            return View("vListarPagosEmpresa");
        }

        public ActionResult RealizarPago()
        {
            return View("vRealizarPago");
        }
    }
}
