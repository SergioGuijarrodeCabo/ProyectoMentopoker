using Microsoft.AspNetCore.Mvc;

namespace ProyectoMentopoker.Controllers
{
    public class TablasController : Controller
    {
        public IActionResult Index()
        {
            return View();

        }
    }
}
