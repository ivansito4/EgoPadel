using EgoPadel.Datos;
using EgoPadel.Models;
using EgoPadel.Utilidades;
using Microsoft.AspNetCore.Mvc;

namespace EgoPadel.Controllers
{
    public class CarritoController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CarritoController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            List<Carrito> carritoLista = new List<Carrito>();

            if(HttpContext.Session.Get<IEnumerable<Carrito>>(WC.SessionCarrito)!=null &&
                HttpContext.Session.Get<IEnumerable<Carrito>>(WC.SessionCarrito).Count() > 0)
            {
                carritoLista = HttpContext.Session.Get<List<Carrito>>(WC.SessionCarrito);
            }

            List<int> prodCarrito = carritoLista.Select(i=>i.ProductoId).ToList();
            List<Producto> prodLista = _db.Producto.Where(p => prodCarrito.Contains(p.Id)).ToList();

            return View(prodLista);
        }
    }
}
