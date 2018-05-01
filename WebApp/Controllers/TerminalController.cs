
using System.Web.Mvc;

namespace WebApp.Controllers
{
    public class TerminalController : Controller
    {
        // GET: Terminal
        public ActionResult Index()
        {
            return View("vListaTerminales");
        }

        public ActionResult vRegistrarTerminal()
        {
            return View();
        }

        public ActionResult vListaTerminales()
        {
            return View();
        }

        public ActionResult vModificarTerminal()
        {
            return View();
        }

        public ActionResult vVerTerminal()
        {
            return View();
        }
    }
}