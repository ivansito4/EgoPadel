﻿using EgoPadel.Datos;
using EgoPadel.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
using System.Security.Claims;

namespace EgoPadel.Controllers
{
    public class ClasIndController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ClasIndController(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<IActionResult> Index()
        {
            var usuarios = from u in _db.UsuarioApp
                          select u;

            usuarios = usuarios.OrderByDescending(u => u.Puntos);

            ViewBag.userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            return View(await usuarios.AsNoTracking().ToListAsync());
        }
    }
}
