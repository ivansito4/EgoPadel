using EgoPadel.Datos;
using EgoPadel.Models;
using Microsoft.AspNetCore.Mvc;

namespace EgoPadel.Controllers
{
    public class XReservaController : Controller
    {
        private readonly ApplicationDbContext _db;

        public XReservaController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<ReservaPista> listaPista = _db.ReservaPista;
            return View(listaPista);
        }

        //Get
        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Crear(ReservaPista reserva)
        {
            if (ModelState.IsValid)
            {
                _db.ReservaPista.Add(reserva);
                _db.SaveChanges();
                return RedirectToAction(nameof(Index)); //Para que mande a index al hacer submit
            }
            return View(reserva);

        }
    }
}
