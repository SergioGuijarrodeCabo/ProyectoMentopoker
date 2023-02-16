using Microsoft.AspNetCore.Mvc;
using ProyectoMentopoker.Models;
using ProyectoMentopoker.Repositories;
namespace ProyectoMentopoker.Controllers
{
    public class TablasController : Controller
    {
        private RepositoryTablas repo;

        public IActionResult Index()
        {
           
            return View();

        }


        public IActionResult GetTabla()
        {
         
            return View();
        }

        [HttpPost]
        public IActionResult GetTabla(int id)
        {
            this.repo = new RepositoryTablas();
            List<Celda> tabla = this.repo.GetTabla(id);
            return View(tabla);
        }


        public IActionResult JugarPartidaSinTabla()
        {

            return View();
        }


        public IActionResult JugarPartidaConTabla()
        {

            return View();
        }

        [HttpPost]
        public IActionResult JugarPartidaConTabla(int id)
        {
            this.repo = new RepositoryTablas();
            List<Celda> tabla = this.repo.GetTabla(id);
            return View(tabla);
        }
    }
}
