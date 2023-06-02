using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Net;
using BodegonSemillas.Models;
using Newtonsoft.Json;

namespace BodegonSemillas.Controllers
{
    public class CarritoController : Controller
    {

        string baseProductosURL = "http://192.168.100.10:5014";

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerCarritoPorSucursal()
        {

            int cliente = 33526;
            int sucursal = 60;

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
                    HttpResponseMessage response = await client.GetAsync("carrito?cliente=" + cliente + "&sucursal=" + sucursal);

                    if (response.IsSuccessStatusCode)
                    {
                        string json = await response.Content.ReadAsStringAsync();
                        VistaCarrito apiResponse = JsonConvert.DeserializeObject<VistaCarrito>(json);
                        List<VistaProducto> productos = apiResponse.Productos;
                        var totalCarrito = apiResponse.TotalCarrito; // Obtener el total del carrito
                        return Ok(new { Productos = productos, totalCarrito });
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
