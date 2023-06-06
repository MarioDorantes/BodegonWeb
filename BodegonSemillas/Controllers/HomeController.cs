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
        string baseProductosURL = "http://192.168.100.10:5014";


		private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(bool? IsLoggedIn)
        {
            ViewBag.IsLoggedIn = IsLoggedIn ?? false;
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

        //GET PARA LAS SUCURSALES, POR DEFECTO HASTA IMPLEMENTAR UBICACIÓN, ES XALAPA, VERACRUZ
        [HttpGet]
		public async Task<IActionResult> ObtenerSucursalesCiudad()
		{
            string ciudad = "XAL";
            string estado = "VER";

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


		[HttpGet]
		public async Task<IActionResult> ObtenerCategorias()
		{

			var handler = new HttpClientHandler();
			handler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;

			using (var client = new HttpClient(handler))
			{
				ServicePointManager.ServerCertificateValidationCallback =
				(sender, cert, chain, sslPolicyErrors) => true;

				client.BaseAddress = new Uri(baseProductosURL);
				client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

				try
				{
					HttpResponseMessage response = await client.GetAsync("productos/categorias");

					if (response.IsSuccessStatusCode)
					{
						string json = await response.Content.ReadAsStringAsync();
						List<VistaCategoria> categorias = JsonConvert.DeserializeObject<List<VistaCategoria>>(json);
						return Json(categorias);
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