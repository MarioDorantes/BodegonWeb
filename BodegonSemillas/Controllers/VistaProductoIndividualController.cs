using BodegonSemillas.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;

namespace BodegonSemillas.Controllers
{
    public class VistaProductoIndividualController : Controller
    {

        string baseProductosURL = "http://192.168.100.10:5014";

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerProductoPorID(int producto)
        {
            //que acepte nulo o el id cliente

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
                    HttpResponseMessage response = await client.GetAsync("productos/find?producto=" + producto);

                    if (response.IsSuccessStatusCode)
                    {
                        string json = await response.Content.ReadAsStringAsync();
                        var productoIndividual = JsonConvert.DeserializeObject<VistaProducto>(json);
                        return Json(productoIndividual);
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
