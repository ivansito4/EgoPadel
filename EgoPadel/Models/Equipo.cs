using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EgoPadel.Models
{
    public class Equipo
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Nombre del Equipo obligatorio.")]
        public string Nombre { get; set; }

        public string? FotoEscudo { get; set; }

        [DefaultValue(0)]
        public int Puntos { get; set; }
    }
}
