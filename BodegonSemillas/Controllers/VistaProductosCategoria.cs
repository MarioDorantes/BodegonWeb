using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Http.Headers;

namespace BodegonSemillas.Controllers
{
	public class VistaProductosCategoria : Controller
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
        public async Task<IActionResult> ObtenerProductosPorSucursalYCategoria()
        {

            int id = 60;
            int categoria = int.Parse(ViewBag.ClaveCategoria);
            int pagina = 1;

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
                        /*
						string json = await response.Content.ReadAsStringAsync();
						List<VistaCategoria> categorias = JsonConvert.DeserializeObject<List<VistaCategoria>>(json);
						return Json(categorias);*/
                        Console.WriteLine("Funciona bien");
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
