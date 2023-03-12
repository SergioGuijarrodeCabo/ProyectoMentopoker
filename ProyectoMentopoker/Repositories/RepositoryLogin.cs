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

        public List<UsuarioModel> GetUsuarios()
        {
            var consulta = from datos in this.context.Usuarios
                           select datos;
            return consulta.ToList();
        }

        public UsuarioModel FindUsuario(string usuario_id)
        {
            var consulta = from datos in this.context.Usuarios
                           where datos.Usuario_id == usuario_id
                           select datos;
            return consulta.FirstOrDefault();

        }

        public string LastId()
        {
            var consulta = (from datos in this.context.Usuarios
                            orderby datos.Usuario_id descending
                            select datos.Usuario_id).FirstOrDefault();

            int nuevoId = int.Parse(consulta)+1;

            return nuevoId.ToString();
          
            
        }

        public async Task InsertUsuario(string Email, string Pass, string Rol)
        {
            UsuarioModel usuario = new UsuarioModel();
            usuario.Usuario_id = this.LastId();
            usuario.Email = Email;
            usuario.Pass = Pass;
            usuario.Rol = Rol;
            this.context.Usuarios.Add(usuario);
            await this.context.SaveChangesAsync();
        }
        public async Task UpdateUsuario(string Usuario_id, string Email, string Pass, string Rol)
        {
            UsuarioModel usuario = this.FindUsuario(Usuario_id);
            usuario.Email = Email;
            usuario.Pass = Pass;
            usuario.Rol = Rol;
            await this.context.SaveChangesAsync();
        }

        public async Task DeleteUsuario(string Usuario_id)
        {
            UsuarioModel usuario = this.FindUsuario(Usuario_id);
            this.context.Remove(usuario);
            await this.context.SaveChangesAsync();
        }
    }
}
