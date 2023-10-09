using HvZ_backend.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace HvZ_backend.Services.PlayerKillRoles
{
    public class PlayerKillRoleService : IPlayerKillRoleService
    {
        private readonly HvZDbContext _context;

        public PlayerKillRoleService(HvZDbContext context)
        {
            _context = context;
        }

        public async Task<PlayerKillRole> AddAsync(PlayerKillRole obj)
        {
            await _context.PlayerKillRole.AddAsync(obj);
            await _context.SaveChangesAsync();
            return obj;
        }

        public async Task DeleteByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<PlayerKillRole>> GetAllAsync()
        {
            return await _context.PlayerKillRole.ToListAsync();
        }

        public async Task<PlayerKillRole> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<PlayerKillRole> UpdateAsync(PlayerKillRole obj)
        {
            throw new NotImplementedException();
        }

        //Helper methods
        public async Task<bool> PlayerKillRoleExistsAsync(int id)
        {
            return await _context.PlayerKillRole.AnyAsync(p => p.Id == id);
        }
    }
}
