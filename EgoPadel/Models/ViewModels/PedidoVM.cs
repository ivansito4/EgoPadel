using Microsoft.AspNetCore.Mvc.Rendering;

namespace EgoPadel.Models.ViewModels
{
    public class PedidoVM
    {
        public Pedido Pedido { get; set; }

        public IEnumerable<SelectListItem> UsuarioLista { get; set; }
    }
}
