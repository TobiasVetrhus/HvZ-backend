using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HvZ_backend.Data.Entities
{
    public class Kill
    {
        public int Id { get; set; }
        [StringLength(255)]
        public string Description { get; set; }
        [Column(TypeName = "timestamp")]
        public DateTime TimeOfKill { get; set; }
        public int KillerPlayerId { get; set; }
        public int VictimPlayerId { get; set; }

        //Navigation to access the killer and victim players
        public Player KillerPlayer { get; set; }
        public Player VictimPlayer { get; set; }


    }
}
