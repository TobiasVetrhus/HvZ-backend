using System.ComponentModel.DataAnnotations;

namespace HvZ_backend.Data.Entities
{
    public class Kill
    {
        public int Id { get; set; }
        [StringLength(1000)]
        public string Description { get; set; }
        public DateTime TimeOfKill { get; set; }
        public int? PlayerId { get; set; }
        public int? LocationId { get; set; }
        public Player Player { get; set; }
        public Location Location { get; set; }
    }
}
