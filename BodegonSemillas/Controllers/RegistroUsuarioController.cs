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

			var errorContraseñas = VerificarPasswords(cliente.Password, Request.Form["password2"]);
			if (errorContraseñas != null)
			{
				ModelState.AddModelError("Password", errorContraseñas);
				return View(cliente);
			}

			//Validaciones del email
			if (cliente.Email == null) 
			{
				ViewData["Error"] = "El correo electrónico es nulo";
				return View();
			}

			// Llmada metodo Post validar email
			var emailValido = await ValidarEmail("\"" + cliente.Email + "\"");

			if (emailValido.correoValido == false)
			{
                // Email ya está registrado
                ViewData["Error"] = "Correo electronico en uso";
				return View("Index", cliente);
			}
			else
			{
				// Email válido
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

		private string? VerificarPasswords(string password1, string password2)
		{
			if (password1 != password2)
			{
				return "Las contraseñas no coinciden";
			}

			if (password1.Length < 8)
			{
				return "La contraseña debe tener al menos 8 caracteres";
			}

			return null;
		}


	}
}
