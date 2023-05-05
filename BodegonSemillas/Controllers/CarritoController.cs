using Microsoft.AspNetCore.Mvc;

namespace BodegonSemillas.Controllers
{
    public class CarritoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
