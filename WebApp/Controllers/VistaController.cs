using System.Web.Mvc;

namespace WebApp.Controllers
{
    public class VistaController : Controller
    {
        // GET: Vista
        public ActionResult vListaVistas()
        {
            return View();
        }
    }
}