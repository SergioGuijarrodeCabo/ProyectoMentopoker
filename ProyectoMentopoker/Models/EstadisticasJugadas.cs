namespace ProyectoMentopoker.Models
{
    public class EstadisticasJugadas
    {
      public List<JugadasCalculadasModel> Jugadas { get; set; }

        public List<string> SeguimientoRondas { get; set; }
        public List<double> CantidadesJugadasRondas { get; set; }
        public List<double> GananciasRondas { get; set; }
        public List<double> RentabilidadRondas { get; set; }
        public List<string> Rondas_ids { get; set; }


        public EstadisticasJugadas()
        {
            this.Rondas_ids = new List<string>();
            this.CantidadesJugadasRondas = new List<double>();
            this.GananciasRondas = new List<double>();
            this.RentabilidadRondas = new List<double>(); 
            this.SeguimientoRondas = new List<string>();

        }
    }
}
