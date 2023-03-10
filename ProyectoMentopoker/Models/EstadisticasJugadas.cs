namespace ProyectoMentopoker.Models
{
    public class EstadisticasJugadas
    {
      public List<JugadasCalculadasModel> Jugadas { get; set; }

        public List<string> SeguimientoTipoRondas { get; set; }
        public List<double> CantidadesJugadasTipoRondas { get; set; }
        public List<double> GananciasTipoRondas { get; set; }
        public List<double> RentabilidadTipoRondas { get; set; }
        public List<string> Rondas_ids { get; set; }




        public EstadisticasJugadas()
        {
            this.Rondas_ids = new List<string>();
           
            this.CantidadesJugadasTipoRondas = new List<double>();
            this.CantidadesJugadasTipoRondas.Add(0);
            this.CantidadesJugadasTipoRondas.Add(0);
            this.CantidadesJugadasTipoRondas.Add(0);
            this.GananciasTipoRondas = new List<double>();
            this.GananciasTipoRondas.Add(0);
            this.GananciasTipoRondas.Add(0);
            this.GananciasTipoRondas.Add(0);
            this.RentabilidadTipoRondas = new List<double>();
            this.RentabilidadTipoRondas.Add(0);
            this.RentabilidadTipoRondas.Add(0);
            this.RentabilidadTipoRondas.Add(0);
            this.SeguimientoTipoRondas = new List<string>();
 
        }
    }
}
