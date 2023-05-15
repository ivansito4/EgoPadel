using EgoPadel.Datos;
using EgoPadel.Models;
using EgoPadel.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EgoPadel.Controllers
{
    public class ReservaController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ReservaController(ApplicationDbContext db)
        {
            _db = db;  
        }
        public IActionResult Index()
        {
            IEnumerable<ReservaPista> listaReserva = _db.ReservaPista.Include(c => c.Pista).Include(p => p.UsuarioApp);
            return View(listaReserva);
        }

        //Get
        public IActionResult Crear()
        {
            //IEnumerable<SelectListItem> pistaDropDown = _db.Pista.Select(c => new SelectListItem
            //{
            //    Text = c.Numero.ToString(),
            //    Value = c.Id.ToString()
            //});

            //ViewBag.pistaDropDown = pistaDropDown;

            //IEnumerable<SelectListItem> usuarioDropDown = _db.UsuarioApp.Select(c => new SelectListItem
            //{
            //    Text = c.UserName,
            //    Value = c.Id.ToString()
            //});
            
            //ViewBag.usuarioDropDown = usuarioDropDown.OrderBy(c => c.Text.ToLower()); ;

            ReservaVM reservaVM = new ReservaVM()
            {
                Reserva = new ReservaPista(),
                PistaLista = _db.Pista.Select(c => new SelectListItem
                {
                    Text = c.Numero.ToString(),
                    Value = c.Id.ToString()
                }),
                UsuarioLista = _db.UsuarioApp.Select(c => new SelectListItem
                {
                    Text = c.UserName,
                    Value = c.Id.ToString()
                }).OrderBy(c => c.Text.ToLower())
        };

            return View(reservaVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Crear(ReservaVM reservaVM)
        {
            if (!ModelState.IsValid)
            {
                _db.ReservaPista.Add(reservaVM.Reserva);
                _db.SaveChanges();
                return RedirectToAction(nameof(Index)); //Para que mande a index al hacer submit
            }
            return View(reservaVM);

        }

        //Get
        public IActionResult Editar(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }
            var obj = _db.ReservaPista.Find(Id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Editar(ReservaPista reserva)
        {
            if (ModelState.IsValid)
            {
                _db.ReservaPista.Update(reserva);
                _db.SaveChanges();
                return RedirectToAction(nameof(Index)); //Para que mande a index al hacer submit
            }
            return View(reserva);
        }

        //Get
        public IActionResult Borrar(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }
            var obj = _db.ReservaPista.Find(Id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Borrar(ReservaPista reserva)
        {
            if (reserva == null)
            {
                return NotFound();
            }
            _db.ReservaPista.Remove(reserva);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index)); //Para que mande a index al hacer submit
        }
    }
}
