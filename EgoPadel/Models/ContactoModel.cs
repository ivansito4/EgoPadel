using System.ComponentModel.DataAnnotations;

namespace EgoPadel.Models
{
    public class ContactoModel
    {
        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El correo electr�nico es obligatorio")]
        [EmailAddress(ErrorMessage = "El correo electr�nico no es v�lido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "El mensaje no puede estar vac�o")]
        public string Mensaje { get; set; }
    }
}