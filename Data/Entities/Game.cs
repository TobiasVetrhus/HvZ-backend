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
        [StringLength(50)]
        public string PictureURL { get; set; }
        public int PlayerId { get; set; }
        public int MissionId { get; set; }

        public Player Player { get; set; }
        public Mission Mission { get; set; }
    }
}
