using HvZ_backend.Data.Entities;

namespace HvZ_backend.Services.Games
{
    public interface IGameService : ICrudService<Game, int>
    {
        Task UpdateRulesAsync(int gameId, int[] ruleIds);
        Task UpdatePlayersAsync(int gameId, int[] playerIds);
        Task UpdateMissionsAsync(int gameId, int[] missionIds);
        Task UpdateConversationsAsync(int gameId, int[] conversations);
        Task<ICollection<Game>> GetGamesByStateAsync(GameStatus gamestatus);
        Task<ICollection<Rule>> GetGameRulesAsync(int gameId);
        Task<ICollection<Player>> GetGamePlayersAsync(int gameId);
        Task<ICollection<Mission>> GetGameMissionsAsync(int gameId);
        Task<ICollection<Conversation>> GetGameConversationsAsync(int gameId);
    }
}
