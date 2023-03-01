using ProyectoMentopoker.Models;
using ProyectoMentopoker.Data;
using Microsoft.EntityFrameworkCore;

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


        public List<Partida> GetPartidas()
        {
            string sql = "SP_GET_PARTIDAS";
            var consulta = this.context.Partidas.FromSqlRaw(sql);
            List<Partida> partidas = consulta.ToList();
            return partidas;

        }

       

    }
}

