using BodegonSemillas.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace BodegonSemillas.Controllers
{
    public class RegistroUsuarioController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

		[HttpPost]
		public async Task<IActionResult> RegistrarClienteAsync(Cliente cliente)
		{

			var handler = new HttpClientHandler();
			handler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
			

			var json = JsonConvert.SerializeObject(cliente);
			var content = new StringContent(json, Encoding.UTF8, "application/json");

			using (var client = new HttpClient(handler))
			{
				ServicePointManager.ServerCertificateValidationCallback =
				(sender, cert, chain, sslPolicyErrors) => true;

				try
				{
					var response = await client.PostAsync("http://192.168.100.10:5297/cliente/signup", content);
					if (response.IsSuccessStatusCode)
					{
						var result = await response.Content.ReadAsStringAsync();
						Console.WriteLine(result);
						return RedirectToAction("Index", "Home");
					}
					else
					{
						ViewData["Error"] = "Hubo un problema al registrar el cliente";
						return View();
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
