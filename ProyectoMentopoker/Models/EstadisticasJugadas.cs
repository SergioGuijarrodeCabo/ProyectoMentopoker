namespace ProyectoMentopoker.Models
{
    public class EstadisticasJugadas
    {
      public List<JugadasCalculadasModel> Jugadas { get; set; }

        public List<string> Rondas_ids { get; set; }
        public List<double> GananciasRondasAcumuladas { get; set; }
        public List<double> MediaGananciasRondas { get; set; }
        public List<double> RentabilidadRondas { get; set; }
        public List<string> SeguimientoRondas { get; set; }
    }
}
