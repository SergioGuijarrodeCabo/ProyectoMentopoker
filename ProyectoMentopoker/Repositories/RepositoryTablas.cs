using System.Data.SqlClient;
using ProyectoMentopoker.Models;

#region
//CREATE OR ALTER PROCEDURE SP_GET_TABLA
//(@ID INT)
//AS
//SELECT * FROM celdas WHERE table_id=@ID
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

            
               // string connectionString = @"Data Source = DESKTOP - E38C8U3\SQLEXPRESS; Initial Catalog = MENTOPOKER; Integrated Security = True";

            string connectionString = @"Data Source=LOCALHOST\DESARROLLO;Initial Catalog=PROYECTOMENTOPOKER;User ID=sa;Password=MCSD2022";
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
                string cell_id = this.reader["cell_id"].ToString();
                string background_color = this.reader["background_color"].ToString();
                string text_color = this.reader["text_color"].ToString();

                Celda celda = new Celda(identificador, table_id, cell_id, background_color, text_color);
                tabla.Add(celda);
            }

            this.reader.Close();

            this.cn.Close();

            this.com.Parameters.Clear();

            return tabla;
        
        }

    }



    
}