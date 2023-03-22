using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using ProyectoMentopoker.Models;
using ProyectoMentopoker.Repositories;
using System.Security.Claims;

namespace ProyectoMentopoker.Controllers
{
    public class ManagedController : Controller
    {
        private RepositoryLogin repoLogin;

        public ManagedController(RepositoryLogin repo)
        {
            this.repoLogin = repo;
        }

        public IActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LogIn(string email
            , string password)
        {
            UsuarioModel usuario = this.repoLogin.Login(email, password);

            if (usuario != null)
            {
                  HttpContext.Session.SetString("EMAIL", usuario.Email);
                HttpContext.Session.SetString("ID", usuario.Usuario_id);
                HttpContext.Session.SetString("ROL", usuario.Rol);


                ClaimsIdentity identity =
               new ClaimsIdentity
               (CookieAuthenticationDefaults.AuthenticationScheme
               , ClaimTypes.Name, ClaimTypes.Role);
                identity.AddClaim
                    (new Claim(ClaimTypes.Email, usuario.Email));
                identity.AddClaim
                    (new Claim(ClaimTypes.Role, usuario.Rol));
             

                if (int.Parse(usuario.Usuario_id) == 1)
                {
                    identity.AddClaim(new Claim("Administrador", "Soy el admin"));

                }
              ;


                ClaimsPrincipal user = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync
                    (CookieAuthenticationDefaults.AuthenticationScheme
                    , user);

                string controller = TempData["controller"].ToString();
                string action = TempData["action"].ToString();
                string id = TempData["id"].ToString();
                return RedirectToAction(action, controller, new { id = id });
                //return RedirectToAction("PerfilDoctor", "Doctores");
                //return RedirectToAction("DeleteEnfermo", "Doctores", new {id =64823});
                //return RedirectToAction("PerfilDoctor", "Doctores", new { id = 64823 });
            }
            else
            {
                ViewData["MENSAJE"] = "Usuario/Password incorrectos";
                return View();
            }
        }

        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync
                (CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Managed");
        }

        public IActionResult ErrorAcceso()
        {

            return View();
        }
    }
}
