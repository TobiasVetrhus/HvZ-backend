using HvZ_backend.Data.Entities;

namespace HvZ_backend.Services.Games
{
    /// <summary>
    /// Interface for performing CRUD (Create, Read, Update, Delete) operations on Game entities.
    /// </summary>
    public interface IGameService : ICrudService<Game, int>
    {
        /// <summary>
        /// Updates rules associated with a game by their IDs.
        /// </summary>
        Task UpdateRulesAsync(int gameId, int[] ruleIds);

        /// <summary>
        /// Updates players associated with a game by their IDs.
        /// </summary>
        Task UpdatePlayersAsync(int gameId, int[] playerIds);

        /// <summary>
        /// Updates missions associated with a game by their IDs.
        /// </summary> 
        Task UpdateMissionsAsync(int gameId, int[] missionIds);

        /// <summary>
        /// Updates conversations associated with a game by their IDs.
        /// </summary>
        Task UpdateConversationsAsync(int gameId, int[] conversations);

        /// <summary>
        /// Adds a rule to a game by their respective IDs.
        /// </summary>
        Task AddRuleAsync(int gameId, int rule);

        /// <summary>
        /// Adds a player to a game by their respective IDs.
        /// </summary>
        Task AddPlayerAsync(int gameId, int player);

        /// <summary>
        /// Adds a mission to a game by their respective IDs.
        /// </summary>
        Task AddMissionAsync(int gameId, int mission);

        /// <summary>
        /// Adds a conversation to a game by their respective IDs.
        /// </summary>
        Task AddConversationAsync(int gameId, int conversation);

        /// <summary>
        /// Removes a rule from a game by their respective IDs.
        /// </summary>
        Task RemoveRuleAsync(int gameId, int ruleId);

        /// <summary>
        /// Removes a mission from a game by their respective IDs.
        /// </summary>
        Task RemoveMissionAsync(int gameId, int missionId);

        /// <summary>
        /// Removes a conversation from a game by their respective IDs.
        /// </summary>
        Task RemoveConversationAsync(int gameId, int conversationId);

        /// <summary>
        /// Removes a player from a game by their respective IDs.
        /// </summary>
        Task RemovePlayerAsync(int gameId, int playerId);

        /// <summary>
        /// Gets games by their game status.
        /// </summary>
        Task<ICollection<Game>> GetGamesByStateAsync(GameStatus gamestatus);

        /// <summary>
        /// Gets rules associated with a game by its ID.
        /// </summary>
        Task<ICollection<Rule>> GetGameRulesAsync(int gameId);

        /// <summary>
        /// Gets players associated with a game by its ID.
        /// </summary>
        Task<ICollection<Player>> GetGamePlayersAsync(int gameId);

        /// <summary>
        /// Gets missions associated with a game by its ID.
        /// </summary>
        Task<ICollection<Mission>> GetGameMissionsAsync(int gameId);

        /// <summary>
        /// Gets conversations associated with a game by its ID.
        /// </summary>
        Task<ICollection<Conversation>> GetGameConversationsAsync(int gameId);
    }
}
