using Microsoft.AspNetCore.Mvc;

namespace EgoPadel.Controllers
{
    public class ProductoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
