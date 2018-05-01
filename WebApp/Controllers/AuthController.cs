using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApp.Controllers
{
    public class AuthController : Controller
    {
        // GET: Auth
        public ActionResult vLogin()
        {
            return View();
        }

        // GET: Auth
        public ActionResult vForgotPassword()
        {
            return View();
        }
    }
}