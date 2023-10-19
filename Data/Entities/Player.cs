using System.ComponentModel.DataAnnotations;

namespace HvZ_backend.Data.Entities
{
    public class Player
    {
        public int Id { get; set; }

        [StringLength(100, ErrorMessage = "Username cannot be longer than 100 characters.")]
        public string Username { get; set; }
        public bool Zombie { get; set; }

        [StringLength(8, ErrorMessage = "Bite code cannot be longer than 8 characters.")]
        public string BiteCode { get; set; }
        public Guid? UserId { get; set; }
        public int? LocationId { get; set; }
        public int? GameId { get; set; }
        public int? SquadId { get; set; }
        public ICollection<Message>? Messages { get; set; }
        public ICollection<Kill>? Kills { get; set; }

        //Navigation
        public AppUser User { get; set; }
        public Game Game { get; set; }
        public Location Location { get; set; }
        public Squad Squad { get; set; }
    }
}
