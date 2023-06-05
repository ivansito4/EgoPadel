using EgoPadel.Datos;
using EgoPadel.Infrastructura;
using EgoPadel.Models;
using EgoPadel.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NuGet.Protocol;
using System.Security.Claims;
using System.Text.RegularExpressions;

namespace EgoPadel.Controllers
{
    public class EquipoController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public EquipoController(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment)
        {
            _db = db;  
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<IActionResult> Index()
        {
            var equipos = from e in _db.Equipo
                          select e;

            equipos = equipos.OrderByDescending(e => e.Puntos);
           
            return View(await equipos.AsNoTracking().ToListAsync());
        }

        public async Task<IActionResult> Buscar()
        {
            var equipos = from equipo in _db.Equipo
                           join user in _db.UsuarioApp
                           on equipo.Id equals user.EquipoId
                           select equipo;
            ViewBag.Reservas = (from usuario in _db.UsuarioApp
                                join equipo in _db.Equipo
                                on usuario.EquipoId equals equipo.Id
                               select new UsuarioApp{
                                EquipoId = usuario.EquipoId,
                                Foto = equipo.FotoEscudo,
                               Nombre = equipo.Nombre,
                               Puntos = equipo.Puntos
                                }).OrderByDescending(e=>e.EquipoId).ToJson();

            return View(await equipos.AsNoTracking().ToListAsync());
        }

        public IActionResult Unirse(int Id)
        {
            int idEquipo = Id;
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            UsuarioApp user = _db.UsuarioApp.FirstOrDefault(u => u.Id == claim.Value);
            user.EquipoId = idEquipo;
            _db.UsuarioApp.Update(user);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        //Get
        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Crear(Equipo equipo)
        {

            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                string webRootPath = _webHostEnvironment.WebRootPath;

                string upload = webRootPath + WC.FotoEscudo;
              
                if (files.Count()==0)
                {
                    equipo.FotoEscudo = @"default.png";
                }
                else
                {
                    string fileName = Guid.NewGuid().ToString();
                    string extension = Path.GetExtension(files[0].FileName);

                    using (var fileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStream);
                    }
                    equipo.FotoEscudo = fileName + extension;
                }
                
                _db.Equipo.Add(equipo);
                _db.SaveChanges();
                return RedirectToAction(nameof(Index)); //Para que mande a index al hacer submit
            }
            return View(equipo);

        }

        //Get
        public IActionResult Editar(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }
            var obj = _db.Equipo.Find(Id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Editar(Equipo equipo)
        {
            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                string webRootPath = _webHostEnvironment.WebRootPath;

                var objEquipo = _db.Equipo.AsNoTracking().FirstOrDefault(e=>e.Id == equipo.Id);

                if (files.Count > 0)
                {
                    string upload = webRootPath + WC.FotoEscudo;
                    string fileName = Guid.NewGuid().ToString();
                    string extension = Path.GetExtension(files[0].FileName);

                    //borrar la imagen anterior
                    var anteriorFile = Path.Combine(upload, objEquipo.FotoEscudo);
                    if( System.IO.File.Exists(anteriorFile))
                    {
                        System.IO.File.Delete(anteriorFile);
                    }
                    //fin borrar imagen anterior

                    using (var fileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStream);
                    }

                    equipo.FotoEscudo = fileName + extension;
                }
                else
                {
                    equipo.FotoEscudo = objEquipo.FotoEscudo;
                }

                _db.Equipo.Update(equipo);
                _db.SaveChanges();
                return RedirectToAction(nameof(Index)); //Para que mande a index al hacer submit
            }
            return View(equipo);
        }

        //Get
        public IActionResult Borrar(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }
            var obj = _db.Equipo.Find(Id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Borrar(Equipo equipo)
        {
            
            if (equipo == null)
            {
                return NotFound();
            }
            var usuarios = _db.UsuarioApp.Where(u => u.EquipoId == equipo.Id);
            foreach(UsuarioApp u in usuarios) {
                u.EquipoId = null;
            }

            _db.Equipo.Remove(equipo);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index)); //Para que mande a index al hacer submit
        }
        public IActionResult Detalle(int Id)
        {

            Equipo equipo = _db.Equipo.Where(e => e.Id == Id).FirstOrDefault();
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            UsuarioApp user = _db.UsuarioApp.FirstOrDefault(u => u.Id == claim.Value);

            if(Id == user.EquipoId)
            {
                ViewBag.Equipo = true;
            }
            else
            {
                ViewBag.Equipo = false;
            }
            

            return View(equipo);
        }
    }
}


