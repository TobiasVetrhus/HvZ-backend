using HvZ_backend.Data.Entities;
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
            throw new NotImplementedException();
            /*
            await _context.Users.AddAsync(obj);
            await _context.SaveChangesAsync();
            return obj;
            */
        }
        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users.Include(u => u.Players).ToListAsync();
        }

        public Task<User> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<User> UpdateAsync(User obj)
        {
            throw new NotImplementedException();
        }

        public Task UpdatePlayersAsync(int userId, int[] playerIds)
        {
            throw new NotImplementedException();
        }

        public Task<User> DeleteByIdAsync(int id)
        {
            throw new NotImplementedException();
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
