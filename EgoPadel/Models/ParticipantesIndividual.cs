using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EgoPadel.Models
{
    public class ParticipantesIndividual
    {

        public void asignarId(UsuarioApp usuarioApp)  
        {
            UsuarioId = usuarioApp.Id;
        }

        [Key]
        public int Id { get; set; }
        [Required]
        public int TorneoId { get; set; }

        [ForeignKey(nameof(TorneoId))]
        public Torneo Torneo{ get; set; }

        [Required]
        public string UsuarioId { get; set; }

        [ForeignKey(nameof(UsuarioId))]
        public UsuarioApp UsuarioApp { get; set; }

    }
}
