namespace ProyectoMentopoker.Models
{
    public class Celda
    {
        public int Identificador { get; set; }
        public int Table_id { get; set; }       
        public string Id_celda { get; set; } 
        public string Background_color { get; set; }    
        public string Color { get; set; }   

        public Celda(int identificador, int table_id, string id_celda, string background_color, string color)
        {
            this.Identificador = identificador;
            this.Table_id = table_id;
            this.Id_celda = id_celda;
            this.Background_color = background_color;
            this.Color = color;

        }
    }
}
