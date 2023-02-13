namespace ProyectoMentopoker.Models
{
    public class Celda
    {
        public int Identificador { get; set; }
        public int Table_id { get; set; }       
        public string Cell_id { get; set; } 
        public string Background_color { get; set; }    
        public string Color { get; set; }   

        public Celda(int identificador, int table_id, string cell_id, string background_color, string color)
        {
            this.Identificador = identificador;
            this.Table_id = table_id;
            this.Cell_id = cell_id;
            this.Background_color = background_color;
            this.Color = color;

        }
    }
}
