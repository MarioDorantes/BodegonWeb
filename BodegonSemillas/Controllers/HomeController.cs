using BodegonSemillas.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.Diagnostics;
using System.Net;
using System.Net.Http.Headers;

namespace BodegonSemillas.Controllers
{
    public class HomeController : Controller
    {

        string baseClientesURL = "http://192.168.100.10:5297";

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        /*
        public async Task<IActionResult> Index()
        {
            
            DataTable dt = new DataTable();
            using(var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseURL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage getData = await client.GetAsync("productos/saludar");
                
                if (getData.IsSuccessStatusCode)
                {
                    string results = getData.Content.ReadAsStringAsync().Result;
                    dt=JsonConvert.DeserializeObject<DataTable>(results);
                }
                else
                {
                    Console.WriteLine("Error en al api");
                }
            }

            return View();
        }*/
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        //[FromBody] string ciudad, [FromBody] string estado

        //GET PARA LAS SUCURSALES
        [HttpGet]
		public async Task<IActionResult> ObtenerSucursalesCiudad()
		{
            string ciudad = "Xalapa";
            string estado = "Veracruz";

            var handler = new HttpClientHandler();
			handler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;

			using (var client = new HttpClient(handler))
			{
				ServicePointManager.ServerCertificateValidationCallback =
				(sender, cert, chain, sslPolicyErrors) => true;

				client.BaseAddress = new Uri(baseClientesURL);
				client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

				try
				{
                    HttpResponseMessage response = await client.GetAsync("sucursal/sucursales?ciudad=" + ciudad + "&estado=" + estado);

                    if (response.IsSuccessStatusCode)
                    {
                        string json = await response.Content.ReadAsStringAsync();
                        List<SucursalApp> sucursales = JsonConvert.DeserializeObject<List<SucursalApp>>(json);
                        return Json(sucursales);
                    }
                    else
                    {
                        Console.WriteLine("Ocurrió un error, intente más tarde");
                    }
                }
				catch (HttpRequestException ex)
				{
					Console.WriteLine("La conexión falló" + ex.Message);
				}

			}

			return View();
		}

	}
}