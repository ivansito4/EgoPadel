using System.ComponentModel.DataAnnotations;

namespace EgoPadel.Models
{
    public class ContactoModel
    {
        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El correo electrónico es obligatorio")]
        [EmailAddress(ErrorMessage = "El correo electrónico no es válido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "El mensaje no puede estar vacío")]
        public string Mensaje { get; set; }
    }
}