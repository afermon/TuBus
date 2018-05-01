using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApp.Controllers
{
    public class TiposTarjetasController : Controller
    {
        // GET: TiposTarjetas
        public ActionResult Index()
        {
            return View("vListaTipoTarjeta");
        }

        public ActionResult Actualizar()
        {
            return View("vUpdateTipoTarjeta");
        }

        public ActionResult CreateTipo()
        {
            return View("vCrearTipoTarjeta");
        }
    }
}
