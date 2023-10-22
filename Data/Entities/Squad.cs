using System.ComponentModel.DataAnnotations;

namespace HvZ_backend.Data.Entities
{
    public class Squad
    {
        public int Id { get; set; }

        [StringLength(100)]
        public string SquadName { get; set; }

        public int? NumberOfMembers { get; set; }

        public int? NumberOfDeceased { get; set; }
        public int? GameId { get; set; }

        public Game Game { get; set; }
        public ICollection<Player>? Players { get; set; } //1-M
    }
}

