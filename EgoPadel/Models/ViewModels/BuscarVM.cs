using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace EgoPadel.Models.ViewModels
{
    public class BuscarVM
    {
        [Key] 
        public int Id { get; set; }
        public Equipo Equipo { get; set; }

        public UsuarioApp User { get; set; }
    }
}

