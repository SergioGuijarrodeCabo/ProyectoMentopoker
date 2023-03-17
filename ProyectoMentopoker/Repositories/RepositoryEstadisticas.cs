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

            if (peticion == "partidas")
            {
                conjunto.EstadisticasPartidas = this.GetEstadisticasPartidas(conjunto);
            }
            if (peticion == "jugadasFecha") 
            {
                conjunto.EstadisticasJugadas = this.GetEstadisticasJugadas(conjunto);

            }if(peticion == "jugadasCellid")
            {
                conjunto.EstadisticasJugadas = this.GetEstadisticasJugadas(conjunto, cell_id);
            }
          




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

        public List<JugadaModel> GetJugadas(string ronda_id)
        {
            var consulta = from datos in this.context.Jugadas
                           where datos.Ronda_id == ronda_id.ToString()
                           select datos;
            List<JugadaModel> jugadas = consulta.ToList();
   
            return jugadas;
        }

        public EstadisticasPartidas GetEstadisticasPartidas(ConjuntoPartidasUsuario partidas)
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



        public EstadisticasJugadas GetEstadisticasJugadas(ConjuntoPartidasUsuario partidas, string? cell_id = null)
        {
            EstadisticasJugadas stats = new EstadisticasJugadas();


           
            
            List <JugadasCalculadasModel> jugadas = new List<JugadasCalculadasModel>();

            var filtroJugada = false;
            var numRondas = 0;
            List<string> cellids = new List<string>();
            List<RondaModel> rondasACalcular = new List<RondaModel>();
            List<JugadaModel> JugadasRondas = new List<JugadaModel>();

            if (cell_id != null)
            {
                filtroJugada = true;

                //Conexión de casa
                //string connectionString = @"Data Source=DESKTOP-E38C8U3;Initial Catalog=PROYECTOMENTOPOKER;User ID=sa;Password=MCSD2022";

                //Conexión de clase
                string connectionString = @"Data Source=LOCALHOST\DESARROLLO;Initial Catalog=PROYECTOMENTOPOKER;User ID=sa;Password=MCSD2022";


                var cn = new SqlConnection(connectionString);
                var com = new SqlCommand();
                com.Connection = cn;



                for (int x = 0; x < partidas.Jugadas.Count; x++)
                {
                    SqlParameter pamidentificador = new SqlParameter("@Identificador", partidas.Jugadas[x].Identificador);
                    com.Parameters.Add(pamidentificador);

                    com.CommandType = System.Data.CommandType.StoredProcedure;
                    com.CommandText = "SP_GET_CELLID";
                    cn.Open();
                    string cellid = com.ExecuteScalar().ToString();
                    com.Parameters.Clear();
                    cn.Close();
                    cellids.Add(cellid);
                    if(cellid == cell_id)
                    {
                        var parameter = new SqlParameter("@Jugada_id", partidas.Jugadas[x].Jugada_id);

                        string sql = "EXECUTE GET_IDSTABLAS_JUGADAS @Jugada_id";
                        JugadasCalculadasModel jugada = this.context.JugadasCalculadas.FromSqlRaw(sql, parameter).AsEnumerable().FirstOrDefault();
                        jugadas.Add(jugada);

                        var parameter2 = new SqlParameter("@Ronda_id", partidas.Jugadas[x].Ronda_id);
                        string sql2 = "EXECUTE SP_GET_RONDA_IDJUGADA @Ronda_id";
                        RondaModel ronda = this.context.Rondas.FromSqlRaw(sql2, parameter2).AsEnumerable().FirstOrDefault();
                        rondasACalcular.Add(ronda);
                       
                    }

                }
                stats.Jugadas = jugadas;

            }
            else
            {


                for (int i = 0; i < partidas.Jugadas.Count; i++)
                {
                    var parameter = new SqlParameter("@Jugada_id", partidas.Jugadas[i].Jugada_id);

                    string sql = "EXECUTE GET_IDSTABLAS_JUGADAS @Jugada_id";
                    JugadasCalculadasModel jugada = this.context.JugadasCalculadas.FromSqlRaw(sql, parameter).AsEnumerable().FirstOrDefault();

                    jugadas.Add(jugada);

                }
                stats.Jugadas = jugadas;
                rondasACalcular = partidas.Rondas;
            }


            for (int i = 0; i < partidas.Rondas.Count; i++)
            {


                string seguimiento = "si";
               var jugadaInsertada = false;
                   for (int x = 0; x < partidas.Jugadas.Count; x++) {
                    if (jugadaInsertada == false)
                    {

                        if (partidas.Rondas[i].Ronda_id == partidas.Jugadas[x].Ronda_id)
                        {
                            if (filtroJugada == true)
                            {

                                if (cellids[x] == cell_id)
                                {
                                    if (partidas.Jugadas[x].Seguimiento_Tabla == false)
                                    {
                                        seguimiento = "no";

                                        jugadaInsertada = true;
                                        JugadasRondas.Add(partidas.Jugadas[x]);
                                    }
                                    if (partidas.Jugadas[x].Seguimiento_Tabla == true)
                                    {
                                        seguimiento = "si";
                                        jugadaInsertada = true;
                                        JugadasRondas.Add(partidas.Jugadas[x]);
                                    }
                                    stats.SeguimientoTipoRondas.Add(seguimiento.ToString());
                                    stats.Rondas_ids.Add(partidas.Rondas[i].Ronda_id);
                                    numRondas++;

                                }
                            }
                            else
                            {

                                if (partidas.Jugadas[x].Seguimiento_Tabla == false)
                                {
                                    seguimiento = "no";

                                    jugadaInsertada = true;
                                    JugadasRondas.Add(partidas.Jugadas[x]);
                                }
                                if (partidas.Jugadas[x].Seguimiento_Tabla == true)
                                {
                                    seguimiento = "si";
                                    jugadaInsertada = true;
                                    JugadasRondas.Add(partidas.Jugadas[x]);

                                }
                                stats.SeguimientoTipoRondas.Add(seguimiento.ToString());
                                stats.Rondas_ids.Add(partidas.Rondas[i].Ronda_id);
                                numRondas++;
                                //if ((partidas.Jugadas[x].Seguimiento_Tabla == true || partidas.Jugadas[x].Seguimiento_Tabla == false) && (contadorFalse ==1 && contadorTrue == 1))
                                //{
                                //    seguimiento = "mixto";
                                //}

                            }
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
                    stats.RentabilidadTipoRondas[0] += ((rondasACalcular[i].Ganancias + rondasACalcular[i].Cantidad_jugada) / rondasACalcular[i].Cantidad_jugada);
                    stats.MediaGananciasTipoRondas[0] += (rondasACalcular[i].Ganancias);
                    rondasSi++;
                }
                if (stats.SeguimientoTipoRondas[i].Equals("no"))
                {
                    stats.CantidadesJugadas[1] += (rondasACalcular[i].Cantidad_jugada);
                    stats.GananciasTipoRondas[1] += (rondasACalcular[i].Ganancias);
                    stats.CantidadesRondas[1] += (JugadasRondas[i].Cantidad_jugada_Preflop);
                    stats.MediaCantidadesJugadas[1] += (JugadasRondas[i].Cantidad_jugada_Preflop);
                    stats.RentabilidadTipoRondas[1] += ((rondasACalcular[i].Ganancias + rondasACalcular[i].Cantidad_jugada) / rondasACalcular[i].Cantidad_jugada);
                    stats.MediaGananciasTipoRondas[1] += (rondasACalcular[i].Ganancias);
                    rondasNo++;
                }

                // stats.CantidadesJugadasTipoRondas.Add(partidas.Rondas[i].Cantidad_jugada);
                //stats.GananciasTipoRondas.Add(partidas.Rondas[i].Ganancias);
                //stats.RentabilidadTipoRondas.Add((partidas.Rondas[i].Ganancias + partidas.Rondas[i].Cantidad_jugada) / partidas.Rondas[i].Cantidad_jugada);
            }

            stats.MediaCantidadesJugadas[0] = stats.MediaCantidadesJugadas[0] / rondasSi;
            stats.MediaCantidadesJugadas[1] = stats.MediaCantidadesJugadas[1] / rondasNo;

            stats.RentabilidadTipoRondas[0] = stats.RentabilidadTipoRondas[0] / rondasSi;
            stats.RentabilidadTipoRondas[1] = stats.RentabilidadTipoRondas[1] / rondasSi;


            stats.MediaGananciasTipoRondas[0] = stats.MediaGananciasTipoRondas[0] / rondasSi;
            stats.MediaGananciasTipoRondas[1] = stats.MediaGananciasTipoRondas[1] / rondasNo;



            return stats;
        }
    }

     
        
}

