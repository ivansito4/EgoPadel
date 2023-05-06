using EgoPadel.Datos;
using EgoPadel.Models;
using Microsoft.AspNetCore.Mvc;

namespace EgoPadel.Controllers
{
    public class ProductoController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ProductoController(ApplicationDbContext db)
        {
            _db = db;  
        }
        public IActionResult Index()
        {
            IEnumerable<Producto> listaProducto = _db.Producto;
            return View(listaProducto);
        }

        //Get
        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Crear(Producto productoCreado)
        {
            if(ModelState.IsValid)
            {
				_db.Producto.Add(productoCreado);
				_db.SaveChanges();
			}
            
            return RedirectToAction(nameof(Index)); //Para que mande a index al hacer submit
        }
    }
}
