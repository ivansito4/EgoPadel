namespace EgoPadel.Models.ViewModels
{
    public class ProductoUsuarioVM
    {
        public ProductoUsuarioVM()
        {
            ListaProducto = new List<Producto>();
        }

        public UsuarioApp UsuarioApp { get; set; }
        public List<Producto> ListaProducto { get; set; }
    }
}
