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
        public ActionResult Index()
        {
            

            return View(_context.Producto.ToList());
        }

        
            
        
    }
}
