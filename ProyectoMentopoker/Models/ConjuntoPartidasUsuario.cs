namespace ProyectoMentopoker.Models
{
    public class ConjuntoPartidasUsuario
    {
        public List<PartidaModel> Partidas { get; set;}
        public List<RondaModel> Rondas { get;set;}
        public List<JugadaModel> Jugadas { get; set; }

        public EstadisticasPartidas Estadisticas { get; set; }


    }
}
