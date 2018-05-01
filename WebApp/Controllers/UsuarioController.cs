using System;
using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Entities;
using Newtonsoft.Json;
using WebApp.Models.Controls;

namespace WebApp.Controllers
{
    public class UsuarioController : Controller
    {
        // GET: Usuario
        public ActionResult Index()
        {
            return View("vPerfil");
        }

        public ActionResult vLista()
        {
            return View();
        }

        public ActionResult vVer()
        {
            return View();
        }

        public ActionResult vModificar()
        {
            return View();
        }

        public ActionResult vRegistrar()
        {
            return View();
        }

        public ActionResult ActivarUsuario()
        {
            var url = Request.Url.Query;
            var decodedUrl = HttpUtility.UrlDecode(url);
            var api = ConfigurationManager.AppSettings["TubusApi"];
            var apiUrl = api + "api/0/Tarjeta/GetCardByUniqueCode" + decodedUrl;

            var tarjetaCurrent = GetResult(apiUrl);
            var existsTarjeta = tarjetaCurrent.Data != null;

            if (existsTarjeta)
                return View("../Auth/vLogin", new MessageViewModel{ Message = "¡Error! ", DescirptionMessage = "Esta tarjeta ya ha sido registrada", ShowMeesage = true , Style = "alert-danger" });

            var user = GetResult(api + "api/0/TarjetaUsuario/GetUserByEmail" + decodedUrl);
            var userExists = user.Data != null;

            if (userExists)
            {
                ActivateCardForCreatedUserAsync(apiUrl);
                return View("../Auth/vLogin", new MessageViewModel { Message = "¡Éxito! ", DescirptionMessage = "La tarjeta se ha activado de manera exitosa", ShowMeesage = true, Style = "alert-success" });
            }

            return View("vActivarUsuario");
        }

        private static ResponseTuBusApi GetResult(string url)
        {
            using (var client = new HttpClient())
            {
                var response = client.GetAsync(new Uri(url)).Result;
                if (!response.IsSuccessStatusCode) return new ResponseTuBusApi();
                var responseContent = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<ResponseTuBusApi>(responseContent);
            }
        }

        private void ActivateCardForCreatedUserAsync(string url)
        {
            var uri = new Uri(url);
            var api = ConfigurationManager.AppSettings["TubusApi"];
            var userMail = HttpUtility.ParseQueryString(uri.Query).Get("email");
            var cardUniqueCode = HttpUtility.ParseQueryString(uri.Query).Get("uniqueCode");
            var cardType = Convert.ToInt32(HttpUtility.ParseQueryString(uri.Query).Get("type"));
            var termId = Convert.ToInt32(HttpUtility.ParseQueryString(uri.Query).Get("terminalid"));
            var convenioId = Convert.ToInt32(HttpUtility.ParseQueryString(uri.Query).Get("agreement") ?? "0");


             PostObjAsync(api+"api/0/TarjetaUsuario/CreateCard", new Tarjeta
            {
                TipoTarjeta = new TipoTarjeta { TipoTarjetaId = cardType },
                CodigoUnico = cardUniqueCode,
                Usuario = new Usuario { Email = userMail },
                Terminal = new Terminal { Id = termId },
                Convenio = new Convenio { CedulaJuridica = convenioId }
            });
        }

        private static async Task PostObjAsync(string url, object obj)
        {
            var client = new HttpClient();
            HttpResponseMessage response = await client.PostAsJsonAsync(url, obj);
        }
    }
}
