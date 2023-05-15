using Microsoft.AspNetCore.Mvc.Rendering;

namespace EgoPadel.Models.ViewModels
{
    public class ReservaVM
    {
        public ReservaPista Reserva { get; set; }

        public IEnumerable<SelectListItem> PistaLista { get; set; }

        public IEnumerable<SelectListItem> UsuarioLista { get; set; }
    }
}
