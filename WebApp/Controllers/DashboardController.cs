using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApp.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Dashboard
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult vAdministrador()
        {
            return View();
        }

        public ActionResult vTerminal()
        {
            return View();
        }

        public ActionResult vEmpresario()
        {
            return View();
        }

        public ActionResult vPasajero()
        {
            return View();
        }
    }
}