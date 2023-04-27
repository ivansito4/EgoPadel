using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EgoPadel.Models
{
    public class ParticipantesEquipo
    {
        [Key]
        public int Id { get; set; }

        public int TorneoId { get; set; }

        [ForeignKey(nameof(TorneoId))]
        public Torneo Torneo { get; set; }
    }
}
