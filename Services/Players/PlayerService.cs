using HvZ_backend.Data.Entities;


namespace HvZ_backend.Services.Players
{
    public class PlayerService : IPlayerService
    {
        private readonly ApplicationDbContext _context;

        public PlayerService(ApplicationDbContext context)
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

            _context.Players.Add(player);
            await _context.SaveChangesAsync();

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

            return player;
        }

        // Delete a player by their ID from the database.
        public async Task<bool> DeletePlayerAsync(int playerId)
        {
            // Find the player with the specified ID.
            var player = await _context.Players.FirstOrDefaultAsync(p => p.Id == playerId);

            if (player != null)
            {
                // Remove the player from the database and save changes.
                _context.Players.Remove(player);
                await _context.SaveChangesAsync();
                return true; // Player successfully deleted.
            }

            return false; // Player with the given ID was not found.
        }
    }
}