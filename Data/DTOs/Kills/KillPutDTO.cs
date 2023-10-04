using System.ComponentModel.DataAnnotations;

namespace HvZ_backend.Data.DTOs.Kills
{
    public class KillPutDTO
    {
        public int Id { get; set; }
        [StringLength(255)]
        public string Description { get; set; }
        public DateTime TimeOfKill { get; set; }
        public int KillerPlayerId { get; set; }
        public int VictimPlayerId { get; set; }
    }
}
