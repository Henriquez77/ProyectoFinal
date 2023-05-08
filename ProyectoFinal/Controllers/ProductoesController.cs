using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoFinal.Models;

namespace ProyectoFinal.Controllers
{
    public class ProductoesController : Controller
    {

        
        private readonly InventarioContext _context;
       
        public ProductoesController(InventarioContext context)
        {
            _context = context;

        }

        
        public async Task<IActionResult> Index()
        {
            
            return _context.Productos != null ?
                        View(await _context.Productos.ToListAsync()) :
                        Problem("Entity set 'InventarioContext.Productos'  is null.");
        }

        
        public async Task<IActionResult> Details(int? id)
        {
        
            if (id == null || _context.Productos == null)
            {
        
                return NotFound();
            }

        
            var producto = await _context.Productos
                .FirstOrDefaultAsync(m => m.ProductoId == id);
        
            if (producto == null)
            {
        
                return NotFound();
            }
        
            return View(producto);
        }

        
        public IActionResult Create()
        {
        
            return View();

        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public async Task<IActionResult> Create([Bind("ProductoId,Nombre,Descripcion,Precio,Cantidad")] Producto producto)
        {
        
            if (ModelState.IsValid)
            {
        
                _context.Add(producto);
                await _context.SaveChangesAsync();
        
                return RedirectToAction(nameof(Index));
            }
        
            return View(producto);
        }

        
        public async Task<IActionResult> Edit(int? id)
        {
        
            if (id == null || _context.Productos == null)
            {
        
                return NotFound();
            }
        
            var producto = await _context.Productos.FindAsync(id);
        
            if (producto == null)
            {
        
                return NotFound();
            }
        
            return View(producto);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public async Task<IActionResult> Edit(int id, [Bind("ProductoId,Nombre,Descripcion,Precio,Cantidad")] Producto producto)
        {
        
            if (id != producto.ProductoId)
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
            
                    if (!ProductoExists(producto.ProductoId))
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

        
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Productos == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos
                .FirstOrDefaultAsync(m => m.ProductoId == id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Productos == null)
            {
                return Problem("Entity set 'InventarioContext.Productos'  is null.");
            }
            var producto = await _context.Productos.FindAsync(id);
            if (producto != null)
            {
                _context.Productos.Remove(producto);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        
        private bool ProductoExists(int id)
        {
            return (_context.Productos?.Any(e => e.ProductoId == id)).GetValueOrDefault();
        }
    }
}
