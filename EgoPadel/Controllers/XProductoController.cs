using EgoPadel.Datos;
using EgoPadel.Models;
using Microsoft.AspNetCore.Mvc;

namespace EgoPadel.Controllers
{
    public class XProductoController : Controller
    {
        private readonly ApplicationDbContext _db;

        public XProductoController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<Producto> listaProducto = _db.Producto;
            return View(listaProducto);
        }
    }
}
