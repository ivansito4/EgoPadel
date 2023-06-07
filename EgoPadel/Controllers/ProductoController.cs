using EgoPadel.Datos;
using EgoPadel.Models;
using EgoPadel.Models.ViewModels;
using EgoPadel.Utilidades;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EgoPadel.Controllers
{
    public class ProductoController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductoController(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
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
            var files = HttpContext.Request.Form.Files;
            string webRootPath = _webHostEnvironment.WebRootPath;

            string upload = webRootPath + WC.FotoProducto;

            if (files.Count() == 0)
            {
                producto.Foto = @"default.png";
            }
            else
            {
                string fileName = Guid.NewGuid().ToString();
                string extension = Path.GetExtension(files[0].FileName);

                using (var fileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
                {
                    files[0].CopyTo(fileStream);
                }
                producto.Foto = fileName + extension;
            }
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
                var files = HttpContext.Request.Form.Files;
                string webRootPath = _webHostEnvironment.WebRootPath;

                var objProd = _db.Producto.AsNoTracking().FirstOrDefault(e => e.Id == producto.Id);

                if (files.Count > 0)
                {
                    string upload = webRootPath + WC.FotoProducto;
                    string fileName = Guid.NewGuid().ToString();
                    string extension = Path.GetExtension(files[0].FileName);

                    //borrar la imagen anterior
                    var anteriorFile = Path.Combine(upload, objProd.Foto);
                    if (System.IO.File.Exists(anteriorFile))
                    {
                        System.IO.File.Delete(anteriorFile);
                    }
                    //fin borrar imagen anterior

                    using (var fileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStream);
                    }

                    producto.Foto = fileName + extension;
                }
                else
                {
                    producto.Foto = objProd.Foto;
                }

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
