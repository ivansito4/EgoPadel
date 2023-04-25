using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EgoPadel.Models
{
    public class Empleado
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Nombre del Trabajador obligatorio.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Apellidos del Trabajador obligatorio.")]
        public string Apellidos { get; set; }

        public string Direccion { get; set; }

        public int Telefono { get; set; }
    }
}
