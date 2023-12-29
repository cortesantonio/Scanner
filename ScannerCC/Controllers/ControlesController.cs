using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ScannerCC.Models;

namespace ScannerCC.Controllers
{
    public class ControlesController : Controller
    {
        private readonly AppDbContext _context;

        public ControlesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Controles
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Controles.Include(c => c.Producto);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Controles/Details/5
        public async Task<IActionResult> Details2(int? id)
        {
            if (id == null || _context.Controles == null)
            {
                return NotFound();
            }

            var controles = await _context.Controles
                .Include(c => c.Producto)
                .FirstOrDefaultAsync(m => m.idControl == id);
            if (controles == null)
            {
                return NotFound();
            }

            return View(controles);
        }

        // GET: Controles/Create
        public IActionResult CreateControl()
        {
            ViewData["ProductoId"] = new SelectList(_context.Producto, "idProducto", "idProducto");
            return View();
        }

        // POST: Controles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateControl([Bind("idControl,ProductoId,Linea,Cepa,PaisOrigen,PaisDestino,Comentario,Tipodecontrol")] Controles controles)
        {
           
                _context.Add(controles);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index","Home");
            
            
        }

        // GET: Controles/Edit/5
        public async Task<IActionResult> Edit2(int? id)
        {
            if (id == null || _context.Controles == null)
            {
                return NotFound();
            }

            var controles = await _context.Controles.FindAsync(id);
            if (controles == null)
            {
                return NotFound();
            }
            ViewData["ProductoId"] = new SelectList(_context.Producto, "idProducto", "idProducto", controles.ProductoId);
            return View(controles);
        }

        // POST: Controles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit2(int id, [Bind("idControl,ProductoId,Linea,Cepa,PaisOrigen,PaisDestino,Comentario,Tipodecontrol")] Controles controles)
        {
            if (id != controles.idControl)
            {
                return NotFound();
            }

            
                try
                {
                    _context.Update(controles);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ControlesExists(controles.idControl))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index","Home");
            
            
        }

        // GET: Controles/Delete/5
        public async Task<IActionResult> Delete2(int? id)
        {
            if (id == null || _context.Controles == null)
            {
                return NotFound();
            }

            var controles = await _context.Controles
                .Include(c => c.Producto)
                .FirstOrDefaultAsync(m => m.idControl == id);
            if (controles == null)
            {
                return NotFound();
            }

            return View(controles);
        }

        // POST: Controles/Delete/5
        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        { 
            var control =  _context.Controles.Where(x=>x.idControl == id).FirstOrDefault();
            _context.Controles.Remove(control);
            _context.SaveChanges();
            return RedirectToAction("Index", "Home");
            
        }

        private bool ControlesExists(int id)
        {
          return (_context.Controles?.Any(e => e.idControl == id)).GetValueOrDefault();
        }
    }
}
