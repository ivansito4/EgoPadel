using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EgoPadel.Models
{
    public class UsuarioApp : IdentityUser  //extendemos para añadir mas campos al usuario(login y reg) 
    {

        [Required(ErrorMessage = "Nombre de Usuario obligatorio.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Apellidos de Usuario obligatorio.")]
        public string Apellidos { get; set; }

        [DefaultValue(0)]
        public int Puntos { get; set; }
        public int? EquipoId { get; set; }

        [ForeignKey(nameof(EquipoId))]
        public Equipo? Equipo { get; set; }

        [DefaultValue("sinfoto.jpg")]
        public string Foto { get; set; }
    }
}
