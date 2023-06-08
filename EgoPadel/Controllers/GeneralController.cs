using Microsoft.AspNetCore.Mvc;

namespace EgoPadel.Controllers
{
	public class GeneralController : Controller
	{
		public IActionResult MapaWeb()
		{
			return View();
		}
		
		public IActionResult Accesibilidad()
		{
			return View();
		}

		public IActionResult Contacto()
		{
			return View();
		}
	}
}
