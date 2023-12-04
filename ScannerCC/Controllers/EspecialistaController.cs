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
                var TrabajadorActivo = _context.Usuario.Where(t => t.Rut.Equals(User.Identity.Name)).FirstOrDefault();
                ViewBag.trab = TrabajadorActivo;

                ViewBag.Usuarios = _context.Usuario.Include(r => r.Rol).ToList();
                ViewBag.Productos = _context.Producto.ToList();



                //Si se realiza busqueda de productos evalua y filtra datos
                if (Busqueda != null)
                {


                    var ProductoResultado = _context.Producto.Where(x => x.Nombre.Contains(Busqueda) || x.CodigoBarra.Contains(Busqueda)).ToList();
                    ViewBag.Productos = ProductoResultado;

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



                return View();
            }
            else { return RedirectToAction("Index", "Home"); }
        }




    }
}
