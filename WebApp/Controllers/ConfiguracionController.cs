using System.Web.Mvc;

namespace WebApp.Controllers
{
    public class ConfiguracionController : Controller
    {
        public ActionResult Index()
        {
            return View("vGeneral");
        }

        public ActionResult vTerminal()
        {
            return View();
        }
    }
}