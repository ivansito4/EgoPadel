using EgoPadel.Datos;
using EgoPadel.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            IEnumerable<ParticipantesIndividual> listaParticipantesIndividual = _db.ParticipantesIndividual
                                                                                .Include(c => c.Torneo).Include(c => c.UsuarioApp);

            return View(listaParticipantesIndividual);
        }

        public IActionResult Borrar(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }
            var obj = _db.ParticipantesIndividual.Find(Id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Borrar(ParticipantesIndividual participantesIndividual)
        {
            if (participantesIndividual == null)
            {
                return NotFound();
            }
            _db.ParticipantesIndividual.Remove(participantesIndividual);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index)); //Para que mande a index al hacer submit
        }
    }
}
