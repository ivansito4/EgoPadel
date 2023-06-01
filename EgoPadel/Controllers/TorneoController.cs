using EgoPadel.Datos;
using EgoPadel.Models;
using EgoPadel.Models.ViewModels;
using EgoPadel.Utilidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

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

        public IActionResult Detalle(int Id)
        {
            
            DetalleTorneoVM detalleVM = new DetalleTorneoVM()
            {
                Torneo = _db.Torneo.Where(p => p.Id == Id).FirstOrDefault(),
                UsuarioRegistrado = false
            };

            return View(detalleVM);
        }

        [HttpPost, ActionName("Detalle")]
        public IActionResult InscribirTorneo(int Id)
        {
            //Busco de que modalidad es el torneo
            string modalidadTorneo = _db.Torneo.Where(p => p.Id == Id).FirstOrDefault().Modalidad;


            //Si entra aquí se registra el usuario en el torneo
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (modalidadTorneo == "Individual")
            {
                
                ParticipantesIndividual participantesIndividual = new ParticipantesIndividual()
                {
                    TorneoId = Id,
                    UsuarioId = userId
                };

                _db.ParticipantesIndividual.Add(participantesIndividual);

                _db.SaveChanges();
                TempData[WC.Exitoso] = "Te has apuntado correctamente";
            }
            else
            {
                UsuarioApp Usuario = (UsuarioApp)_db.Users.Where(f => f.Id == userId).FirstOrDefault();
                if (Usuario.EquipoId == null || Usuario.EquipoId == 0)
                {   //No tiene equipo
                    TempData[WC.Error] = "Necesitas un equipo para poder registrarte";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    //Tiene equipo y se inscribe
                    ParticipantesEquipo participantesEquipo = new ParticipantesEquipo()
                    {
                        TorneoId = Id,
                        EquipoId = (int)Usuario.EquipoId
                    };
                    _db.ParticipantesEquipos.Add(participantesEquipo);
                    _db.SaveChanges();
                    TempData[WC.Exitoso] = "Equipo apuntado correctamente";
                }
            }
            return RedirectToAction(nameof(Index));
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
                TempData[WC.Exitoso] = "Torneo creado correctamente";
                return RedirectToAction(nameof(Index)); //Para que mande a index al hacer submit
			}
            TempData[WC.Error] = "Error al crear el torneo";
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
                TempData[WC.Exitoso] = "Torneo editado correctamente";
                return RedirectToAction(nameof(Index)); //Para que mande a index al hacer submit
			}
            TempData[WC.Error] = "Error al editar el torneo";
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
                TempData[WC.Error] = "Error al borrar el torneo";
                return NotFound();
			}
			_db.Torneo.Remove(torneo);
			_db.SaveChanges();
            TempData[WC.Exitoso] = "Torneo borrado correctamente";
            return RedirectToAction(nameof(Index)); //Para que mande a index al hacer submit
		}
	}
}
