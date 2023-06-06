using BodegonSemillas.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Sockets;
using System.Text;
using static System.Net.WebRequestMethods;

namespace BodegonSemillas.Controllers
{
    public class InicioSesionController : Controller
    {
        //Clientes
        string baseURL = "http://192.168.100.10:5297";

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> IndexAsync(Credenciales credenciales)
        {

            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;


            var json = JsonConvert.SerializeObject(credenciales);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            using (var client = new HttpClient(handler))
            {
                ServicePointManager.ServerCertificateValidationCallback =
                (sender, cert, chain, sslPolicyErrors) => true;

                try
                {
                    var response = await client.PostAsync("http://192.168.100.10:5297/cliente/login", content);
                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        var idCliente = result;
                        int idClienteParseado = int.Parse(idCliente);
                        await ObtenerNombreUsuario(idClienteParseado);
                        HttpContext.Session.SetString("IdCliente", idClienteParseado.ToString());
                        //Informacion para session
                        HttpContext.Session.SetString("IsLoggedIn", true.ToString());
                        return RedirectToAction("Index", "Home", new { IsLoggedIn = true });
                    }
                    else
                    {
                        ViewData["Error"] = "Credenciales incorrectas";
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

        [HttpGet]
        public async Task<IActionResult> ObtenerNombreUsuario(int idUsuario)
        {

            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;

            using (var client = new HttpClient(handler))
            {
                ServicePointManager.ServerCertificateValidationCallback =
                (sender, cert, chain, sslPolicyErrors) => true;

                client.BaseAddress = new Uri(baseURL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                try
                {
                    HttpResponseMessage response = await client.GetAsync("cliente/getusername?idUsuario=" + idUsuario);

                    if (response.IsSuccessStatusCode)
                    {
                        string results = response.Content.ReadAsStringAsync().Result;
                        var nombreUsuarioLogueado = JsonConvert.DeserializeObject(results);
                        TempData["NombreUsuarioLogueado"] = nombreUsuarioLogueado.ToString();
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

        public IActionResult CerrarSesion()
        {
            HttpContext.Session.Remove("IsLoggedIn");
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }


    }
}
