using System.ComponentModel.DataAnnotations;

namespace HvZ_backend.Data.DTOs.Kills
{
    public class KillPostDTO
    {
        [StringLength(255)]
        public string Description { get; set; }
        public DateTime TimeOfKill { get; set; }
        public int KillerPlayerId { get; set; }
        public int VictimPlayerId { get; set; }
    }
}
