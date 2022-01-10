using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Pizza_App.Data;
using Pizza_App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Pizza_App.Controllers
{
    
    public class AccountController : Controller
    {

        private readonly MyContext _context;

        public AccountController(MyContext contexto)
        {
            _context = contexto;
        }

        public IActionResult Login()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public  IActionResult Login(Account usuario)
        {
            if (ModelState.IsValid)
            {
                usuario usuariologeado;
                usuariologeado=   _context.usuarios.Where(u=> u.nombre_usuario.ToUpper().Trim() == usuario.userlogged.ToUpper().Trim()).FirstOrDefault();
                if (usuariologeado != null)
                {
                    if (usuariologeado.password.ToUpper() == usuario.password.ToUpper().Trim())
                    {
                       
                        var identity = new ClaimsIdentity(new[] {
                        new Claim(ClaimTypes.Name, usuariologeado.nombre_usuario),
                        new Claim(ClaimTypes.Role,usuariologeado.tipo_usuario)
                         }, CookieAuthenticationDefaults.AuthenticationScheme);                        

                        var principal = new ClaimsPrincipal(identity);

                        var login = HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal,
                            new AuthenticationProperties
                            {
                                ExpiresUtc = DateTime.UtcNow.AddMinutes(20),
                                IsPersistent = false,
                                AllowRefresh = false
                            });
                      
                        if(usuariologeado.tipo_usuario=="ADMIN")
                        return RedirectToAction("Index","orden");
                        else
                            return RedirectToAction("PendingOrders", "orden");
                    }
                }

            }
            return View();
        }

        [Authorize]
        public IActionResult Logout()
        {
            //HttpContext.Session.Remove("NIVEL");
            //HttpContext.Session.Remove("USUARIO");
            var login = HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login","Account");
        }

    }
}
