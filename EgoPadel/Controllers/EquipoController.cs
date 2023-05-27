using EgoPadel.Datos;
using EgoPadel.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;

namespace EgoPadel.Controllers
{
    public class EquipoController : Controller
    {
        private readonly ApplicationDbContext _db;

        public EquipoController(ApplicationDbContext db)
        {
            _db = db;  
        }
        public async Task<IActionResult> Index()
        {
            var equipos = from e in _db.Equipo
                          select e;

            equipos = equipos.OrderByDescending(e => e.Puntos);
           
            return View(await equipos.AsNoTracking().ToListAsync());
        }

        //Get
        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Crear(Equipo equipo)
        {
            if (ModelState.IsValid)
            {
                _db.Equipo.Add(equipo);
                _db.SaveChanges();
                return RedirectToAction(nameof(Index)); //Para que mande a index al hacer submit
            }
            return View(equipo);

        }

        //Get
        public IActionResult Editar(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }
            var obj = _db.Equipo.Find(Id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Editar(Equipo equipo)
        {
            if (ModelState.IsValid)
            {
                _db.Equipo.Update(equipo);
                _db.SaveChanges();
                return RedirectToAction(nameof(Index)); //Para que mande a index al hacer submit
            }
            return View(equipo);
        }

        //Get
        public IActionResult Borrar(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }
            var obj = _db.Equipo.Find(Id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Borrar(Equipo equipo)
        {
            if (equipo == null)
            {
                return NotFound();
            }
            _db.Equipo.Remove(equipo);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index)); //Para que mande a index al hacer submit
        }
    }
}
