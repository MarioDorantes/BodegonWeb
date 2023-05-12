using Microsoft.AspNetCore.Mvc;

namespace BodegonSemillas.Controllers
{
    public class PedidosController : Controller
    {
        //Historial pedidos
        public IActionResult MisPedidos()
        {
            return View();
        }
    }
}
