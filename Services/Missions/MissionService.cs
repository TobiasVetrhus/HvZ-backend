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

            var mission = await _context.Missions.
                Where(m => m.Id == id)
                .FirstAsync();

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


        public async Task AddLocationToMissionAsync(int missionId, int locationId)
        {
            if (!await MissionExistsAsync(missionId))
                throw new EntityNotFoundException(nameof(Mission), missionId);

            if (!await LocationExistsAsync(locationId))
                throw new EntityNotFoundException(nameof(Location), locationId);

            var mission = await _context.Missions.FirstOrDefaultAsync(m => m.Id == missionId);
            var location = await _context.Locations.FindAsync(locationId);

            if (mission != null && location != null)
            {
                // Set the Location property in Mission to associate them.
                mission.Location = location;

                // Save the changes to the database.
                await _context.SaveChangesAsync();
            }
        }





        //Helper methods
        public async Task<bool> MissionExistsAsync(int id)
        {
            return await _context.Missions.AnyAsync(m => m.Id == id);
        }

        public async Task<bool> LocationExistsAsync(int id)
        {
            return await _context.Locations.AnyAsync(m => m.Id == id);
        }
    }
}
