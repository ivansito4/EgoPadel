namespace EgoPadel.Models.ViewModels
{
    public class DetalleProductoVM
    {
        public DetalleProductoVM()
        {
            Producto = new Producto();
        }
        public Producto Producto { get; set; }
        public bool ExisteEnCarrito { get; set; }
    }
}
