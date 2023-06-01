using EgoPadel.Datos;
using EgoPadel.Models;
using EgoPadel.Models.ViewModels;
using EgoPadel.Utilidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EgoPadel.Controllers
{
    [Authorize]
    public class CarritoController : Controller
    {
        private readonly ApplicationDbContext _db;

        [BindProperty]
        public ProductoUsuarioVM productoUsuarioVM { get; set; }

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Index")]
        public IActionResult IndexPost()
        {
            return RedirectToAction(nameof(Resumen));
        }

        public IActionResult Resumen()
        {
            //Pillar usuario conectado
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            List<Carrito> carritoLista = new List<Carrito>();

            if (HttpContext.Session.Get<IEnumerable<Carrito>>(WC.SessionCarrito) != null &&
                HttpContext.Session.Get<IEnumerable<Carrito>>(WC.SessionCarrito).Count() > 0)
            {
                carritoLista = HttpContext.Session.Get<List<Carrito>>(WC.SessionCarrito);
            }

            List<int> prodCarrito = carritoLista.Select(i => i.ProductoId).ToList();
            List<Producto> prodLista = _db.Producto.Where(p => prodCarrito.Contains(p.Id)).ToList();

            productoUsuarioVM = new ProductoUsuarioVM()
            {
                UsuarioApp = _db.UsuarioApp.FirstOrDefault(u => u.Id == claim.Value),
                ListaProducto = prodLista
            };

            return View(productoUsuarioVM);
        }

        public IActionResult Quitar (int Id)
        {
            List<Carrito> carritoLista = new List<Carrito>();

            if (HttpContext.Session.Get<IEnumerable<Carrito>>(WC.SessionCarrito) != null &&
                HttpContext.Session.Get<IEnumerable<Carrito>>(WC.SessionCarrito).Count() > 0)
            {
                carritoLista = HttpContext.Session.Get<List<Carrito>>(WC.SessionCarrito);
            }

            carritoLista.Remove(carritoLista.FirstOrDefault(p => p.ProductoId == Id));
            HttpContext.Session.Set(WC.SessionCarrito, carritoLista);
            return RedirectToAction("Index");
        }
    }

}
