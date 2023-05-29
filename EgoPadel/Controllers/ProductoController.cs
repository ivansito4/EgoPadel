using EgoPadel.Datos;
using EgoPadel.Models;
using EgoPadel.Models.ViewModels;
using EgoPadel.Utilidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            ProductoVM productovm = new ProductoVM()
            {
                Producto = _db.Producto
            };
            return View(productovm);
        }

        public IActionResult Detalle(int Id)
        {
            List<Carrito> carritoLista = new List<Carrito>();
            if (HttpContext.Session.Get<IEnumerable<Carrito>>(WC.SessionCarrito) != null &&
                HttpContext.Session.Get<IEnumerable<Carrito>>(WC.SessionCarrito).Count() > 0)
            {
                carritoLista = HttpContext.Session.Get<List<Carrito>>(WC.SessionCarrito);
            }

            DetalleProductoVM detalleVM = new DetalleProductoVM()
            {
                Producto = _db.Producto.Where(p => p.Id == Id).FirstOrDefault(),
                ExisteEnCarrito = false
            };

            foreach (var item in carritoLista)
            {
                if(item.ProductoId == Id)
                {
                    detalleVM.ExisteEnCarrito = true;
                }
            }

            return View(detalleVM);
        }

        [HttpPost , ActionName("Detalle")]
        public IActionResult DetallePost(int Id)
        {
            List<Carrito> carritoLista = new List<Carrito>();
            if (HttpContext.Session.Get<IEnumerable<Carrito>>(WC.SessionCarrito)!=null && 
                HttpContext.Session.Get<IEnumerable<Carrito>>(WC.SessionCarrito).Count()>0)
            {
                carritoLista = HttpContext.Session.Get<List<Carrito>>(WC.SessionCarrito);
            }
            carritoLista.Add(new Carrito { ProductoId = Id });
            HttpContext.Session.Set(WC.SessionCarrito, carritoLista);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult QuitarCarrito(int Id)
        {
            List<Carrito> carritoLista = new List<Carrito>();
            if (HttpContext.Session.Get<IEnumerable<Carrito>>(WC.SessionCarrito) != null &&
                HttpContext.Session.Get<IEnumerable<Carrito>>(WC.SessionCarrito).Count() > 0)
            {
                carritoLista = HttpContext.Session.Get<List<Carrito>>(WC.SessionCarrito);
            }
            
            var productoAQuitar = carritoLista.SingleOrDefault(x=>x.ProductoId == Id);
            if(productoAQuitar != null)
            {
                carritoLista.Remove(productoAQuitar);
            }

            HttpContext.Session.Set(WC.SessionCarrito, carritoLista);
            return RedirectToAction(nameof(Index));
        }



        //Get
        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Crear(Producto producto)
        {
            if (ModelState.IsValid)
            {
                _db.Producto.Add(producto);
                _db.SaveChanges();
                return RedirectToAction(nameof(Index)); //Para que mande a index al hacer submit
            }
            return View(producto);

        }

        //Get
        public IActionResult Editar(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }
            var obj = _db.Producto.Find(Id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Editar(Producto producto)
        {
            if (ModelState.IsValid)
            {
                _db.Producto.Update(producto);
                _db.SaveChanges();
                return RedirectToAction(nameof(Index)); //Para que mande a index al hacer submit
            }
            return View(producto);
        }

        //Get
        public IActionResult Borrar(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }
            var obj = _db.Producto.Find(Id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Borrar(Producto producto)
        {
            if (producto == null)
            {
                return NotFound();
            }
            _db.Producto.Remove(producto);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index)); //Para que mande a index al hacer submit
        }

    }
}
