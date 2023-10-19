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
        public async Task<IEnumerable<Player>> GetAllAsync()
        {
            return await _context.Players
                .Include(p => p.Messages)
                .ToListAsync();
        }

        // Retrieve a player by their ID from the database.
        public async Task<Player> GetByIdAsync(int id)
        {
            if (!await PlayerExistsAsync(id))
                throw new EntityNotFoundException(nameof(Player), id);

            var player = await _context.Players
                .Include(p => p.Messages)
                .FirstOrDefaultAsync(p => p.Id == id);

            return player;
        }

        // Create a new player and add them to the database.
        public async Task<Player> AddAsync(Player obj)
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
            bool zombiesExist = await _context.Players.AnyAsync(p => p.GameId == obj.GameId && p.Zombie);

            obj.LocationId = location.Id;
            obj.BiteCode = biteCode;
            obj.Zombie = zombiesExist ? false : true;

            _context.Players.Add(obj);
            await _context.SaveChangesAsync();

            return obj;
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
        public async Task<Player> UpdateAsync(Player obj)
        {
            if (!await PlayerExistsAsync(obj.Id))
                throw new EntityNotFoundException("Player", obj.Id);

            _context.Entry(obj).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return obj;
        }

        // Delete a player by their ID from the database.
        public async Task DeleteByIdAsync(int id)
        {
            if (!await PlayerExistsAsync(id))
                throw new EntityNotFoundException("Player", id);

            var player = await _context.Players
                .Where(p => p.Id == id)
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

        public async Task<Player> UpdateZombieStateAsync(string biteCode)
        {
            var player = await GetPlayerByBiteCodeAsync(biteCode);

            if (player == null)
            {
                throw new EntityNotFoundException("Player", "Player not found for bite code: " + biteCode);
            }

            // Update the Zombie attribute
            player.Zombie = true;
            await UpdateAsync(player);

            return player;
        }


        public async Task<Player> GetPlayerByBiteCodeAsync(string biteCode)
        {
            return await _context.Players.SingleOrDefaultAsync(p => p.BiteCode == biteCode);
        }
    }
}