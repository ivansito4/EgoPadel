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
        [Required(ErrorMessage = "Dirección del Trabajador obligatorio.")]
        public string Direccion { get; set; }
        [Required(ErrorMessage = "Teléfono del Trabajador obligatorio.")]
        public int Telefono { get; set; }
    }
}
