using EgoPadel.Datos;
using EgoPadel.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EgoPadel.Controllers
{
    public class PuntosController : Controller
    {
        private readonly ApplicationDbContext _db;

        public PuntosController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Detalle(string nombreUsuario)
        {

            UsuarioApp user = _db.UsuarioApp.Where(u => u.UserName == nombreUsuario).FirstOrDefault();

            return View(user);
        }


        public IActionResult CambiarPuntosIndividual(string Id, int Puntos)
        {
         
             UsuarioApp user = _db.UsuarioApp.Where(u => u.Id == Id).FirstOrDefault();
             user.Puntos = Puntos;
             _db.UsuarioApp.Update(user);
             _db.SaveChanges();
             return RedirectToAction("Index" , "ClasInd");
            
        }

        public IActionResult CambiarPuntosEquipo(int Id, int Puntos)
        {

            Equipo equipo = _db.Equipo.Where(u => u.Id == Id).FirstOrDefault();
            equipo.Puntos = Puntos;
            _db.Equipo.Update(equipo);
            _db.SaveChanges();
            return RedirectToAction("Index", "Equipo");

        }
    }
}
