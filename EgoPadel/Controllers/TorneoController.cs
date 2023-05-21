using EgoPadel.Datos;
using EgoPadel.Models;
using EgoPadel.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EgoPadel.Controllers
{
    public class TorneoController : Controller 
    {
        private readonly ApplicationDbContext _db;

        public TorneoController(ApplicationDbContext db)
        {
            _db = db;  
        }
        public IActionResult Index()
        {
			TorneoVM torneovm = new TorneoVM()
			{
				Torneo=_db.Torneo
			};
            return View(torneovm);
        }

        //Get
        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Crear(Torneo torneo)
        {
            if (ModelState.IsValid)
            {
                _db.Torneo.Add(torneo);
                _db.SaveChanges();
				return RedirectToAction(nameof(Index)); //Para que mande a index al hacer submit
			}
            return View(torneo);
            
        }

		//Get
		public IActionResult Editar(int? Id)
		{
            if (Id== null ||Id==0)
            {
                return NotFound();
            }
            var obj = _db.Torneo.Find(Id);
            if(obj == null)
            {
                return NotFound();
            }
			return View(obj);
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Editar(Torneo torneo)
		{
			if (ModelState.IsValid)
			{
				_db.Torneo.Update(torneo);
				_db.SaveChanges();
				return RedirectToAction(nameof(Index)); //Para que mande a index al hacer submit
			}
			return View(torneo);
		}

		//Get
		public IActionResult Borrar(int? Id)
		{
			if (Id == null || Id == 0)
			{
				return NotFound();
			}
			var obj = _db.Torneo.Find(Id);
			if (obj == null)
			{
				return NotFound();
			}
			return View(obj);
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Borrar(Torneo torneo)
		{
			if (torneo == null)
			{
				return NotFound();
			}
			_db.Torneo.Remove(torneo);
			_db.SaveChanges();
			return RedirectToAction(nameof(Index)); //Para que mande a index al hacer submit
		}
	}
}
