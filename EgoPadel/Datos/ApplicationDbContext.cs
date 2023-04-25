using EgoPadel.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EgoPadel.Datos
{
    public class ApplicationDbContext :IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {

        }
        public DbSet<Producto> Producto { get; set; }
        public DbSet<Pista> Pista { get; set; }

        //añadir ReservarPista

        public DbSet<Equipo> Equipo { get; set; }
        public DbSet<UsuarioApp> UsuarioApp { get; set; }
    }
}
