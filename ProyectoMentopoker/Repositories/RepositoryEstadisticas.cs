using ProyectoMentopoker.Models;
using ProyectoMentopoker.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using System.Diagnostics.Metrics;

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
//SELECT Jugada_id, Cantidad_jugada_Preflop, Seguimiento_Tabla, Identificador, Ronda_id  FROM Jugadas WHERE Ronda_id = @RONDA_ID
//GO



//AHORA ES ESTE
//CREATE OR ALTER   PROCEDURE [dbo].[GET_IDSTABLAS_JUGADAS]
//(@Jugada_id INT)
//AS

//SELECT C.Cell_id, C.Table_id, J.Cantidad_jugada_Preflop, T.Condicion, R.Cantidad_jugada, J.Seguimiento_Tabla, J.Ronda_id, J.Jugada_Id, R.Cantidad_jugada,R.Ganancias
//FROM Celdas C
//INNER JOIN Tablas T ON C.Table_id = T.Table_id
//INNER JOIN Jugadas J ON J.Identificador = C.Identificador
//INNER JOIN Rondas R ON R.Ronda_id = J.Ronda_id
//WHERE J.Jugada_id = @Jugada_id
//GO





//CREATE OR ALTER PROCEDURE GET_IDSTABLAS_JUGADAS
//(@Identificador INT)
//AS

//SELECT C.Cell_id, C.Table_id, T.Condicion, J.Cantidad_jugada, J.Seguimiento_Tabla, J.Ronda_id, J.Jugada_Id
//FROM Celdas C
//INNER JOIN Tablas T ON C.Table_id = T.Table_id
//INNER JOIN Jugadas J ON J.Identificador = C.Identificador
//WHERE C.Identificador = @Identificador




//CREATE OR ALTER PROCEDURE GET_IDSTABLAS_JUGADAS
//(
//@Jugada_id NVARCHAR(50))
//AS

//SELECT C.Cell_id, C.Table_id, T.Condicion, J.Cantidad_jugada, J.Seguimiento_Tabla, J.Ronda_id, J.Jugada_Id
//FROM Celdas C
//INNER JOIN Tablas T ON C.Table_id = T.Table_id
//INNER JOIN Jugadas J ON J.Identificador = C.Identificador
//WHERE j.Jugada_id = @Jugada_id





//VFINAL
//ALTER PROCEDURE[dbo].[GET_IDSTABLAS_JUGADAS]
//(@Ronda_id INT, @Cell_id NVARCHAR(4) = NULL)
//AS

//IF @Cell_id IS NULL  BEGIN
//SELECT C.Cell_id, C.Table_id, T.Condicion, J.Cantidad_jugada_Preflop, J.Seguimiento_Tabla, J.Ronda_id, J.Jugada_Id, R.Cantidad_jugada, R.Ganancias
//FROM Celdas C
//INNER JOIN Tablas T ON C.Table_id = T.Table_id
//INNER JOIN Jugadas J ON J.Identificador = C.Identificador
//INNER JOIN Rondas R ON R.Ronda_id = J.Ronda_id
//WHERE R.Ronda_id = @Ronda_id
//END
//ELSE
//SELECT C.Cell_id, C.Table_id, T.Condicion, J.Cantidad_jugada_Preflop, J.Seguimiento_Tabla, J.Ronda_id, J.Jugada_Id, R.Cantidad_jugada, R.Ganancias
//FROM Celdas C
//INNER JOIN Tablas T ON C.Table_id = T.Table_id
//INNER JOIN Jugadas J ON J.Identificador = C.Identificador
//INNER JOIN Rondas R ON R.Ronda_id = J.Ronda_id
//WHERE R.Ronda_id = @Ronda_id AND C.Cell_id = @Cell_id














//create or alter procedure /*SP_GETCELLID*/
//(@identificador int)
//as
//select cell_id from Celdas where celdas.Identificador = @identificador
//go


//create or alter procedure SP_GET_CELLID
//(@identificador int)
//as
//select Cell_id from Celdas where Identificador = @identificador
//go



//create or alter procedure SP_GET_RONDA_IDJUGADA
//  (@Ronda_id NVARCHAR(50))
//  AS
//  SELECT * FROM Rondas where Ronda_id = @Ronda_Id

