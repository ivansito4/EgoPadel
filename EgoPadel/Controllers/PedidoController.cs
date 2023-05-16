using EgoPadel.Datos;
using EgoPadel.Models;
using EgoPadel.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EgoPadel.Controllers
{
    public class PedidoController : Controller   
    {
        private readonly ApplicationDbContext _db;

        public PedidoController(ApplicationDbContext db)
        {
            _db = db;  
        }
        public IActionResult Index()
        {
            IEnumerable<Pedido> listaPedido = _db.Pedido.Include(c => c.UsuarioApp);
            return View(listaPedido);
        }

        //Get
        public IActionResult Crear()
        {
            PedidoVM pedidoVM = new PedidoVM()
            {
                Pedido = new Pedido(),
                UsuarioLista = _db.UsuarioApp.Select(c => new SelectListItem
                {
                    Text = c.UserName,
                    Value = c.Id.ToString()
                }).OrderBy(c => c.Text.ToLower())
            };

            return View(pedidoVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Crear(PedidoVM pedidovm)
        {
            if (!ModelState.IsValid)
            {
                _db.Pedido.Add(pedidovm.Pedido);
                _db.SaveChanges();
                return RedirectToAction(nameof(Index)); //Para que mande a index al hacer submit
            }
            return View(pedidovm);

        }

        //Get
        public IActionResult Editar(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }
            var obj = _db.Pedido.Find(Id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Editar(Pedido pedido)
        {
            if (ModelState.IsValid)
            {
                _db.Pedido.Update(pedido);
                _db.SaveChanges();
                return RedirectToAction(nameof(Index)); //Para que mande a index al hacer submit
            }
            return View(pedido);
        }

        //Get
        public IActionResult Borrar(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }
            var obj = _db.Pedido.Find(Id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Borrar(Pedido pedido)
        {
            if (pedido == null)
            {
                return NotFound();
            }
            _db.Pedido.Remove(pedido);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index)); //Para que mande a index al hacer submit
        }
    }
}
