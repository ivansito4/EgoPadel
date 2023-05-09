using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace EgoPadel.Models
{
    public class UsuarioApp : IdentityUser  //extendemos para añadir mas campos al usuario(login y reg) 
    {
        public string Nombre { get; set; }
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
