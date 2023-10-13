using HvZ_backend.Data.Entities;

namespace HvZ_backend.Services.Games
{
    public interface IGameService : ICrudService<Game, int>
    {
        Task UpdateRulesAsync(int gameId, int[] ruleIds);
        Task UpdatePlayersAsync(int gameId, int[] playerIds);
        Task UpdateMissionsAsync(int gameId, int[] missionIds);
        Task UpdateConversationsAsync(int gameId, int[] conversations);
        Task AddRuleAsync(int gameId, int rule);
        Task AddPlayerAsync(int gameId, int player);
        Task AddMissionAsync(int gameId, int mission);
        Task AddConversationAsync(int gameId, int conversation);
        Task RemoveRuleAsync(int gameId, int ruleId);
        Task RemoveMissionAsync(int gameId, int missionId);
        Task RemoveConversationAsync(int gameId, int conversationId);
        Task RemovePlayerAsync(int gameId, int playerId);
        Task<ICollection<Game>> GetGamesByStateAsync(GameStatus gamestatus);
        Task<ICollection<Rule>> GetGameRulesAsync(int gameId);
        Task<ICollection<Player>> GetGamePlayersAsync(int gameId);
        Task<ICollection<Mission>> GetGameMissionsAsync(int gameId);
        Task<ICollection<Conversation>> GetGameConversationsAsync(int gameId);
    }
}
