using HvZ_backend.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace HvZ_backend.Services.Players
{
    public class PlayerService : IPlayerService
    {
        private readonly HvZDbContext _context;

        public PlayerService(HvZDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        // Retrieve all players from the database.
        public async Task<IEnumerable<Player>> GetAllPlayersAsync()
        {
            return await _context.Players.ToListAsync();
        }

        // Retrieve a player by their ID from the database.
        public async Task<Player> GetPlayerByIdAsync(int playerId)
        {

            return await _context.Players.FirstOrDefaultAsync(p => p.Id == playerId);
        }

        // Create a new player and add them to the database.
        public async Task<Player> CreatePlayerAsync(Player player)
        {
            if (player == null)
            {
                throw new ArgumentNullException(nameof(player));
            }

            // Add the new player to the database and save changes.
            _context.Players.Add(player);
            await _context.SaveChangesAsync();

            // Return the newly created player.
            return player;
        }

        // Update an existing player's information in the database.
        public async Task<Player> UpdatePlayerAsync(Player player)
        {
            if (player == null)
            {
                throw new ArgumentNullException(nameof(player));
            }

            // Set the state of the player entity to modified to update it in the database.
            _context.Entry(player).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            // Return the updated player.
            return player;
        }

        // Delete a player by their ID from the database.
        public async Task<bool> DeletePlayerAsync(int playerId)
        {
            // Find the player with the specified ID.
            var player = await _context.Players.FirstOrDefaultAsync(p => p.Id == playerId);

            if (player != null)
            {
                var messagesToDelete = await _context.Messages
                    .Where(m => m.PlayerId == playerId)
                    .ToListAsync();

                _context.Messages.RemoveRange(messagesToDelete);

                // Remove the player from the database and save changes.
                _context.Players.Remove(player);
                await _context.SaveChangesAsync();

                // Player successfully deleted.
                return true;
            }

            // Player with the given ID was not found.
            return false;
        }
    }
}
