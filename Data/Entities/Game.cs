using System.ComponentModel.DataAnnotations;

namespace HvZ_backend.Data.Entities
{
    public enum GameStatus
    {
        Registration,
        InProgress,
        Complete
    }
    public class Game
    {
        public int Id { get; set; }
        [StringLength(50)]
        public string Title { get; set; }
        [StringLength(255)]
        public string Description { get; set; }
        public GameStatus GameState { get; set; }
        [StringLength(250)]
        public string PictureURL { get; set; }
        public string MapURL { get; set; }

        public ICollection<Rule>? Rules { get; set; }
        public ICollection<Player>? Players { get; set; }
        public ICollection<Mission>? Missions { get; set; }
        public ICollection<Conversation>? Conversations { get; set; }


    }
}
