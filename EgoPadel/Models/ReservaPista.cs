using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EgoPadel.Models
{
    public class ReservaPista
    {
        public void asignarId(UsuarioApp usuarioApp)  //Pasarselo por parametro
        {
            UsuarioId = usuarioApp.Id;
        }
        [Key]
        public int Id { get; set; }

        [Required]
        public int PistaId { get; set; }

        [ForeignKey(nameof(PistaId))]
        public Pista Pista { get; set;}

        [Required]      
        public string UsuarioId { get; set; }

        [ForeignKey(nameof(UsuarioId))]
        public UsuarioApp UsuarioApp { get; set; }

        [Required(ErrorMessage = "Hora y Fecha de Pista obligatorio.")]
        public DateTime HoraInicio { get; set; }

        [Required(ErrorMessage = "Duración de Pista obligatoria.")]
        public int Duracion { get; set; }

        [Required(ErrorMessage = "El Precio de la reserva es Requerido")]
        [Range(1, int.MaxValue, ErrorMessage = "El Precio tiene que ser mayor a cero")]
        public double Precio { get; set; }
    }
}
