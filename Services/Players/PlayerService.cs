using HvZ_backend.Data.Entities;
using HvZ_backend.Data.Exceptions;
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
            return await _context.Players
                .Include(p => p.Messages)
                .Include(p => p.PlayerRolesInKills)
                .ToListAsync();
        }

        // Retrieve a player by their ID from the database.
        public async Task<Player> GetPlayerByIdAsync(int playerId)
        {
            var player = await _context.Players
                .Include(p => p.Messages)
                .Include(p => p.PlayerRolesInKills)
                .FirstOrDefaultAsync(p => p.Id == playerId);

            if (player == null)
            {
                throw new EntityNotFoundException("Player", playerId);
            }

            return player;
        }

        // Create a new player and add them to the database.
        public async Task<Player> CreatePlayerAsync(Player player)
        {
            //Add starting location
            var location = new Location { XCoordinate = 1, YCoordinate = 1 };
            _context.Locations.Add(location);
            await _context.SaveChangesAsync();

            //Add random BiteCode
            var random = new Random();
            const string allowedChars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            var biteCode = new string(Enumerable.Repeat(allowedChars, 8)
                .Select(s => s[random.Next(s.Length)]).ToArray());

            //Determine Zombie property
            bool zombiesExist = await _context.Players.AnyAsync(p => p.GameId == player.GameId && p.Zombie);

            player.LocationId = location.Id;
            player.BiteCode = biteCode;
            player.Zombie = zombiesExist ? false : true;

            _context.Players.Add(player);
            await _context.SaveChangesAsync();

            return player;
        }

        public async Task<bool> updatePlayerLocationAsync(int playerId, int x, int y)
        {
            if (!await PlayerExistsAsync(playerId))
                throw new EntityNotFoundException(nameof(Player), playerId);

            var player = await _context.Players
                .Include(p => p.Location)
                .FirstOrDefaultAsync(p => p.Id == playerId);

            player.Location.XCoordinate = x;
            player.Location.YCoordinate = y;


            await _context.SaveChangesAsync();
            return true;
        }

        // Update an existing player's information in the database.
        public async Task<Player> UpdatePlayerAsync(Player player)
        {
            if (player == null)
            {
                throw new ArgumentNullException(nameof(player));
            }

            if (!await PlayerExistsAsync(player.Id))
            {
                throw new EntityNotFoundException("Player", player.Id);
            }

            _context.Entry(player).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return player;
        }

        // Delete a player by their ID from the database.
        public async Task DeletePlayerAsync(int playerId)
        {
            if (!await PlayerExistsAsync(playerId))
            {
                throw new EntityNotFoundException("Player", playerId);
            }

            var player = await _context.Players
                .Where(p => p.Id == playerId)
                .Include(p => p.Messages)
                .FirstAsync();

            // Remove all related messages.
            player.Messages.Clear();

            _context.Players.Remove(player);
            await _context.SaveChangesAsync();
        }

        // Check if a player with a specific ID exists in the database.
        private async Task<bool> PlayerExistsAsync(int playerId)
        {
            return await _context.Players.AnyAsync(p => p.Id == playerId);
        }

        public async Task<Player> UpdateZombieStateAsync(int playerId, bool zombie, string biteCode)
        {
            var player = await _context.Players.FindAsync(playerId);

            if (player == null)
            {
                throw new EntityNotFoundException("Player", playerId);
            }

            // Update the Zombie attribute
            player.Zombie = zombie;
            player.BiteCode = biteCode;


            await _context.SaveChangesAsync();

            return player;
        }

        public async Task<Player> GetPlayerByBiteCodeAsync(string biteCode)
        {
            return await _context.Players.SingleOrDefaultAsync(p => p.BiteCode == biteCode);
        }
    }
}