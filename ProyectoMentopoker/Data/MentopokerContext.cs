using Microsoft.EntityFrameworkCore;
using ProyectoMentopoker.Models;

namespace ProyectoMentopoker.Data
{
    public class MentopokerContext : DbContext
    {
        public MentopokerContext(DbContextOptions<MentopokerContext> options) : base(options) {}

        public DbSet<Partida> Partidas { get; set; }
    }
}
