using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApp.Controllers
{
    public class MultaController : Controller
    {
        // GET: Multa
        public ActionResult Index()
        {
            return View("vListarMultas");
        }

        public ActionResult vListarMultas()
        {
            return View();
        }

        public ActionResult vVerMulta()
        {
            return View();
        }

        public ActionResult vRegistrarMulta()
        {
            return View();
        }

        public ActionResult vModificarMulta()
        {
            return View();
        }
    }
}