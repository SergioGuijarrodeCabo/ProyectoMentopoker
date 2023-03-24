using Microsoft.AspNetCore.Mvc;

namespace ProyectoMentopoker.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
