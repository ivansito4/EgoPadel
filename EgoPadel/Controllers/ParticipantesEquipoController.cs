using EgoPadel.Datos;
using EgoPadel.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EgoPadel.Controllers
{
    public class ParticipantesEquipoController : Controller 
    {
        private readonly ApplicationDbContext _db;

        public ParticipantesEquipoController(ApplicationDbContext db)
        {
            _db = db;  
        }
        public IActionResult Index()
        {
            IEnumerable<ParticipantesEquipo> listaParticipantesEquipo = _db.ParticipantesEquipos.Include(c => c.Torneo).Include(c => c.Equipo);
            return View(listaParticipantesEquipo);
        }

        //Get
        public IActionResult Editar(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }
            var obj = _db.ParticipantesEquipos.Find(Id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Editar(ParticipantesEquipo participantesEquipo)
        {
            if (ModelState.IsValid)
            {
                _db.ParticipantesEquipos.Update(participantesEquipo);
                _db.SaveChanges();
                return RedirectToAction(nameof(Index)); //Para que mande a index al hacer submit
            }
            return View(participantesEquipo);
        }

        //Get
        public IActionResult Borrar(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }
            var obj = _db.ParticipantesEquipos.Find(Id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Borrar(ParticipantesEquipo participantesEquipo)
        {
            if (participantesEquipo == null)
            {
                return NotFound();
            }
            _db.ParticipantesEquipos.Remove(participantesEquipo);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index)); //Para que mande a index al hacer submit
        }
    }
}
