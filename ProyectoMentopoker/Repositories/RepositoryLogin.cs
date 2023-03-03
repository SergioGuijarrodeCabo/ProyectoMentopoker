using ProyectoMentopoker.Models;
using ProyectoMentopoker.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;




namespace ProyectoMentopoker.Repositories
{
    public class RepositoryLogin
    {
        private MentopokerContext context;


        public RepositoryLogin(MentopokerContext context)
        {
            this.context = context;
        }


        public UsuarioModel Login(string email, string password)
        {
            //UsuarioModel usuario = new UsuarioModel();
            var consulta = from datos in this.context.Usuarios
                           where datos.Email == email.ToString() && datos.Pass == password.ToString()
                           select datos;
            //usuario =  consulta.First();

            return consulta.FirstOrDefault();

        }
    }
}
