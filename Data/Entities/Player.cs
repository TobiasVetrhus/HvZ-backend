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
        public int UserId { get; set; }
        public int LocationId { get; set; }
        public int GameId { get; set; }
        public int SquadId { get; set; }
        //public ICollection<Conversation>? Conversations { get; set; } //M-M
        public ICollection<Message>? Messages { get; set; } //M-M
        public ICollection<Kill>? KillsAsKiller { get; set; } // Navigation property for kills made by this player as a killer
        public ICollection<Kill>? KillsAsVictim { get; set; } // Navigation property for kills received by this player as a victim


        //Navigation
        public User User { get; set; }
        public Game Game { get; set; }
        public Location Location { get; set; }
        public Squad squad { get; set; }
    }
}
