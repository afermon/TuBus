using System.Web.Mvc;

namespace WebApp.Controllers
{
    public class ChoferController : Controller
    {
        // GET: Chofer
        public ActionResult Index()
        {
            return View("vListarChofer");
        }

        public ActionResult vRegistrarChofer()
        {
            return View();
        }

        public ActionResult vListarChofer()
        {
            return View();
        }

        public ActionResult vVerChofer()
        {
            return View();
        }

        public ActionResult vModificarChofer()
        {
            return View();
        }
    }
}