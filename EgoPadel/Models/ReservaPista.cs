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

        [Required(ErrorMessage = "Fecha de Reserva obligatoria.")]
        public string FechaReserva { get; set; }

        [Required(ErrorMessage = "Hora de Reserva obligatoria.")]
        public string HoraInicio { get; set; }

        [Required(ErrorMessage = "Duración de Pista obligatoria.")]
        public int Duracion { get; set; }

        [Required(ErrorMessage = "El Precio de la reserva es Requerido")]
        
        public double Precio { get; set; }
    }
}
