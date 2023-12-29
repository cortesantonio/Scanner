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
        public ActionResult Index(string Busqueda, string BusquedaUsuarios, int paginaEscaneos = 1, int paginaControles = 1)
        {
            DateTime fechaHoy = DateTime.Now;
            DateTime fechaMesAnterior = fechaHoy.AddMonths(-1);
            DateTime fechaHaceUnAnio = fechaHoy.AddYears(-1);
            DateTime fechaHaceDosAnio = fechaHoy.AddYears(-2);


            if (User.Identity.IsAuthenticated)
            {
                // OBTENER INFORMACIOON DE USUARIO ACTIVO EN BASE DE DATOS.
                var TrabajadorActivo = _context.Usuario.Where(t => t.Rut.Equals(User.Identity.Name)).FirstOrDefault();
                ViewBag.trab = TrabajadorActivo;

                //Consulta de usuarios en base de datos de usuarios con rol'Especialista'.
                var Especialistas = _context.Usuario
                    .Include(x => x.Rol)
                    .Where(r => r.Rol.Nombre == "Especialista").ToList();
                
                ViewBag.countUsuariosEspecialista = Especialistas.Count;
                

                //Consulta de usuarios en base de datos de usuarios con rol'Administrador'.

                var Administradores = _context.Usuario
                    .Include(x => x.Rol)
                    .Where(r => r.Rol.Nombre == "Administrador").ToList();

                ViewBag.countUsuariosAdministrador = Administradores.Count;

                //DATOS PARA GRAFICO DE TORTA DE TIPOS DE ETIQUETAS
                var counts = _context.Producto
                    .GroupBy(r => r.TipoEtiqueta)
                    .Select(g => new { Etiqueta = g.Key, Count = g.Count() })
                    .ToList();

                ViewBag.countRose = counts.FirstOrDefault(c => c.Etiqueta == "Rose")?.Count ?? 0;
                ViewBag.countTinto = counts.FirstOrDefault(c => c.Etiqueta == "Tinto")?.Count ?? 0;
                ViewBag.countBlanco = counts.FirstOrDefault(c => c.Etiqueta == "Blanco")?.Count ?? 0;


                // DATOS PARA GRAFICO PAISES - PRODUCTOS 
                ViewBag.paisesConRecuento = _context.Producto
               .GroupBy(p => p.PaisDestino)
               .Select(g => new Paises { PaisDestino = g.Key, Cantidad = g.Count() })
               .ToList();

                // COUNT REGISTROS 
                ViewBag.countProductos = _context.Producto.ToList().Count;
                ViewBag.countUsuarios = _context.Usuario.ToList().Count;
                ViewBag.countEscaneos = _context.Escaneo.ToList().Count;
                    //ESCANEOS DEL MES ACTUAL
                ViewBag.countEscaneosMes = _context.Escaneo
                    .Where(e => e.Fecha >= fechaMesAnterior && e.Fecha <= fechaHoy)
                    .ToList().Count;
                    // ESCANEO DE HACE UN ANIO
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


                //Datos para la tablaEscaneos

                const int registrosPorPagina = 2;

                var escaneos = _context.Escaneo
                    .Include(e => e.Producto)
                    .Include(e => e.Usuario)
                    .OrderByDescending(e => e.Fecha)
                    .Skip((paginaEscaneos - 1) * registrosPorPagina)
                    .Take(registrosPorPagina)
                    .ToList();

                ViewBag.Escaneos = escaneos;
                ViewBag.PaginaActualEscaneos = paginaEscaneos;

                //Datos adm. controles

                var controles = _context.Controles
                .Include(c => c.Producto)
                .ToList();

                ViewBag.Controles = controles;


                //Datos para la tablaControl y sus count

                int totalControles = _context.Controles.Count();

                var controless = _context.Controles
                    .Include(c => c.Producto)
                    .Skip((paginaControles - 1) * registrosPorPagina)
                    .Take(registrosPorPagina)
                    .ToList();

                ViewBag.Controless = controless;
                ViewBag.PaginaActualControles = paginaControles;
                ViewBag.TotalControles = totalControles;

                int rechazos = _context.Controles.Count(c => c.Tipodecontrol == true);
                int preventivos = _context.Controles.Count(c => c.Tipodecontrol == false);

                ViewBag.Rechazos = rechazos;
                ViewBag.Preventivos = preventivos;


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
                        E.Fecha = fechaHoy;
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
