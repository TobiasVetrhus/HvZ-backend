using HvZ_backend.Data.Entities;

namespace HvZ_backend.Services.Users
{
    /// <summary>
    /// Interface for performing CRUD (Create, Read, Update, Delete) operations on AppUser entities.
    /// </summary>
    public interface IAppUserService : ICrudService<AppUser, Guid>
    {
        /// <summary>
        /// Adds a player to an AppUser's association.
        /// </summary>
        /// <param name="userId">The unique ID of the AppUser.</param>
        /// <param name="playerId">The ID of the player to associate with the user.</param>
        Task AddPlayerAsync(Guid userId, int playerId);

        /// <summary>
        /// Updates the players associated with an AppUser.
        /// </summary>
        /// <param name="userId">The unique ID of the AppUser.</param>
        /// <param name="playerIds">An array of player IDs to associate with the user.</param>
        Task UpdatePlayersAsync(Guid userId, int[] playerIds);

        /// <summary>
        /// Removes a player from an AppUser's association.
        /// </summary>
        /// <param name="userId">The unique ID of the AppUser.</param>
        /// <param name="playerId">The ID of the player to disassociate from the user.</param>
        Task RemovePlayerAsync(Guid userId, int playerId);

        /// <summary>
        /// Gets an AppUser by their unique ID if it exists.
        /// </summary>
        /// <param name="id">The unique ID of the AppUser to retrieve.</param>
        /// <returns>The AppUser if found; otherwise, null.</returns>
        Task<AppUser> GetUserIfExists(Guid id);
    }
}

