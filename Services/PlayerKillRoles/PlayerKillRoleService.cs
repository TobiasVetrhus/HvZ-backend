using HvZ_backend.Data.Entities;
using HvZ_backend.Data.Exceptions;
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
            if (!await PlayerKillRoleExistsAsync(id))
                throw new EntityNotFoundException(nameof(PlayerKillRole), id);

            var playerkillrole = await _context.PlayerKillRole
                .Where(p => p.Id == id)
                .FirstAsync();

            return playerkillrole;
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
