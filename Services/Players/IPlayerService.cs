using HvZ_backend.Data.Entities;

namespace HvZ_backend.Services.Players
{
    /// <summary>
    /// Interface for performing CRUD (Create, Read, Update, Delete) operations on Player entities.
    /// </summary>
    public interface IPlayerService : ICrudService<Player, int>
    {
        /// <summary>
        /// Updates a player's Zombie state based on their BiteCode.
        /// </summary>
        /// <param name="biteCode">The BiteCode of the player to update.</param>
        Task<Player> UpdateZombieStateAsync(string biteCode);

        Task<Player> UpdatePlayerState(int playerId, bool state);

        /// <summary>
        /// Retrieves a player by their BiteCode.
        /// </summary>
        /// <param name="biteCode">The BiteCode of the player to retrieve.</param>
        Task<Player> GetPlayerByBiteCodeAsync(string biteCode);

        /// <summary>
        /// Updates a player's location in the game world.
        /// </summary>
        /// <param name="playerId">The ID of the player to update.</param>
        /// <param name="x">The X-coordinate of the new location.</param>
        /// <param name="y">The Y-coordinate of the new location.</param>
        Task<bool> updatePlayerLocationAsync(int playerId, int x, int y);
    }
}

