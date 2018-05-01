using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApp.Controllers
{
    public class LineaController : Controller
    {
        public ActionResult Index()
        {
            return View("vListarLinea");
        }

        public ActionResult Actualizar()
        {
            return View("vUpdateLine");
        }

        public ActionResult Crear()
        {
            return View("vCrearLinea");
        }
    }
}