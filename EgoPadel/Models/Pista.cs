using System.ComponentModel.DataAnnotations;

namespace EgoPadel.Models
{
    public class Pista
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Numero de Pista obligatorio.")]
        public int Numero { get; set; }

        public string? Descripcion { get; set; }
    }
}
