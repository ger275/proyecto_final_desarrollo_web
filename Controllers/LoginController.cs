using Microsoft.AspNetCore.Mvc;
using ProyectoFinalDesarrolloWeb.Data;
using ProyectoFinalDesarrolloWeb.Models;

using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using ProyectoFinalDesarrolloWeb.Datos;

namespace ProyectoFinalDesarrolloWeb.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginModel modelo)
        {
            Login d = new Login();

            var usuario = d.ValidarUsuario(modelo.usuario, modelo.pass);

            if (usuario != null)
            {
                var claims = new List<Claim> { 
                    new Claim(ClaimTypes.Name, usuario.usuario),
                    new Claim("Pass", modelo.pass)
                };

                foreach (string roll in usuario.roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, roll));
                }

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }

        public async Task<IActionResult> Salir()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "Login");
        }
    }
}
