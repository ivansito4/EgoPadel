using EgoPadel.Models;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;

namespace EgoPadel.Controllers
{
	public class GeneralController : Controller
	{

        private readonly IEmailSender _emailSender;
        public GeneralController(IEmailSender emailSender)
        {
            _emailSender = emailSender;   
        }

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

        [HttpPost]
        public async Task<IActionResult> Contacto(ContactoModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData[WC.Error] = "Ha ocurrido un error al enviar el mensaje, intentalo más tarde";
                return View(model);
            }

            // Procesar el formulario de contacto 
            var subject = "Mensaje del usuario "+model.Nombre;

            string messageBody = "El usuario " + model.Nombre + " con dirección de correo '" + model.Email + "' te ha escrito " +
                "el siguiente mensaje:<br><br><br>"+model.Mensaje;

            await _emailSender.SendEmailAsync("phpochita@gmail.com", subject, messageBody);
            TempData[WC.Exitoso] = "Se ha enviado el mensaje. En breves contactaremos contigo.";

            return RedirectToAction("Index", "Home");
        }
    }
}
