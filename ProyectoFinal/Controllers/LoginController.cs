using Microsoft.AspNetCore.Mvc;
using ProyectoFinal.Models;

namespace ProyectoFinal.Controllers
{
    public class LoginController : Controller
    {
        
        private readonly InventarioContext _context;

        
        public LoginController(InventarioContext context)
        {
            _context = context;
        }

        
        public IActionResult Index()
        {
            return View();
        }


        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([Bind("Nombre,Contrasenia")] Usuario user)
        {
        
            List<Usuario> bucar = _context.Usuarios.ToList();

        
            if (bucar != null)
            {
        
                foreach (var item in bucar)
                {
        
        
                    if (item.Nombre == user.Nombre && item.Contrasenia == user.Contrasenia)
                    {
        
                        return RedirectToAction(nameof(Index), "Dashboard");
                    }
                }
            }

        
            ViewData["UserIncorret"] = "Usuario o Contraseña incorrectos";
        
            return View();
        }
        
        public IActionResult Registrar()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registrar([Bind("Nombre", "Contrasenia")] Usuario user)
        {
        
            if (ModelState.IsValid)
            {
            
                _context.Add(user);
                await _context.SaveChangesAsync();
            
                return RedirectToAction(nameof(Index), "Login");
            }
            
            return View("Registrar");
        }
    }
}
