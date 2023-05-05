using Microsoft.AspNetCore.Mvc;

namespace BodegonSemillas.Controllers
{
    public class FavoritosController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
