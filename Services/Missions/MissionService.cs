using HvZ_backend.Data.Entities;
using HvZ_backend.Data.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace HvZ_backend.Services.Missions
{
    public class MissionService : IMissionService
    {
        private readonly HvZDbContext _context;

        public MissionService(HvZDbContext context)
        {
            _context = context;
        }

        public async Task<Mission> AddAsync(Mission obj)
        {
            await _context.Missions.AddAsync(obj);
            await _context.SaveChangesAsync();
            return obj;
        }

        public async Task DeleteByIdAsync(int id)
        {
            if (!await MissionExistsAsync(id))
                throw new EntityNotFoundException(nameof(Mission), id);

            var mission = await _context.Missions
                .Where(m => m.Id == id)
                .FirstAsync();

            _context.Missions.Remove(mission);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Mission>> GetAllAsync()
        {
            return await _context.Missions.ToListAsync();
        }

        public async Task<Mission> GetByIdAsync(int id)
        {
            if (!await MissionExistsAsync(id))
                throw new EntityNotFoundException(nameof(Mission), id);

            var mission = await _context.Missions.Where(m => m.Id == id).FirstAsync();

            return mission;
        }

        public async Task<Mission> UpdateAsync(Mission obj)
        {
            if (!await MissionExistsAsync(obj.Id))
                throw new EntityNotFoundException(nameof(Mission), obj.Id);

            _context.Entry(obj).State = EntityState.Modified;
            _context.SaveChanges();

            return obj;
        }

        //Helper methods
        public async Task<bool> MissionExistsAsync(int id)
        {
            return await _context.Missions.AnyAsync(m => m.Id == id);
        }
    }
}
