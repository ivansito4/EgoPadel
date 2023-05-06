using EgoPadel.Datos;
using EgoPadel.Models;
using Microsoft.AspNetCore.Mvc;

namespace EgoPadel.Controllers
{
    public class EmpleadoController : Controller
    {
        private readonly ApplicationDbContext _db;

        public EmpleadoController(ApplicationDbContext db)
        {
            _db = db;  
        }
        public IActionResult Index()
        {
            IEnumerable<Empleado> listaEmpleado = _db.Empleado;
            return View(listaEmpleado);
        }

        //Get
        public IActionResult Crear()
        {
            return View();
        }

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Crear(Empleado empleado)
		{
			if (ModelState.IsValid)
			{
				_db.Empleado.Add(empleado);
				_db.SaveChanges();
				return RedirectToAction(nameof(Index)); //Para que mande a index al hacer submit
			}
			return View(empleado);

		}

		//Get
		public IActionResult Editar(int? Id)
		{
			if (Id == null || Id == 0)
			{
				return NotFound();
			}
			var obj = _db.Empleado.Find(Id);
			if (obj == null)
			{
				return NotFound();
			}
			return View(obj);
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Editar(Empleado empleado)
		{
			if (ModelState.IsValid)
			{
				_db.Empleado.Update(empleado);
				_db.SaveChanges();
				return RedirectToAction(nameof(Index)); //Para que mande a index al hacer submit
			}
			return View(empleado);
		}

		//Get
		public IActionResult Borrar(int? Id)
		{
			if (Id == null || Id == 0)
			{
				return NotFound();
			}
			var obj = _db.Empleado.Find(Id);
			if (obj == null)
			{
				return NotFound();
			}
			return View(obj);
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Borrar(Empleado empleado)
		{
			if (empleado == null)
			{
				return NotFound();
			}
			_db.Empleado.Remove(empleado);
			_db.SaveChanges();
			return RedirectToAction(nameof(Index)); //Para que mande a index al hacer submit
		}
	}
}
