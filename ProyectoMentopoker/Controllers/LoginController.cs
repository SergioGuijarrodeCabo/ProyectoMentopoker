using Microsoft.AspNetCore.Mvc;
using ProyectoMentopoker.Models;
using ProyectoMentopoker.Repositories;
using System.Diagnostics;

namespace ProyectoMentopoker.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILogger<LoginController> _logger;
        private RepositoryLogin repoLogin;

        public LoginController(ILogger<LoginController> logger, RepositoryLogin repoLogin)
        {           
            this.repoLogin = repoLogin;
            _logger = logger;
            
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AccesoDenegado()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Index(string email, string password)
        {
            
            
            
            UsuarioModel usuario = this.repoLogin.Login(email, password);
            
            if(usuario != null)
            {
               
                HttpContext.Session.SetString("EMAIL", usuario.Email);
                HttpContext.Session.SetString("ID", usuario.Usuario_id);
                HttpContext.Session.SetString("ROL", usuario.Rol);
                return RedirectToAction("Index", "Tablas");
            }
            else
            {
                ViewData["MENSAJE"] = "Usuario/password incorrectos";
                return View();
            }


            //if (usuario.ToLower() == "admin" && password.ToLower() == "admin")
            //{
            //    //ALMACENAMOS EL USUARIO EN SESSION
            //    HttpContext.Session.SetString("USUARIO", usuario);

            //    return RedirectToAction("Index", "Tablas");
            //}
            //else
            //{
            //    ViewData["MENSAJE"] = "Usuario/password incorrectos";
            //    return View();
            //}
            return View();
        }
    }
}