using HvZ_backend.Data.Entities;

namespace HvZ_backend.Services.Squads
{
    /// <summary>
    /// Interface for performing CRUD (Create, Read, Update, Delete) operations on Squad entities.
    /// </summary>
    public interface ISquadService : ICrudService<Squad, int>
    {
        /// <summary>
        /// Get a collection of squads associated with a specific game.
        /// </summary>
        /// <param name="gameId">The ID of the game for which squads are requested.</param>
        Task<ICollection<Squad>> GetSquadsByGameAsync(int gameId);

        /// <summary>
        /// Update the players in a squad by their IDs.
        /// </summary>
        /// <param name="squadId">The ID of the squad to update.</param>
        /// <param name="playerIds">An array of player IDs to associate with the squad.</param>
        Task UpdatePlayersAsync(int squadId, int[] playerIds);

        /// <summary>
        /// Add a player to a squad.
        /// </summary>
        /// <param name="squadId">The ID of the squad to add the player to.</param>
        /// <param name="playerId">The ID of the player to add to the squad.</param>
        Task AddPlayerAsync(int squadId, int player);

        /// <summary>
        /// Remove a player from a squad.
        /// </summary>
        /// <param name="squadId">The ID of the squad to remove the player from.</param>
        /// <param name="playerId">The ID of the player to remove from the squad.</param>
        Task RemovePlayerAsync(int squadId, int player);

        /// <summary>
        /// Associate a game with a squad.
        /// </summary>
        /// <param name="squadId">The ID of the squad to associate with a game.</param>
        /// <param name="gameId">The ID of the game to associate with the squad.</param>
        Task AddGameToSquadAsync(int squadId, int gameId);

        /// <summary>
        /// Get a collection of squads based on their size within a specified range.
        /// </summary>
        /// <param name="minSize">The minimum squad size.</param>
        /// <param name="maxSize">The maximum squad size.</param>
        Task<ICollection<Squad>> GetSquadsBySizeAsync(int minSize, int maxSize);

        /// <summary>
        /// Get a collection of players within a squad.
        /// </summary>
        /// <param name="squadId">The ID of the squad to retrieve players from.</param>
        Task<ICollection<Player>> GetSquadPlayersAsync(int squadId);
    }
}
