using System.Data.SqlClient;
using ProyectoMentopoker.Models;
using static System.Net.Mime.MediaTypeNames;

#region
//CREATE OR ALTER PROCEDURE SP_GET_TABLA
//(@ID INT)
//AS
//SELECT * FROM celdas WHERE table_id=@ID
//GO


//CREATE OR ALTER PROCEDURE SP_INSERT_PARTIDA
//(
//@CASH_INICIAL FLOAT,
//@CASH_FINAL FLOAT
//)
//AS
//INSERT INTO Partidas  (Partida_id, Cash_inicial, Cash_final, Fecha) VALUES((SELECT max(CAST(Partida_id AS INT)) from Partidas) + 1, @CASH_INICIAL, @CASH_FINAL, GETDATE())
//GO

//CREATE OR ALTER PROCEDURE SP_INSERT_RONDA
//(

//@CANTIDAD_RONDA FLOAT,
//@GANANCIAS FLOAT,
//@PARTIDA_ID NVARCHAR(50)
//)
//AS
//INSERT INTO Rondas (Ronda_id, Cantidad_jugada, Ganancias, Partida_id) VALUES((SELECT max(CAST(Ronda_id AS INT)) from Rondas) + 1, @CANTIDAD_RONDA, @GANANCIAS, @PARTIDA_ID)
//GO

//CREATE OR ALTER PROCEDURE SP_INSERT_JUGADA
//(
//@JUGADA_ID NVARCHAR(50),
//@CANTIDAD_JUGADA FLOAT,
//@SEGUIMIENTO_TABLA BIT,
//@IDENTIFICADOR INT,
//@RONDA_ID NVARCHAR(50)
//)
//AS
//INSERT INTO Jugadas (Jugada_id, Cantidad_jugada, Seguimiento_Tabla, Identificador, Ronda_id) VALUES((SELECT max(CAST(Jugada_id AS INT)) from Jugadas) + 1, @CANTIDAD_JUGADA, @SEGUIMIENTO_TABLA, @IDENTIFICADOR, @RONDA_ID)
//GO



#endregion


namespace ProyectoMentopoker.Repositories
{


    public class RepositoryTablas
    {

        SqlConnection cn;
        SqlCommand com;
        SqlDataReader reader;

        public RepositoryTablas()
        {


            string connectionString = @"Data Source=DESKTOP-E38C8U3;Initial Catalog=PROYECTOMENTOPOKER;User ID=sa;Password=MCSD2022";
            // string connectionString = @"Data Source = DESKTOP - E38C8U3\SQLEXPRESS; Initial Catalog = MENTOPOKER; Integrated Security = True";
            //string connectionString = @"Data Source = LOCALHOST\DESARROLLO; Initial Catalog = PROYECTOMENTOPOKER; User ID = sa; Password = MCSD2022";
            // string connectionString = @"Data Source=LOCALHOST\DESARROLLO;Initial Catalog=PROYECTOMENTOPOKER;User ID=sa;Password=MCSD2022";
            // this.cn = new SqlConnection(HelperConfiguartion.GetConnectionString());
            this.cn = new SqlConnection(connectionString);
            this.com = new SqlCommand();
            this.com.Connection = this.cn;
        }


        public List<Celda> GetTabla(int id)
        {
            List<Celda> tabla = new List<Celda>();
            string sql = "select * from Celdas where table_id=@ID";

            SqlParameter pamid = new SqlParameter("@ID", id);

            this.com.Parameters.Add(pamid);

            this.com.CommandType = System.Data.CommandType.Text;

            this.com.CommandText = sql;

            this.cn.Open();

            this.reader = this.com.ExecuteReader();


            while (this.reader.Read())
            {

                int identificador = int.Parse(this.reader["Identificador"].ToString());
                int table_id = int.Parse(this.reader["table_id"].ToString());
                string id_celda = this.reader["cell_id"].ToString();
                //  string id_celda = this.reader["Id_celda"].ToString();
                string background_color = this.reader["background_color"].ToString();
                string text_color = this.reader["text_color"].ToString();

                Celda celda = new Celda(identificador, table_id, id_celda, background_color, text_color);
                tabla.Add(celda);
            }

            this.reader.Close();

            this.cn.Close();

            this.com.Parameters.Clear();

            return tabla;

        }



        //public Boolean insertPartida(int[] ids_Jugadas, int[] ids_Rondas, double[] ganancias_Rondas,
        //    string[] cell_ids_Jugadas, int[] table_ids_Jugadas, double[] cantidades_Jugadas,
        //    Boolean[] seguimiento_jugadas, double dinero_inicial, double dinero_actual)
        //{

        //}

        public void insertRonda(double cantidades_Rondas, double ganancias_Ronda, string partida_id)
        {

            SqlParameter pamcantidad = new SqlParameter("@CANTIDAD_RONDA", cantidades_Rondas);
            this.com.Parameters.Add(pamcantidad);
            SqlParameter pamganancias = new SqlParameter("@GANANCIAS", ganancias_Ronda);
            this.com.Parameters.Add(pamganancias);
            SqlParameter pampartidaid = new SqlParameter("@PARTIDA_ID", partida_id);
            this.com.Parameters.Add(pampartidaid);
            this.com.CommandType = System.Data.CommandType.StoredProcedure;
            this.com.CommandText = "SP_INSERT_RONDA";

            this.cn.Open();
            this.com.ExecuteNonQuery();
            this.com.Parameters.Clear();
            this.cn.Close();
        }


        public Boolean insertPartida(int[] ids_Jugadas, int[] ids_Rondas, double[] ganancias_Rondas, double[] cantidades_Rondas,
            string[] cell_ids_Jugadas, int[] table_ids_Jugadas, double[] cantidades_Jugadas,
            Boolean[] seguimiento_jugadas, double dinero_inicial, double dinero_actual)
        {
            int exito = 0;
            Boolean exitob = false;


            SqlParameter pamcashinicial = new SqlParameter("@CASH_INICIAL", dinero_inicial);
            this.com.Parameters.Add(pamcashinicial);
            SqlParameter pamcashfinal = new SqlParameter("@CASH_FINAL", dinero_actual);
            this.com.Parameters.Add(pamcashfinal);
            this.com.CommandType = System.Data.CommandType.StoredProcedure;
            this.com.CommandText = "SP_INSERT_PARTIDA";

            this.cn.Open();
            exito = this.com.ExecuteNonQuery();
            this.com.Parameters.Clear();
            this.cn.Close();

            if (exito == 1)
            {
                exitob = true;
            }


            this.com.CommandText = "SELECT MAX(CAST(Partida_id AS INT)) from Partidas";
            this.com.CommandType = System.Data.CommandType.Text;
            this.cn.Open();
            string partidaId = this.com.ExecuteScalar().ToString();
           
  
            this.cn.Close();

            for (int i=0; i < cantidades_Rondas.Length; i++)
                  {
                this.insertRonda(cantidades_Rondas[i], ganancias_Rondas[i], partidaId);
                  }



            return exitob;
        }



    }


}