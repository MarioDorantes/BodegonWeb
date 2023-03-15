using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BodegonSemillas.Controllers
{
    public class LoginController : Controller
    {

        string baseURL = "http://192.168.100.10:5014";


        public async Task<IActionResult> MyAction()
        {
            using var httpClient = new HttpClient();
            var response = await httpClient.GetAsync("http://192.168.100.10:5014/productos/saludar");
            var content = await response.Content.ReadAsStringAsync();
            return View("MyView", content);
        }


    }
    
}
