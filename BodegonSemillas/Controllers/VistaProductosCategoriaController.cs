using BodegonSemillas.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;

namespace BodegonSemillas.Controllers
{
	public class VistaProductosCategoriaController : Controller
	{

        string baseProductosURL = "https://192.168.100.10:5014";

        //Vista de productos por sucursal y categoria que se mostraran
        public IActionResult Index(string clave, string nombre)
		{
            ViewBag.ClaveCategoria = clave;
            ViewBag.NombreCategoria = nombre;
            return View();
		}

        [HttpGet]
        public async Task<IActionResult> ObtenerProductosPorSucursalYCategoria(string clave, int pagina = 1)
        {
            //id de la sucursal
            int id = 60;
            string categoria = clave;

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
                    HttpResponseMessage response = await client.GetAsync("productos?id=" + id + "&categoria=" + categoria + "&pagina=" + pagina);

                    if (response.IsSuccessStatusCode)
                    {
                        string json = await response.Content.ReadAsStringAsync();
                        VistaProductosResponse apiResponse = JsonConvert.DeserializeObject<VistaProductosResponse>(json);
                        List<VistaProducto> productos = apiResponse.Products;
                        return Ok(productos);
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
