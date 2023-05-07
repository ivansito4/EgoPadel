using EgoPadel.Datos;
using EgoPadel.Models;
using Microsoft.AspNetCore.Mvc;

namespace EgoPadel.Controllers
{
    public class ParticipantesIndividualController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ParticipantesIndividualController(ApplicationDbContext db)
        {
            _db = db;  
        }
        public IActionResult Index()
        {
            IEnumerable<ParticipantesIndividual> listaParticipantesIndividual = _db.ParticipantesIndividual;

            return View(listaParticipantesIndividual);
        }

        //Get
        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Crear(ParticipantesIndividual participantesIndividual)
        {
            if (ModelState.IsValid)
            {
                _db.ParticipantesIndividual.Add(participantesIndividual);
                _db.SaveChanges();
            }
            return RedirectToAction(nameof(Index)); //Para que mande a index al hacer submit
        }
    }
}
