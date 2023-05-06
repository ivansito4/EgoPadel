using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EgoPadel.Models
{
    public class Torneo
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Tipo del Torneo obligatorio.")]
        public byte Tipo { get; set; }

        [Required(ErrorMessage = "Fecha del Torneo obligatoria.")]
        public DateTime Fecha { get; set; }

        [Required(ErrorMessage = "Número de plazas del Torneo obligatoria.")]
        public int NroPlazas { get; set; }

        public string? Descripcion { get; set; }

        public string? Premio { get; set; }
    }
}
