using System.Data.SqlClient;
namespace ProyectoMentopoker.Repositories
{


    public class RepositoryTablas
    {

        SqlConnection cn;
        SqlCommand com;
        SqlDataReader reader;

        public RepositoryTablas()
        {


            string connectionString = @"Data Source=DESKTOP-E38C8U3\SQLEXPRESS;Initial Catalog=MENTOPOKER;Integrated Security=True";
            // this.cn = new SqlConnection(HelperConfiguartion.GetConnectionString());
            this.cn = new SqlConnection(connectionString);
            this.com = new SqlCommand();
            this.com.Connection = this.cn;
        }




    }
}