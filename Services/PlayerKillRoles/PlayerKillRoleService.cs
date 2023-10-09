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

        public Task<PlayerKillRole> AddAsync(PlayerKillRole obj)
        {
            throw new NotImplementedException();
        }

        public Task DeleteByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<PlayerKillRole>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<PlayerKillRole> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<PlayerKillRole> UpdateAsync(PlayerKillRole obj)
        {
            throw new NotImplementedException();
        }

        //Helper methods
        public async Task<bool> PlayerKillRoleExistsAsync(int id)
        {
            return await _context.PlayerKillRoles.AnyAsync(l => l.Id == id);
        }
    }
}
