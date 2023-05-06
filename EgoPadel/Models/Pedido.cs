using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EgoPadel.Models
{
    public class Pedido
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UsuarioId { get; set; }

        [ForeignKey("UsuarioAplicacionId")]
        public UsuarioApp UsuarioApp { get; set; }

        public DateTime FechaOrden { get; set; }

        [Required]      //Guardar los datos otra vez por si los cambia, que se guarden los de ESTE
        public string Telefono { get; set; }

        [Required]
        public string NombreCompleto { get; set; }

        [Required]
        public string Email { get; set; }
    }
}
