using System.ComponentModel.DataAnnotations;

namespace EgoPadel.Models
{
    public class Producto
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Nombre de Producto obligatorio.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Precio de Producto obligatorio.")]
        [Range(1, int.MaxValue, ErrorMessage = "El Precio tiene que ser mayor a cero.")]
        public double Precio { get; set; }

        public string? Descripcion { get; set; }

        public string? Foto { get; set; }


    }
}
