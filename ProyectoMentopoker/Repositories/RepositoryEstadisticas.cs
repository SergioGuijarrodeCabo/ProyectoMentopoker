using ProyectoMentopoker.Models;
using ProyectoMentopoker.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;

#region

//CREATE OR ALTER PROCEDURE SP_GET_PARTIDAS
//AS
//SELECT Partida_id, Cash_inicial, Cash_final, Fecha, Comentarios, Usuario_id FROM Partidas
//GO

//CREATE OR ALTER PROCEDURE SP_GET_RONDAS
//(@PARTIDA_ID NVARCHAR(50))
//AS
//SELECT Ronda_id, Cantidad_jugada, Ganancias, Partida_id FROM Rondas WHERE PARTIDA_ID = @PARTIDA_ID
//GO


//CREATE OR ALTER PROCEDURE SP_GET_JUGADAS
//(@RONDA_ID NVARCHAR(50))
//AS
//SELECT Jugada_id, Cantidad_jugada, Seguimiento_Tabla, Identificador, Ronda_id  FROM Jugadas WHERE Ronda_id = @RONDA_ID
//GO





#endregion


namespace ProyectoMentopoker.Repositories
{
    public class RepositoryEstadisticas 
    {
        private MentopokerContext context;


        public RepositoryEstadisticas(MentopokerContext context)
        {
            this.context = context;
        }


        public ConjuntoPartidasUsuario GetPartidas(int Usuario_id, DateTime? fecha = null)
        {
            //string sql = "SP_GET_PARTIDAS";
            //var consulta = this.context.Partidas.FromSqlRaw(sql);
            //List<Partida> partidas = consulta.ToList();

            //for(int i=0;i<partidas.Count;i++)
            //{
            //    partidas[i].Rondas = this.GetRondas(int.Parse(partidas[i].Partida_id));

            //}

            //var consulta = from datos in this.context.Partidas
            //                where datos.Usuario_id == Usuario_id.ToString()
            //                select datos;

            var consulta = from datos in this.context.Partidas
                           where datos.Usuario_id == Usuario_id.ToString() &&
                                 (!fecha.HasValue || datos.Fecha > fecha.Value)
                           select datos;



            ConjuntoPartidasUsuario conjunto = new ConjuntoPartidasUsuario();
            List<PartidaModel> partidas = consulta.ToList();

            List<RondaModel> rondas = new List<RondaModel>();
            List<JugadaModel> jugadas = new List<JugadaModel>();
            
            for (int i=0;i<partidas.Count;i++)
            {
                rondas.AddRange(this.GetRondas(partidas[i].Partida_id));
               
            }
            for (int i = 0; i < rondas.Count; i++)
            {
                jugadas.AddRange(this.GetJugadas(rondas[i].Ronda_id));

            }

            conjunto.Partidas = partidas;
            conjunto.Rondas = rondas;
            conjunto.Jugadas = jugadas;          
            conjunto.Estadisticas =  this.GetEstadisticas(conjunto);



            return conjunto;


        }

        public List<RondaModel> GetRondas(string partida_id)
        {

            var consulta = from datos in this.context.Rondas
                           where datos.Partida_id == partida_id.ToString()
                           select datos;
            List<RondaModel> rondas = consulta.ToList();
            //for (int i = 0; i < rondas.Count; i++)
            //{
            //    rondas[i].Jugadas = this.GetJugadas(rondas[i].Partida_id);

            //}
            return rondas;


        }
      
        public List<JugadaModel> GetJugadas(string ronda_id)
        {
            var consulta = from datos in this.context.Jugadas
                           where datos.Ronda_id == ronda_id.ToString()
                           select datos;
            List<JugadaModel> jugadas = consulta.ToList();
   
            return jugadas;
        }

        public EstadisticasPartidas GetEstadisticas(ConjuntoPartidasUsuario partidas)
        {
            EstadisticasPartidas stats = new EstadisticasPartidas();


            double rentabilidades = 0;

            double medias = 0 ;
            for (int i = 0; i<partidas.Partidas.Count; i++)
            {
                stats.GananciasPartidasAcumuladas += (partidas.Partidas[i].Cash_Final - partidas.Partidas[i].Cash_Inicial);
                stats.CashInicialPartidas += partidas.Partidas[i].Cash_Inicial;
                stats.CashFinalPartidas += partidas.Partidas[i].Cash_Final;
                medias+=(partidas.Partidas[i].Cash_Final - partidas.Partidas[i].Cash_Inicial);
                rentabilidades+=((partidas.Partidas[i].Cash_Final - partidas.Partidas[i].Cash_Inicial) / partidas.Partidas[i].Cash_Inicial *100);
                
            }

            stats.MediaGananciasPartidas = medias / partidas.Partidas.Count;
            
            

            stats.RentabilidadPartidas = rentabilidades / partidas.Partidas.Count;



            return stats;
        }

        //public EstadisticasPartidas GetEstadisticas(int Usuario_id, DateTime fecha)
        //{
        //    EstadisticasPartidas stats = new EstadisticasPartidas();

        //    ConjuntoPartidasUsuario partidas = this.GetPartidas(Usuario_id, fecha);

        //    List<double> rentabilidades = new List<double>();

        //    double medias = 0;
        //    for (int i = 0; i < partidas.Partidas.Count; i++)
        //    {
        //        stats.GananciasPartidasAcumuladas += Math.Round((partidas.Partidas[i].Cash_Final - partidas.Partidas[i].Cash_Inicial), 2);
        //        stats.CashInicialPartidas += Math.Round(partidas.Partidas[i].Cash_Inicial, 2);
        //        stats.CashFinalPartidas += Math.Round(partidas.Partidas[i].Cash_Final, 2);
        //        medias += (partidas.Partidas[i].Cash_Final - partidas.Partidas[i].Cash_Inicial);
        //        rentabilidades.Add(((partidas.Partidas[i].Cash_Final - partidas.Partidas[i].Cash_Inicial) / partidas.Partidas[i].Cash_Inicial) * 100);

        //    }

        //    stats.MediaGananciasPartidas = Math.Round((medias / partidas.Partidas.Count), 2);

        //    for (int i = 0; i < rentabilidades.Count; i++)
        //    {
        //        stats.RentabilidadPartidas += rentabilidades[i];
        //    }
        //    stats.RentabilidadPartidas = Math.Round((stats.MediaGananciasPartidas / rentabilidades.Count), 2);



        //    return stats;
        //}

    }
}

