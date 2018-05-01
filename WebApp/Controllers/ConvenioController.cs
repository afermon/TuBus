using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using Entities;
using Newtonsoft.Json;
using WebApp.Models.Controls;

namespace WebApp.Controllers
{
    public class ConvenioController : Controller
    {
        public ActionResult Index()
        {
            return View("vListaConvenio");
        }

        public ActionResult Actualizar()
        {
            return View("vUpdateConvenio");
        }

        public ActionResult Crear()
        {
            return View("vCrearConvenio");
        }

        public ActionResult ActivarTarjetasConvenio()
        {
            var url = Request.Url.Query;
            var decodedUrl = HttpUtility.UrlDecode(url);
            var apiUrl = ConfigurationManager.AppSettings["TubusApi"] + "api/0/Convenio/ProcesarSolicitud" + decodedUrl;
            var solicitudTarjeta = GetResult(apiUrl);
            var existsSolicitud = string.IsNullOrEmpty(solicitudTarjeta.Message);

            if (existsSolicitud)
                return View("../Auth/vLogin", new MessageViewModel { Message = "¡Error! ", DescirptionMessage = "Esta solicitud ya fue aprobada por el administrador del convenio", ShowMeesage = true, Style = "alert-danger" });

            return View("../Auth/vLogin", new MessageViewModel { Message = "¡Éxito! ", DescirptionMessage = "Se ha aprobado la solicitud", ShowMeesage = true, Style = "alert-success" });
        }

        private static ResponseTuBusApi GetResult(string url)
        {
            using (var client = new HttpClient())
            {
                var response = client.PutAsync(url,null).Result;
                if (!response.IsSuccessStatusCode) return new ResponseTuBusApi();
                var responseContent = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<ResponseTuBusApi>(responseContent);
            }
        }

        public ActionResult ListaTarjetasConvenio()
        {
            return View("vListaTarjetasConvenios");
        }

    }
}
