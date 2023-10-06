using System.ComponentModel.DataAnnotations;

namespace HvZ_backend.Data.Entities
{
    public class Kill
    {
        public int Id { get; set; }
        [StringLength(255)]
        public string Description { get; set; }
        public DateTime TimeOfKill { get; set; }
        public ICollection<PlayerKillRole>? PlayerRoles { get; set; }
    }
}
