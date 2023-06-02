using EgoPadel.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGeneration.EntityFrameworkCore;
using EgoPadel.Models.ViewModels;

namespace EgoPadel.Datos
{
    public class ApplicationDbContext :IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {

        }
        public DbSet<Producto> Producto { get; set; }
        public DbSet<Pista> Pista { get; set; }
        public DbSet<Equipo> Equipo { get; set; }
        public DbSet<UsuarioApp> UsuarioApp { get; set; }
        public DbSet<ReservaPista> ReservaPista { get; set; }
        public DbSet<Empleado> Empleado { get; set; }
        public DbSet<Torneo> Torneo { get; set; } 
        public DbSet<ParticipantesIndividual> ParticipantesIndividual { get; set; }
        public DbSet<ParticipantesEquipo> ParticipantesEquipos { get; set; }
        public DbSet<Pedido> Pedido { get; set; }   

        public DbSet<PedidoDetalle> PedidoDetalles { get; set; }

        public DbSet<EgoPadel.Models.ViewModels.BuscarVM>? BuscarVM { get; set; }
    }
}
