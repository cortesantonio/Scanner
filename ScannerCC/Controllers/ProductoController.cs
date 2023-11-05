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
    public class ProductoController : Controller
    {
        private readonly AppDbContext _context;

        public ProductoController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Productoes
        public async Task<IActionResult> Index(string Busqueda)
        {
            if (Busqueda == null)
            {
                return _context.Producto != null ?
                View(await _context.Producto.ToListAsync()) :
                Problem("Entity set 'AppDbContext.Producto'  is null.");
            }
            else
            {

                var esNumero = int.TryParse(Busqueda, out int parsedInt);
                if (esNumero)
                {
                    // Si es una entero, realiza la búsqueda por Codigo.

                    var Resultado = await _context.Producto.Where(x => x.CodigoBarra == parsedInt).ToListAsync();
                    return View(Resultado);

                }
                else
                {
                    // Si es una cadena, realiza la búsqueda por Nombre.
                    var Resultado = await _context.Producto.Where(x => x.Nombre.Contains(Busqueda)).ToListAsync();
                    return View(Resultado);

                }

            }

        }
        // GET: Productoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Producto == null)
            {
                return NotFound();
            }

            var producto = await _context.Producto
                .FirstOrDefaultAsync(m => m.idProducto == id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        // GET: Productoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Productoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
    [Bind("idProducto,CodigoBarra,Nombre,Cepa,PaisOrigen,PaisDestino,FechaCosecha,FechaProduccion,Capacidad," +
    "GradoAlcohol,Azucar,Sulfuroso,Densidad,TipoCapsula,TipoEtiqueta,ColorBotella,Medalla,ColorCapsula,TipoCorcho," +
    "TipoBotella,AlturaBotella,AnchoBotella,MedidadEtiquetaABoquete,MedidaEtiquetaABase")] Producto producto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(producto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(producto);
        }


        // GET: Productoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Producto == null)
            {
                return NotFound();
            }

            var producto = await _context.Producto.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }
            return View(producto);
        }

        // POST: Productoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("idProducto,CodigoBarra,Nombre,Cepa,PaisOrigen,PaisDestino,FechaCosecha,FechaProduccion,Capacidad,GradoAlcohol,Azucar,Sulfuroso,Densidad,TipoCapsula,TipoEtiqueta,ColorBotella,Medalla,ColorCapsula,TipoCorcho,TipoBotella,AlturaBotella,AnchoBotella,MedidadEtiquetaABoquete,MedidaEtiquetaABase")] Producto producto)
        {
            if (id != producto.idProducto)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(producto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductoExists(producto.idProducto))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(producto);
        }

        // GET: Productoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Producto == null)
            {
                return NotFound();
            }

            var producto = await _context.Producto
                .FirstOrDefaultAsync(m => m.idProducto == id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        // POST: Productoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Producto == null)
            {
                return Problem("Entity set 'AppDbContext.Producto'  is null.");
            }
            var producto = await _context.Producto.FindAsync(id);
            if (producto != null)
            {
                _context.Producto.Remove(producto);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductoExists(int id)
        {
          return (_context.Producto?.Any(e => e.idProducto == id)).GetValueOrDefault();
        }
    }
}
