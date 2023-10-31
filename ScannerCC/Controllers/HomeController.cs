﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScannerCC.Models;
using System.Diagnostics;


namespace ScannerCC.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var rol = _context.Rol.ToList().Count;
            if (rol == 0) {
                string[] roles = { "Especialista", "Administrador"};
                for (int i = 0; i < roles.Length; i++)
                {
                    Rol R = new Rol();
                    R.Nombre = roles[i];
                    _context.Rol.Add(R);
                    _context.SaveChanges();

                }
            }


            if (User.Identity.IsAuthenticated)
            {
                var trab = _context.Usuario.Include(r => r.Rol).Where(t => t.Rut.Equals(User.Identity.Name)).FirstOrDefault();
                
                if (trab != null && trab.Rol.Nombre == "Especialista")
                {
                    return RedirectToAction("Index", "Especialista");
                }
                if (trab != null && trab.Rol.Nombre == "Administrador")
                {
                    return RedirectToAction("Index", "Administrador");

                }
            }
            else
            {
                return View();

            }

            return View();

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
    }
}