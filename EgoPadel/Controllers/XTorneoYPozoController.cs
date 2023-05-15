using EgoPadel.Datos;
using EgoPadel.Models;
using Microsoft.AspNetCore.Mvc;

namespace EgoPadel.Controllers
{
    public class XTorneoYPozoController : Controller
    {
        private readonly ApplicationDbContext _db;

        public XTorneoYPozoController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<Torneo> listaTorneo = _db.Torneo;
            return View(listaTorneo);
        }
    }
}
