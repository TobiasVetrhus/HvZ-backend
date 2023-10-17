using HvZ_backend.Data.Entities;
using HvZ_backend.Data.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace HvZ_backend.Services.Users
{
    public class AppUserService : IAppUserService
    {
        private readonly HvZDbContext _context;

        public AppUserService(HvZDbContext context)
        {
            _context = context;
        }

        public async Task<AppUser> AddAsync(AppUser obj)
        {
            await _context.AppUsers.AddAsync(obj);
            await _context.SaveChangesAsync();
            return obj;
        }
        public async Task<IEnumerable<AppUser>> GetAllAsync()
        {
            return await _context.AppUsers.Include(u => u.Players).ToListAsync();
        }

        public async Task<AppUser> GetUserIfExists(Guid id)
        {
            var user = await _context.AppUsers.FirstOrDefaultAsync(u => u.Id == id);
            return user;
        }

        public async Task<AppUser> GetByIdAsync(Guid id)
        {
            if (!await UserExistsAsync(id))
                throw new EntityNotFoundException(nameof(AppUser), id);

            var user = await _context.AppUsers.Where(c => c.Id == id)
                .Include(u => u.Players)
                .FirstOrDefaultAsync();

            return user;
        }

        public async Task<AppUser> UpdateAsync(AppUser obj)
        {
            if (!await UserExistsAsync(obj.Id))
                throw new EntityNotFoundException(nameof(AppUser), obj.Id);

            _context.Entry(obj).State = EntityState.Modified;
            _context.SaveChanges();

            return obj;
        }

        public async Task DeleteByIdAsync(Guid id)
        {
            if (!await UserExistsAsync(id))
                throw new EntityNotFoundException(nameof(AppUser), id);

            var user = await _context.AppUsers
                .Where(u => u.Id == id)
                .Include(u => u.Players)
                .FirstAsync();

            user.Players.Clear();

            _context.AppUsers.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task AddPlayerAsync(Guid userId, int playerId)
        {
            if (!await UserExistsAsync(userId))
                throw new EntityNotFoundException(nameof(AppUser), userId);

            if (!await PlayerExistsAsync(playerId))
                throw new EntityNotFoundException(nameof(Player), playerId);

            var user = await _context.AppUsers
                .Include(u => u.Players)
                .FirstOrDefaultAsync(u => u.Id == userId);

            var player = await _context.Players
                .FindAsync(playerId);
            user.Players.Add(player);

            await _context.SaveChangesAsync();
        }

        public async Task UpdatePlayersAsync(Guid userId, int[] playerIds)
        {
            if (!await UserExistsAsync(userId))
                throw new EntityNotFoundException(nameof(AppUser), userId);

            var user = await _context.AppUsers
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
                if (!await PlayerExistsAsync(playerId))
                    throw new EntityNotFoundException(nameof(Player), playerId);

                var player = await _context.Players.FindAsync(playerId);
                user.Players.Add(player);
            }

            await _context.SaveChangesAsync();
        }


        public async Task RemovePlayerAsync(Guid userId, int playerId)
        {
            if (!await UserExistsAsync(userId))
                throw new EntityNotFoundException(nameof(AppUser), userId);

            if (!await PlayerExistsAsync(playerId))
                throw new EntityNotFoundException(nameof(Player), playerId);

            var user = _context.AppUsers
                .Where(u => u.Id == userId)
                .Include(u => u.Players)
                //.FirstOrDefaultAsync();
                .First();

            var player = _context.Players.First(p => p.Id == playerId);
            //user.Players.Remove(player);
            //player.UserId = null;
            await _context.SaveChangesAsync();

        }


        private async Task<bool> UserExistsAsync(Guid id)
        {
            return await _context.AppUsers.AnyAsync(u => u.Id == id);
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
