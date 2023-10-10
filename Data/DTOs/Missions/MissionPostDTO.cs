using System.ComponentModel.DataAnnotations;

namespace HvZ_backend.Data.DTOs.Missions
{
    public class MissionPostDTO
    {
        [StringLength(50)]
        public string Name { get; set; }
        [StringLength(255)]
        public string Description { get; set; }
        public int? LocationId { get; set; }
        public int? GameId { get; set; }
    }
}
