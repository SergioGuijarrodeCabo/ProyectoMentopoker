using Microsoft.AspNetCore.Mvc;
using ProyectoMentopoker.Repositories;
using ProyectoMentopoker.Models;
using ProyectoMentopoker.Filters;

namespace ProyectoMentopoker.Controllers
{
    public class UsuariosController : Controller
    {

        private RepositoryLogin repoLogin;

        public UsuariosController(RepositoryLogin repoLogin)
        {
            this.repoLogin = repoLogin;
           

        }




        [AuthorizeUsers(Policy = "AdminOnly")]
        public IActionResult Crud()
        {
            List<UsuarioModel> usuarios = this.repoLogin.GetUsuarios();
            return View(usuarios);
        }


        [AuthorizeUsers(Policy = "AdminOnly")]
        public IActionResult Insert()
        {     
            return View();

        }


        [AuthorizeUsers(Policy = "AdminOnly")]
        [HttpPost]
        public async Task<IActionResult> Insert(UsuarioModel usuario)
        {
            await this.repoLogin.InsertUsuario(usuario.Email, usuario.Pass, usuario.Rol);

            return RedirectToAction("Crud");

        }


        [AuthorizeUsers(Policy = "AdminOnly")]
        public IActionResult Update(string Usuario_id)
        {
            UsuarioModel usuario = this.repoLogin.FindUsuario(Usuario_id);
            return View(usuario);

        }

        [AuthorizeUsers(Policy = "AdminOnly")]
        [HttpPost]
        public async Task<IActionResult> Update(UsuarioModel usuario)
        {
            await this.repoLogin.UpdateUsuario(usuario.Usuario_id, usuario.Email, usuario.Pass, usuario.Rol);

            return RedirectToAction("Crud");

        }

        [AuthorizeUsers(Policy = "AdminOnly")]
        public async Task<IActionResult> Delete(string Usuario_id)
        {
            await this.repoLogin.DeleteUsuario(Usuario_id);
            return RedirectToAction("Crud");

        }


    }
}
