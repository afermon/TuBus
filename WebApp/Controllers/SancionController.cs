using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApp.Controllers
{
    public class SancionController : Controller
    {
        // GET: Sancion
        public ActionResult Index()
        {
            return View("vListarSanciones");
        }

        public ActionResult vRegistrarSancion()
        {
            return View();
        }

        public ActionResult vListarSanciones()
        {
            return View();
        }

        public ActionResult vVerSancion()
        {
            return View();
        }

        public ActionResult vModificarSancion()
        {
            return View();
        }
    }
}