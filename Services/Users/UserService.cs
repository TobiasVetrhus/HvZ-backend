using HvZ_backend.Data.Entities;
using HvZ_backend.Data.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace HvZ_backend.Services.Users
{
    public class UserService : IUserService
    {
        private readonly HvZDbContext _context;

        public UserService(HvZDbContext context)
        {
            _context = context;
        }

        public async Task<User> AddAsync(User obj)
        {
            await _context.Users.AddAsync(obj);
            await _context.SaveChangesAsync();
            return obj;
        }
        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users.Include(u => u.Players).ToListAsync();
        }

        public async Task<User> GetByIdAsync(int id)
        {
            if (!await UserExistsAsync(id))
                throw new EntityNotFoundException(nameof(User), id);

            var user = await _context.Users.Where(c => c.Id == id)
                .Include(u => u.Players)
                .FirstAsync();

            return user;
        }

        public async Task<User> UpdateAsync(User obj)
        {
            if (!await UserExistsAsync(obj.Id))
                throw new EntityNotFoundException(nameof(User), obj.Id);

            _context.Entry(obj).State = EntityState.Modified;
            _context.SaveChanges();

            return obj;
        }



        public async Task DeleteByIdAsync(int id)
        {
            if (!await UserExistsAsync(id))
                throw new EntityNotFoundException(nameof(User), id);

            var user = await _context.Users
                .Where(c => c.Id == id)
                .FirstAsync();

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task AddPlayerAsync(int userId, int playerId)
        {
            if (!await UserExistsAsync(userId))
                throw new EntityNotFoundException(nameof(User), userId);

            if (!await UserExistsAsync(playerId))
                throw new EntityNotFoundException(nameof(Player), playerId);

            var user = await _context.Users
                .Include(u => u.Players)
                .FirstOrDefaultAsync(u => u.Id == userId);

            var player = await _context.Players
                .FindAsync(playerId);
            user.Players.Add(player);

            await _context.SaveChangesAsync();
        }

        public async Task UpdatePlayersAsync(int userId, int[] playerIds)
        {
            if (!await UserExistsAsync(userId))
                throw new EntityNotFoundException(nameof(User), userId);

            var user = await _context.Users
                .Include(u => u.Players)
                .FirstOrDefaultAsync(u => u.Id == userId);

            // Collect the player IDs that are no longer associated with the user
            var removedPlayerIds = user.Players.Select(p => p.Id).Except(playerIds).ToList();

            // Delete messages associated with the removed players
            foreach (int removedPlayerId in removedPlayerIds)
            {
                var messagesToDelete = await _context.Messages
                    .Where(m => m.PlayerId == removedPlayerId)
                    .ToListAsync();

                _context.Messages.RemoveRange(messagesToDelete);
            }

            // Clear the current player associations
            //user.Players.Clear();

            // Associate the user with the new set of players
            foreach (int playerId in playerIds)
            {
                if (!await UserExistsAsync(playerId))
                    throw new EntityNotFoundException(nameof(Player), playerId);

                var player = await _context.Players.FindAsync(playerId);
                user.Players.Add(player);
            }

            await _context.SaveChangesAsync();
        }


        public async Task RemovePlayerAsync(int userId, int playerId)
        {
            if (!await UserExistsAsync(userId))
                throw new EntityNotFoundException(nameof(User), userId);

            if (!await UserExistsAsync(playerId))
                throw new EntityNotFoundException(nameof(Player), playerId);

            var user = _context.Users
                .Where(u => u.Id == userId)
                .Include(u => u.Players)
                //.FirstOrDefaultAsync();
                .First();

            var player = _context.Players.First(p => p.Id == playerId);
            //user.Players.Remove(player);
            //player.UserId = null;
            await _context.SaveChangesAsync();

        }


        private async Task<bool> UserExistsAsync(int id)
        {
            return await _context.Users.AnyAsync(u => u.Id == id);
        }

        private async Task<bool> PlayerExistsAsync(int id)
        {
            return await _context.Players.AnyAsync(u => u.Id == id);
        }



        /*
private async Task<bool> PlayerExistsAsync(int movieId)
{
   return await _context.Players.AnyAsync(p => p.Id == playerId);
}
*/




    }
}