//  GO

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


        public ConjuntoPartidasUsuario GetPartidas(int Usuario_id, string peticion, DateTime? fechaInicio = null, DateTime? fechaFinal = null, string? cell_id = null)
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





            List<PartidaModel> partidas = new List<PartidaModel>();




            if (peticion == "partidas" || peticion == "jugadasFecha")
            {
                //  var consulta = from datos in this.context.Partidas
                //                where datos.Usuario_id == Usuario_id.ToString() &&
                //                      (!fecha.HasValue || datos.Fecha > fecha.Value)
                //                select datos;
                //partidas = consulta.ToList();


                var consulta = from datos in this.context.Partidas
                               where datos.Usuario_id == Usuario_id.ToString() &&
                                     ((!fechaInicio.HasValue || datos.Fecha >= fechaInicio.Value) &&
                                      (!fechaFinal.HasValue || datos.Fecha <= fechaFinal.Value))
                               select datos;
                partidas = consulta.ToList();

            }

            if (peticion == "jugadasCellid")
            {
                var consulta = from datos in this.context.Partidas
                               select datos;
                partidas = consulta.ToList();
            }
          
            


            ConjuntoPartidasUsuario conjunto = new ConjuntoPartidasUsuario();
           

            List<RondaModel> rondas = new List<RondaModel>();
            List<JugadasCalculadasModel> jugadas = new List<JugadasCalculadasModel>();
            
            for (int i=0;i<partidas.Count;i++)
            {
                rondas.AddRange(this.GetRondas(partidas[i].Partida_id));
               
            }
            for (int i = 0; i < rondas.Count; i++)
            {
                jugadas.AddRange(this.GetJugadas(rondas[i].Ronda_id,cell_id));

            }

            conjunto.Partidas = partidas;
            conjunto.Rondas = rondas;
            conjunto.Jugadas = jugadas;

            //if (peticion == "partidas")
            //{
            //    conjunto.EstadisticasPartidas = this.GetEstadisticasPartidas(conjunto);
            //}
            //if (peticion == "jugadasFecha") 
            //{
            //    conjunto.EstadisticasJugadas = this.GetEstadisticasJugadas(conjunto);

            //}if(peticion == "jugadasCellid")
            //{
            //    conjunto.EstadisticasJugadas = this.GetEstadisticasJugadas(conjunto, cell_id);
            //}
          




            return conjunto;


        }

        public List<RondaModel> GetRondas(string partida_id)
        {
            var consulta = from datos in this.context.Rondas
                           where datos.Partida_id == partida_id
                           select datos;

            
            List<RondaModel> rondas = consulta.ToList();
            return rondas;
        }

        public List<JugadasCalculadasModel> GetJugadas(string ronda_id, string? cell_id = null)
        {
            List<JugadasCalculadasModel> jugadas = new List<JugadasCalculadasModel>();
            if (cell_id == null)
            {


                var parameter = new SqlParameter("@Ronda_id", ronda_id);

                string sql = "EXECUTE GET_IDSTABLAS_JUGADAS @Ronda_id";
                 jugadas = this.context.JugadasCalculadas.FromSqlRaw(sql, parameter).ToList();
            }
            else
            {
                var parameterR = new SqlParameter("@Ronda_id", ronda_id);
                var parameterC = new SqlParameter("@Cell_id", cell_id);
                string sql = "EXECUTE GET_IDSTABLAS_JUGADAS @Ronda_id, @Cell_id";
                jugadas = this.context.JugadasCalculadas.FromSqlRaw(sql, parameterR, parameterC).ToList();


            }
      


            return jugadas;
        }

        public EstadisticasPartidas GetEstadisticasPartidas(int Usuario_id, string peticion, DateTime? fechaInicio = null, DateTime? fechaFinal = null)
        {
            EstadisticasPartidas stats = new EstadisticasPartidas();

   

            stats.partidas = this.GetPartidas(Usuario_id, peticion, fechaInicio, fechaFinal);

            double rentabilidades = 0;

            double medias = 0 ;
            for (int i = 0; i< stats.partidas.Partidas.Count; i++)
            {
                stats.GananciasPartidasAcumuladas += (stats.partidas.Partidas[i].Cash_Final - stats.partidas.Partidas[i].Cash_Inicial);
                stats.CashInicialPartidas += stats.partidas.Partidas[i].Cash_Inicial;
                stats.CashFinalPartidas += stats.partidas.Partidas[i].Cash_Final;
                medias+=(stats.partidas.Partidas[i].Cash_Final - stats.partidas.Partidas[i].Cash_Inicial);
                rentabilidades+=((stats.partidas.Partidas[i].Cash_Final - stats.partidas.Partidas[i].Cash_Inicial) / stats.partidas.Partidas[i].Cash_Inicial *100);
                
            }

            stats.MediaGananciasPartidas = medias / stats.partidas.Partidas.Count;
            
            

            stats.RentabilidadPartidas = rentabilidades / stats.partidas.Partidas.Count;



            return stats;
        }



        public EstadisticasJugadas GetEstadisticasJugadas(int Usuario_id, string peticion, DateTime? fechaInicio = null, DateTime? fechaFinal = null, string? cell_id = null)
        {
            EstadisticasJugadas stats = new EstadisticasJugadas();

       

            stats.partidas = this.GetPartidas(Usuario_id, peticion, fechaInicio, fechaFinal, cell_id);

            //List <JugadasCalculadasModel> jugadas = new List<JugadasCalculadasModel>();

          
            var numRondas = 0;


            //if (cell_id == null)
            //{
            //List<RondaModel> rondasACalcular = stats.partidas.Rondas;


            List<RondaModel> rondasACalcular = new List<RondaModel>();

            List<JugadasCalculadasModel> JugadasRondas = new List<JugadasCalculadasModel>();

            





            for (int i = 0; i < stats.partidas.Rondas.Count; i++)
            {


                string seguimiento = "si";
               var jugadaInsertada = false;
                   for (int x = 0; x <stats.partidas.Jugadas.Count; x++) {
                    if (jugadaInsertada == false)
                    {

                        if (stats.partidas.Rondas[i].Ronda_id == stats.partidas.Jugadas[x].Ronda_id)
                        {
                            
                            

                                if (stats.partidas.Jugadas[x].Seguimiento_Tabla == false)
                                {
                                    seguimiento = "no";

                                    jugadaInsertada = true;
                                    JugadasRondas.Add(stats.partidas.Jugadas[x]);
                                   rondasACalcular.Add(stats.partidas.Rondas[i]);
                                }
                                if (stats.partidas.Jugadas[x].Seguimiento_Tabla == true)
                                {
                                    seguimiento = "si";
                                    jugadaInsertada = true;
                                    JugadasRondas.Add(stats.partidas.Jugadas[x]);
                                   rondasACalcular.Add(stats.partidas.Rondas[i]);

                            }
                                stats.SeguimientoTipoRondas.Add(seguimiento.ToString());
                                stats.Rondas_ids.Add(stats.partidas.Rondas[i].Ronda_id);
                                numRondas++;
                                //if ((partidas.Jugadas[x].Seguimiento_Tabla == true || partidas.Jugadas[x].Seguimiento_Tabla == false) && (contadorFalse ==1 && contadorTrue == 1))
                                //{
                                //    seguimiento = "mixto";
                                //}

                            
                        }
                    }
                    else { break; }
                    
                }
               
               
                
            }

            var rondasSi = 0;
            var rondasNo = 0;


            for (int i = 0; i < numRondas; i++)
            {
                if (stats.SeguimientoTipoRondas[i].Equals("si"))
                {

                    stats.CantidadesJugadas[0] += (rondasACalcular[i].Cantidad_jugada);
                    stats.GananciasTipoRondas[0] += (rondasACalcular[i].Ganancias);
                    stats.CantidadesRondas[0] += (JugadasRondas[i].Cantidad_jugada_Preflop);
                    stats.MediaCantidadesJugadas[0] += (JugadasRondas[i].Cantidad_jugada_Preflop);
                    stats.RentabilidadTipoRondas[0] += ((rondasACalcular[i].Ganancias /*+ rondasACalcular[i].Cantidad_jugada*/) / rondasACalcular[i].Cantidad_jugada)*100;
                    stats.MediaGananciasTipoRondas[0] += (rondasACalcular[i].Ganancias);
                    rondasSi++;
                }
                if (stats.SeguimientoTipoRondas[i].Equals("no"))
                {
                    stats.CantidadesJugadas[1] += (rondasACalcular[i].Cantidad_jugada);
                    stats.GananciasTipoRondas[1] += (rondasACalcular[i].Ganancias);
                    stats.CantidadesRondas[1] += (JugadasRondas[i].Cantidad_jugada_Preflop);
                    stats.MediaCantidadesJugadas[1] += (JugadasRondas[i].Cantidad_jugada_Preflop);
                    stats.RentabilidadTipoRondas[1] += (rondasACalcular[i].Ganancias / rondasACalcular[i].Cantidad_jugada)*100;
                    stats.MediaGananciasTipoRondas[1] += (rondasACalcular[i].Ganancias);
                    rondasNo++;
                }

                // stats.CantidadesJugadasTipoRondas.Add(partidas.Rondas[i].Cantidad_jugada);
                //stats.GananciasTipoRondas.Add(partidas.Rondas[i].Ganancias);
                //stats.RentabilidadTipoRondas.Add((partidas.Rondas[i].Ganancias + partidas.Rondas[i].Cantidad_jugada) / partidas.Rondas[i].Cantidad_jugada);
            }

            stats.MediaCantidadesJugadas[0] = stats.MediaCantidadesJugadas[0] / rondasSi;
            stats.MediaCantidadesJugadas[1] = stats.MediaCantidadesJugadas[1] / rondasNo;


            double rent = stats.RentabilidadTipoRondas[1];

            stats.RentabilidadTipoRondas[0] = stats.RentabilidadTipoRondas[0] / rondasSi;
            stats.RentabilidadTipoRondas[1] = stats.RentabilidadTipoRondas[1] / rondasNo;

            double rentFinal = stats.RentabilidadTipoRondas[1];

            stats.MediaGananciasTipoRondas[0] = stats.MediaGananciasTipoRondas[0] / rondasSi;
            stats.MediaGananciasTipoRondas[1] = stats.MediaGananciasTipoRondas[1] / rondasNo;



            return stats;
        }
    }

     
        
}

