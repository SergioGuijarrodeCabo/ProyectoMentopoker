using Microsoft.AspNetCore.Mvc;
using ProyectoMentopoker.Models;
using ProyectoMentopoker.Repositories;
namespace ProyectoMentopoker.Controllers
{
    public class EstadisticasController : Controller
    {
        private RepositoryTablas repoTablas;
        private RepositoryEstadisticas repoStats;

        public EstadisticasController(RepositoryEstadisticas repoStats)
        {
            this.repoTablas = new RepositoryTablas();
            this.repoStats = repoStats;
        }

        public IActionResult VerPartidas()
        {
            var usuario_id = HttpContext.Session.GetString("ID");

            if (usuario_id == null)
            {
                usuario_id = "1";
            }
            ConjuntoPartidasUsuario conjunto = this.repoStats.GetPartidas(int.Parse(usuario_id));

            return View(conjunto);
        }


        [HttpPost]
        public IActionResult VerPartidas(DateTime fecha)
        {

            var usuario_id = HttpContext.Session.GetString("ID");

            if (usuario_id == null)
            {
                usuario_id = "1";
            }
            ConjuntoPartidasUsuario conjunto = this.repoStats.GetPartidas(int.Parse(usuario_id), fecha);

            return View(conjunto);

        }

    }
}
