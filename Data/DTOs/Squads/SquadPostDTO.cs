using System.ComponentModel.DataAnnotations;

namespace HvZ_backend.Data.DTOs.Squads
{
    public class SquadPostDTO
    {
        [StringLength(100)]
        public string SquadName { get; set; }
        public int? NumberOfMembers { get; set; }
        public int? NumberOfDeceased { get; set; }
        public int[]? PlayerIds { get; set; }
    }
}
