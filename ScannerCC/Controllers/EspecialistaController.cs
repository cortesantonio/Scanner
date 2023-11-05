using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScannerCC.Models;

namespace ScannerCC.Controllers
{
    public class EspecialistaController : Controller
    {
        private readonly AppDbContext _context;

        public EspecialistaController(AppDbContext context)
        {
            _context = context;
        }

        // GET: EspecialistaController
        public ActionResult Index(string Busqueda)
        {
            DateTime fechaHoy = DateTime.Now;


            if (User.Identity.IsAuthenticated)
            {
                ViewBag.trab = _context.Usuario.Where(t => t.Rut.Equals(User.Identity.Name)).FirstOrDefault();

               
                ViewBag.Usuarios = _context.Usuario.Include(r => r.Rol).ToList();
                ViewBag.Productos = _context.Producto.ToList();

                //Si se realiza busqueda de productos evalua y filtra datos
                if (Busqueda != null)
                {
                    var esNumero = int.TryParse(Busqueda, out int parsedInt);
                    if (esNumero)
                    {
                        // Si es una entero, realiza la búsqueda por Codigo.

                        ViewBag.Productos = _context.Producto.Where(x => x.CodigoBarra == parsedInt).ToList();


                    }
                    else
                    {
                        // Si es una cadena, realiza la búsqueda por Nombre.
                        ViewBag.Productos = _context.Producto.Where(x => x.Nombre.Contains(Busqueda)).ToList();

                    }

                }


                return View();
            }
            else { return RedirectToAction("Index", "Home"); }
        }




    }
}
