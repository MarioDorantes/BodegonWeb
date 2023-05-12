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
			if (cliente.Email == null) // Verifica si el correo electrónico es nulo
			{
				ViewData["Error"] = "El correo electrónico es nulo";
				return View();
			}

			// Primero, validamos el correo electrónico
			var emailValido = await ValidarEmail("\"" + cliente.Email + "\"");

			if (emailValido.correoValido == false)
			{
				// El correo electrónico ya está registrado, mostramos un mensaje de error al usuario
				ModelState.AddModelError("Email", "El correo electrónico ya está registrado");
				return View(cliente);
			}
			else
			{
				// El correo electrónico es válido, podemos continuar con el registro
				var handler = new HttpClientHandler();
				handler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;

				var json = JsonConvert.SerializeObject(cliente);
				var content = new StringContent(json, Encoding.UTF8, "application/json");

				using (var client = new HttpClient(handler))
				{
					ServicePointManager.ServerCertificateValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;

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
						return View();
					}
				}
			}
		}


		[HttpPost]
		public async Task<dynamic> ValidarEmail([FromBody] string email)
		{
			var handler = new HttpClientHandler();
			handler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;

			using (var client = new HttpClient(handler))
			{
				ServicePointManager.ServerCertificateValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;

				try
				{
					var content = new StringContent(email, Encoding.UTF8, "application/json");
					var response = await client.PostAsync("http://192.168.100.10:5297/cliente/validateaccount", content);

					if (response.IsSuccessStatusCode)
					{
						var result = await response.Content.ReadAsStringAsync();
						bool correoValido = (result.Trim().ToLower() == "true") ? false : true;
						return new { correoValido };
					}
					else
					{
						ViewData["Error"] = "Error en el servidor";
						return new { correoValido = false };
					}
				}
				catch (HttpRequestException ex)
				{
					Console.WriteLine("La conexión falló" + ex.Message);
				}
			}

			return new { correoValido = false };
		}

	}
}
