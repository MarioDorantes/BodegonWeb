using BodegonSemillas.Models;
using BodegonSemillas.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Net.Http.Json;
using System.Text;

namespace BodegonSemillas.Controllers
{
    public class InicioSesionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(Cliente cliente)
        {

            using (var httpClient = new HttpClient())
             {

                //httpClient.DefaultRequestHeaders.Accept.Clear();
                //HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "");

                Credenciales credenciales = new Credenciales(cliente.Email, cliente.Password);

                string json = JsonConvert.SerializeObject(credenciales); 
                using var content = new StringContent(json, Encoding.UTF8, "application/json");
                using var response = await httpClient.PostAsync("https://192.168.100.10:7021/cliente/login", content);

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("paso");
                }
                else
                {
                    Console.WriteLine("jajs no");
                }

                //request.Content = new FormUrlEncodedContent(Credenciales);

                //var stringified = JsonConvert.SerializeObject(Credenciales);
                //var response = await httpClient.PostAsync("", new StringContent(stringified, Encoding.UTF8, "application/json"));

                /*string json = JsonConvert.SerializeObject(credenciales);

                var response = await httpClient.PostAsJsonAsync(url, credenciales);
                                
                */

            }

            return View();

            //ModelState.AddModelError(string.Empty, "Error en el servidor, intente más tarde");
            //return View(Index);

        }


    }
}
