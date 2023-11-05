using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Security.Cryptography;

using ScannerCC.Models;


namespace ScannerCC.Controllers
{
    public class AuthController : Controller
    {
        private readonly AppDbContext _context;

        public AuthController(AppDbContext context)
        {
            _context = context;
        }


        [HttpPost]
        public async Task<IActionResult> Login(string user, string password)
        {

            //Trabajador
            var us = _context.Usuario.Include(r => r.Rol).Where(u => u.Email.Equals(user) || u.Rut.Equals(user)).FirstOrDefault();
            if (us != null)
            {
                //Usuario Encontrado
                if (VerificarPass(password, us.PasswordHash, us.PasswordSalt))
                {


                    if (us.Rol.Nombre == "Administrador") {
                            var Claims = new List<Claim>
                            {
                            new Claim(ClaimTypes.Name, us.Rut),
                            new Claim(ClaimTypes.NameIdentifier, us.Rut),
                            new Claim(ClaimTypes.Role, "Administrador")
                            };
                            var identity = new ClaimsIdentity(Claims,
                            CookieAuthenticationDefaults.AuthenticationScheme);

                            var principal = new ClaimsPrincipal(identity);

                            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal,
                                new AuthenticationProperties { IsPersistent = true }
                                );

                            return RedirectToAction("Index", "Administrador");
                    }
                    else
                    {
                            var Claims = new List<Claim>
                            {
                            new Claim(ClaimTypes.Name, us.Rut),
                            new Claim(ClaimTypes.NameIdentifier, us.Rut),
                            new Claim(ClaimTypes.Role, "Especialista")
                            }; 
                            var identity = new ClaimsIdentity(Claims,
                                CookieAuthenticationDefaults.AuthenticationScheme);

                            var principal = new ClaimsPrincipal(identity);

                            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal,
                                new AuthenticationProperties { IsPersistent = true }
                                );

                            return RedirectToAction("Index", "Especialista");
                    }

  
                }
                else
                {
                    //Usuario correcto pero contraseña mala
                    ModelState.AddModelError("", "Contraseña Incorrecta");
                    return RedirectToAction("Index", "Home");
                }


            }
            else
            {
                //Usuario No Existe
                ModelState.AddModelError("", "Usuario no Encontrado!");
                return RedirectToAction("Index", "Home");

            }



        }
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }








        [HttpPost]
        public IActionResult Create(string Email, string Rut, string Nombre, string Password, string Rol)
        {
            //Especialista
            var us = _context.Usuario.Where(u => u.Rut.Equals(Rut)).FirstOrDefault();
            if (us != null)
            {
                //el usuario ya esta registrado con el corre ingresado
                ModelState.AddModelError("", "RUT Ya Registrado!");
                return View();
            }
            else
            {
                var id_rol_ingresado = _context.Rol.Where(u => u.Nombre.Contains(Rol)).FirstOrDefault().idRol;

                Usuario U = new Usuario();
                U.Nombre = Nombre;
                U.Email = Email;
                U.Rut = Rut;
                U.RolId = id_rol_ingresado;

                CreatePasswordHash(Password, out byte[] passwordHash, out byte[] passwordSalt);

                U.PasswordHash = passwordHash;
                U.PasswordSalt = passwordSalt;
                _context.Usuario.Add(U);
                _context.SaveChanges();
                return RedirectToAction("Index", "Usuario");
            }

        }
        [HttpPost]
        public IActionResult Edit(string Email, string Rut, string Nombre, string Password, string Rol)
        {

            var U = _context.Usuario.Include(r => r.Rol).FirstOrDefault(u => u.Rut.Equals(Rut));
            if (U != null) {
                if(U.Rol.Nombre == Rol)
                {
                    U.Nombre = Nombre;
                    U.Email = Email;
                    U.Rut = Rut;
                }
                else
                {
                    var id_rol = _context.Rol.FirstOrDefault( i => i.Nombre.Equals(Rol)).idRol;

                    U.Nombre = Nombre;
                    U.Email = Email;
                    U.Rut = Rut;
                    U.RolId = id_rol;
                }

                CreatePasswordHash(Password, out byte[] passwordHash, out byte[] passwordSalt);

                U.PasswordHash = passwordHash;
                U.PasswordSalt = passwordSalt;
                _context.Usuario.Update(U);
                _context.SaveChanges();
                return RedirectToAction("Index", "Usuario");
            }
            else
            {
                return RedirectToAction("Index", "Usuario");
            }


        }

        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public bool VerificarPass(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var pass = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return pass.SequenceEqual(passwordHash);
            }
        }

    }
}
