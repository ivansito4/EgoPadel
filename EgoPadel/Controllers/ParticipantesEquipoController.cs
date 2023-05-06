using EgoPadel.Datos;
using EgoPadel.Models;
using Microsoft.AspNetCore.Mvc;

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
            IEnumerable<ParticipantesEquipo> listaParticipantesEquipo = _db.ParticipantesEquipos;
            return View(listaParticipantesEquipo);
        }

        //Get
        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Crear(ParticipantesEquipo participantesEquipo)
        {
            if (ModelState.IsValid)
            {
                _db.ParticipantesEquipos.Add(participantesEquipo);
                _db.SaveChanges();
            }
            return RedirectToAction(nameof(Index)); //Para que mande a index al hacer submit
        }
    }
}
