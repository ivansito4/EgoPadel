using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EgoPadel.Models
{
    public class Pedido
    {
        [Key]
        public int Id { get; set; }
        public int ProductoId { get; set; }

        [ForeignKey(nameof(ProductoId))]
        public Producto Producto { get; set; }

        [Required(ErrorMessage = "Precio Total del Pedido obligatorio.")]
        public float PrecioTotal { get; set; }
        
       [Required(ErrorMessage = "Fecha del Pedido obligatorio.")]
        public DateTime Fecha { get; set; }
    }
}
