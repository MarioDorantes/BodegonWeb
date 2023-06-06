using BodegonSemillas.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;
using System.Text;

namespace BodegonSemillas.Controllers
{
    public class RealizarPedidoController : Controller
    {

        string baseClientesURL = "http://192.168.100.10:5297";

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerDirecciones()
        {

            string cliente = HttpContext.Session.GetString("IdCliente");
            //Es la sucursal seleccionada, es nombrada como dirección por como se construyó en el servicio API
            int direccion = 60;

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
                    HttpResponseMessage response = await client.GetAsync("direccion/all?cliente=" + cliente + "&direccion=" + direccion);

                    if (response.IsSuccessStatusCode)
                    {
                        string json = await response.Content.ReadAsStringAsync();
                        List<VistaDireccion> direcciones = JsonConvert.DeserializeObject<List<VistaDireccion>>(json);
                        return Json(direcciones);
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

        [HttpPost]
        public async Task<IActionResult> RegistrarNuevoPedido([FromBody] PedidoCarritoInfo carritoPedido)
        {

            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;


            var json = JsonConvert.SerializeObject(carritoPedido);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            using (var client = new HttpClient(handler))
            {
                ServicePointManager.ServerCertificateValidationCallback =
                (sender, cert, chain, sslPolicyErrors) => true;

                try
                {
                    var response = await client.PostAsync("http://192.168.100.10:5269/pedido", content);
                    if (response.IsSuccessStatusCode)
                    {
                        return Json(new { success = true });
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


    }
}
