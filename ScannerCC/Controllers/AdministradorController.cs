using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScannerCC.Migrations;
using ScannerCC.Models;
using System.Linq;

namespace ScannerCC.Controllers
{
    public class AdministradorController : Controller
    {

        private readonly AppDbContext _context;

        public AdministradorController(AppDbContext context)
        {
            _context = context;
        }
        // GET: AdministradorController
        public ActionResult Index()
        {
            DateTime fechaHoy = DateTime.Now;
            DateTime fechaMesAnterior = fechaHoy.AddMonths(-1);

            if (User.Identity.IsAuthenticated)
            {
                var trab = _context.Usuario.Where(t => t.Rut.Equals(User.Identity.Name)).FirstOrDefault();

                ViewBag.countUsuariosEspecialista = _context.Usuario
                    .Include(x => x.Rol)
                    .Where(r => r.Rol.Nombre == "Especialista").ToList().Count;
                
                ViewBag.countUsuariosAdministrador = _context.Usuario
                    .Include(x => x.Rol)
                    .Where(r => r.Rol.Nombre == "Administrador").ToList().Count;

                ViewBag.countRose = _context.Producto.Where(r => r.TipoEtiqueta == "Rose").ToList().Count;
                ViewBag.countTinto = _context.Producto.Where(r => r.TipoEtiqueta == "Tinto").ToList().Count;
                ViewBag.countBlanco = _context.Producto.Where(r => r.TipoEtiqueta == "Blanco").ToList().Count;

                ViewBag.paisesConRecuento = _context.Producto
               .GroupBy(p => p.PaisDestino)
               .Select(g => new Paises { PaisDestino = g.Key, Cantidad = g.Count() })
               .ToList();




                ViewBag.countProductos = _context.Producto.ToList().Count;
                ViewBag.countUsuarios = _context.Usuario.ToList().Count;
                ViewBag.countEscaneos = _context.Escaneo.ToList().Count;
                ViewBag.countEscaneosMes = _context.Escaneo
                    .Where(e => e.Fecha >= fechaMesAnterior && e.Fecha <= fechaHoy)
                    .ToList().Count;


                return View(trab);
            }
            else { return RedirectToAction("Index", "Home"); }
        }

       
    }
}
