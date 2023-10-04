using System.ComponentModel.DataAnnotations;

namespace HvZ_backend.Data.DTOs.Rules
{
    public class RulePostDTO
    {
        [StringLength(50)]
        public string Title { get; set; }
        [StringLength(255)]
        public string Description { get; set; }
        public int[]? GameIds { get; set; }
    }
}
