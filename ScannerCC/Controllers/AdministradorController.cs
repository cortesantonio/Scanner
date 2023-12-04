using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public ActionResult Index(string Busqueda, string BusquedaUsuarios)
        {
            DateTime fechaHoy = DateTime.Now;
            DateTime fechaMesAnterior = fechaHoy.AddMonths(-1);
            DateTime fechaHaceUnAnio = fechaHoy.AddYears(-1);
            DateTime fechaHaceDosAnio = fechaHoy.AddYears(-2);


            if (User.Identity.IsAuthenticated)
            {
                var TrabajadorActivo = _context.Usuario.Where(t => t.Rut.Equals(User.Identity.Name)).FirstOrDefault();
                ViewBag.trab = TrabajadorActivo;

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


                ViewBag.produccionUnAnio = _context.Producto
                .Where(e => (e.FechaProduccion ?? DateTime.MinValue) >= fechaHaceUnAnio && (e.FechaProduccion ?? DateTime.MinValue) <= fechaHoy)
                .ToList().Count;
                if (ViewBag.produccionUnAnio == null)
                {
                    ViewBag.produccionUnAnio = 0;
                }

                ViewBag.produccionDosAnio = _context.Producto
                .Where(e => e.FechaProduccion >= fechaHaceDosAnio && e.FechaProduccion <= fechaHaceUnAnio)
                .ToList().Count;
                if (ViewBag.produccionDosAnio == null)
                {
                    ViewBag.produccionDosAnio = 0;
                }

                ViewBag.Usuarios= _context.Usuario.Include(r => r.Rol).ToList();
                ViewBag.Productos = _context.Producto.ToList();

                //Si se realiza busqueda de productos evalua y filtra datos
                if (Busqueda != null)
                {
                    

                    var ProductoResultado = _context.Producto.Where(x => x.Nombre.Contains(Busqueda) || x.CodigoBarra.Contains(Busqueda)).ToList();
                    ViewBag.Productos= ProductoResultado;
                    if (ProductoResultado.Count > 0)
                    {
                        Escaneo E = new Escaneo();
                        E.ProductoId = ProductoResultado.FirstOrDefault().idProducto;
                        E.UsuarioId = TrabajadorActivo.idUsuario;
                        E.Fecha = DateTime.Now;
                        _context.Escaneo.Add(E);
                        _context.SaveChanges();

                    }


                }

                //buscar usuarios
                if (BusquedaUsuarios != null)
                {
                        ViewBag.Usuarios = _context.Usuario.Where(x => x.Rut == BusquedaUsuarios || x.Nombre == BusquedaUsuarios).ToList();
                }
                return View();
            }
            else { return RedirectToAction("Index", "Home"); }
        }

       
    }
}
