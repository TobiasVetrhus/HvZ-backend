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

            // Mark the object as modified so it will be updated in the database
            _context.Entry(obj).State = EntityState.Modified;
            _context.SaveChanges();

            return obj;
        }

        public Task UpdatePlayersAsync(int userId, int[] playerIds)
        {
            throw new NotImplementedException();
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

        private async Task<bool> UserExistsAsync(int id)
        {
            return await _context.Users.AnyAsync(u => u.Id == id);
        }

        /*
private async Task<bool> PlayerExistsAsync(int movieId)
{
   return await _context.Players.AnyAsync(p => p.Id == playerId);
}
*/




    }
}
