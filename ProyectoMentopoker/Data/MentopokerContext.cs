using Microsoft.EntityFrameworkCore;
using ProyectoMentopoker.Models;

namespace ProyectoMentopoker.Data
{
    public class MentopokerContext : DbContext
    {
        public MentopokerContext(DbContextOptions<MentopokerContext> options) : base(options) {}

        public DbSet<PartidaModel> Partidas { get; set; }

        public DbSet<RondaModel> Rondas { get; set; }
        public DbSet<JugadaModel> Jugadas { get; set; }
        public DbSet<UsuarioModel> Usuarios { get; set; }
    }
}
