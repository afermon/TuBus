using System.Web.Mvc;

namespace WebApp.Controllers
{
    public class QuejasController : Controller
    {
        // GET: Quejas
        public ActionResult Index()
        {
            return View("vListarQuejas");
        }

        public ActionResult vRegistrarQuejas()
        {
            return View();
        }

        public ActionResult vListarQuejas()
        {
            return View();
        }

        public ActionResult vVerQueja()
        {
            return View();
        }
    }
}