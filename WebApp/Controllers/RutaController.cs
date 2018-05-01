using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApp.Controllers
{
    public class RutaController : Controller
    {
        // GET: Ruta
        public ActionResult Index()
        {
            return View("vLista");
        }

        public ActionResult vVer()
        {
            return View();
        }

        public ActionResult vRegistrar()
        {
            return View();
        }

        public ActionResult vModificar()
        {
            return View();
        }
    }
}