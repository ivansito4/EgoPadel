using EgoPadel.Datos;
using EgoPadel.Models;
using Microsoft.AspNetCore.Mvc;

namespace EgoPadel.Controllers
{
    public class PedidoDetalleController : Controller
    {
        private readonly ApplicationDbContext _db;

        public PedidoDetalleController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<PedidoDetalle> listaPedidoDetalleDetalle = _db.PedidoDetalles;
            return View(listaPedidoDetalleDetalle);
        }

        //Get
        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Crear(PedidoDetalle pedidoDetalle)
        {
            if (ModelState.IsValid)
            {
                _db.PedidoDetalles.Add(pedidoDetalle);
                _db.SaveChanges();
                return RedirectToAction(nameof(Index)); //Para que mande a index al hacer submit
            }
            return View(pedidoDetalle);

        }

        //Get
        public IActionResult Editar(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }
            var obj = _db.PedidoDetalles.Find(Id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Editar(PedidoDetalle pedidoDetalle)
        {
            if (ModelState.IsValid)
            {
                _db.PedidoDetalles.Update(pedidoDetalle);
                _db.SaveChanges();
                return RedirectToAction(nameof(Index)); //Para que mande a index al hacer submit
            }
            return View(pedidoDetalle);
        }

        //Get
        public IActionResult Borrar(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }
            var obj = _db.PedidoDetalles.Find(Id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Borrar(PedidoDetalle pedidoDetalle)
        {
            if (pedidoDetalle == null)
            {
                return NotFound();
            }
            _db.PedidoDetalles.Remove(pedidoDetalle);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index)); //Para que mande a index al hacer submit
        }
    }
}
