using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoFinal.Models;

namespace ProyectoFinal.Controllers
{
    public class ComprasController : Controller
    {
        private readonly InventarioContext _context;

        public ComprasController(InventarioContext context)
        {
            _context = context;
        }

        
        public async Task<IActionResult> Index()
        {
            var inventarioContext = _context.Compras.Include(c => c.Producto).Include(c => c.Proveedor);
            return View(await inventarioContext.ToListAsync());
        }

        
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Compras == null)
            {
                return NotFound();
            }

            var compra = await _context.Compras
                .Include(c => c.Producto)
                .Include(c => c.Proveedor)
                .FirstOrDefaultAsync(m => m.CompraId == id);
            if (compra == null)
            {
                return NotFound();
            }

            return View(compra);
        }

        
        public IActionResult Create()
        {
            ViewData["ProductoId"] = new SelectList(_context.Productos, "ProductoId", "Nombre");
            ViewData["ProveedorId"] = new SelectList(_context.Proveedores, "ProveedorId", "Nombre");
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CompraId,Titulo,Fecha,ProductoId,Cantidad,ProveedorId,Total")] Compra compra)
        {
            if (ModelState.IsValid)
            {
                _context.Add(compra);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductoId"] = new SelectList(_context.Productos, "ProductoId", "ProductoId", compra.ProductoId);
            ViewData["ProveedorId"] = new SelectList(_context.Proveedores, "ProveedorId", "ProveedorId", compra.ProveedorId);
            return View(compra);
        }

        
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Compras == null)
            {
                return NotFound();
            }

            var compra = await _context.Compras.FindAsync(id);
            if (compra == null)
            {
                return NotFound();
            }
            ViewData["ProductoId"] = new SelectList(_context.Productos, "ProductoId", "Nombre", compra.ProductoId);
            ViewData["ProveedorId"] = new SelectList(_context.Proveedores, "ProveedorId", "Nombre", compra.ProveedorId);
            return View(compra);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CompraId,Titulo,Fecha,ProductoId,Cantidad,ProveedorId,Total")] Compra compra)
        {
            if (id != compra.CompraId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(compra);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompraExists(compra.CompraId))
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
            ViewData["ProductoId"] = new SelectList(_context.Productos, "ProductoId", "ProductoId", compra.ProductoId);
            ViewData["ProveedorId"] = new SelectList(_context.Proveedores, "ProveedorId", "ProveedorId", compra.ProveedorId);
            return View(compra);
        }

        
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Compras == null)
            {
                return NotFound();
            }

            var compra = await _context.Compras
                .Include(c => c.Producto)
                .Include(c => c.Proveedor)
                .FirstOrDefaultAsync(m => m.CompraId == id);
            if (compra == null)
            {
                return NotFound();
            }

            return View(compra);
        }

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Compras == null)
            {
                return Problem("Entity set 'InventarioContext.Compras'  is null.");
            }
            var compra = await _context.Compras.FindAsync(id);
            if (compra != null)
            {
                _context.Compras.Remove(compra);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CompraExists(int id)
        {
            return (_context.Compras?.Any(e => e.CompraId == id)).GetValueOrDefault();
        }
    }
}
