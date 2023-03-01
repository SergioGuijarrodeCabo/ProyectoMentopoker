﻿using ProyectoMentopoker.Models;
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


        public ConjuntoPartidasUsuario GetPartidas(int Usuario_id)
        {
            //string sql = "SP_GET_PARTIDAS";
            //var consulta = this.context.Partidas.FromSqlRaw(sql);
            //List<Partida> partidas = consulta.ToList();

            //for(int i=0;i<partidas.Count;i++)
            //{
            //    partidas[i].Rondas = this.GetRondas(int.Parse(partidas[i].Partida_id));

            //}

            var consulta = from datos in this.context.Partidas
                            where datos.Usuario_id == Usuario_id.ToString()
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

    }
}

