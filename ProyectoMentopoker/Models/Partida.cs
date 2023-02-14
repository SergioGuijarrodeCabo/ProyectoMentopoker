namespace ProyectoMentopoker.Models
{
    public class Partida
    {
        public string Partida_id { get; set; }
        public double Cash_Inicial { get; set; }
        public double Cash_Final { get; set; }
        public DateOnly Fecha { get; set; }
        public string Comentarios { get; set; }
        public string Usuario_id { get; set; }
    }
}
