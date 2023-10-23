using HvZ_backend.Data.Entities;

namespace HvZ_backend.Data.DTOs.Games
{
    public class GameDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public GameStatus GameState { get; set; }
        public string GameStateString => GameState.ToString(); // Convert enum to string
        public string PictureURL { get; set; }
        public string MapURL { get; set; }
        public int[]? RuleIds { get; set; }
        public int[]? PlayerIds { get; set; }
        public int[]? MissionIds { get; set; }
        public int[]? ConversationIds { get; set; }
    }
}
