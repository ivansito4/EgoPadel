using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EgoPadel.Models
{
    public class ReservaPista
    {
        public void asignarPista(UsuarioApp usuarioApp)  //Pasarselo por parametro
        {
            UsuarioApp = usuarioApp;
        }
        [Key]
        public int Id { get; set; }

        [Required]
        public int PistaId { get; set; }

        [ForeignKey(nameof(PistaId))]
        public Pista Pista { get; set;}

        [Required]      //INICIALIZAR EN UN CTOR?????????
        public UsuarioApp UsuarioApp { get; set; }

        [Required(ErrorMessage = "Hora y Fecha de Pista obligatorio.")]
        public DateTime HoraInicio { get; set; }

        [Required(ErrorMessage = "Duración de Pista obligatoria.")]
        public int Duracion { get; set; }

        [Required]  
        public float Precio { get; set; }
    }
}
