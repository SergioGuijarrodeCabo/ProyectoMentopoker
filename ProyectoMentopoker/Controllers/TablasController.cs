using Microsoft.AspNetCore.Mvc;
using ProyectoMentopoker.Models;
using ProyectoMentopoker.Repositories;
namespace ProyectoMentopoker.Controllers
{
    public class TablasController : Controller
    {
        private RepositoryTablas repo;

        public TablasController()
        {
            this.repo = new RepositoryTablas();
        }

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
        public List<Celda> JugarPartidaConTabla(int id)
        {
            this.repo = new RepositoryTablas();
            List<Celda> tabla = this.repo.GetTabla(id);
            return tabla;
        }

        public IActionResult insertarPartida()
        {

            return View();
        }

        [HttpPost]
        public IActionResult insertarPartida(int[] ids_Jugadas, int[] ids_Rondas, double[] ganancias_Rondas, double[] cantidades_Rondas,
            string[] cell_ids_Jugadas, int[] table_ids_Jugadas, double[] cantidades_Jugadas,
            Boolean[] seguimiento_jugadas, double dineroInicial, double dineroActual)
        {

            // Create a JSON object to return as the response
            var result = new
            {
                success = true,
                message = "Partida insertada correctamente"
            };

            this.repo.insertPartida( ids_Jugadas, ids_Rondas, ganancias_Rondas, cantidades_Rondas, cell_ids_Jugadas, table_ids_Jugadas, cantidades_Jugadas, seguimiento_jugadas, dineroInicial, dineroActual);

            return Json(result);
        }

        public IActionResult Prueba()
        {
            return View();
        }
    }
}
