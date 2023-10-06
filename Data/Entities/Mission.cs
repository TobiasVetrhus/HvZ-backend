using System.ComponentModel.DataAnnotations;

namespace HvZ_backend.Data.Entities
{
    public class Mission
    {
        public int Id { get; set; }
        [StringLength(50)]
        public string Name { get; set; }
        [StringLength(255)]
        public string Description { get; set; }
        public int? LocationId { get; set; }
        public int? GameId { get; set; }

        // Navigation property 
        public Location Location { get; set; }
        public Game Game { get; set; }

    }
}
