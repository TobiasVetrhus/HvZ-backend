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

        // Add a new mission to the database
        public async Task<Mission> AddAsync(Mission obj)
        {
            await _context.Missions.AddAsync(obj);
            await _context.SaveChangesAsync();
            return obj;
        }

        // Delete a mission by its ID
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

        // Get all missions
        public async Task<IEnumerable<Mission>> GetAllAsync()
        {
            return await _context.Missions.ToListAsync();
        }

        // Get a mission by its ID
        public async Task<Mission> GetByIdAsync(int id)
        {
            if (!await MissionExistsAsync(id))
                throw new EntityNotFoundException(nameof(Mission), id);

            var mission = await _context.Missions.
                Where(m => m.Id == id)
                .FirstAsync();

            return mission;
        }

        // Update an existing mission
        public async Task<Mission> UpdateAsync(Mission obj)
        {
            if (!await MissionExistsAsync(obj.Id))
                throw new EntityNotFoundException(nameof(Mission), obj.Id);

            _context.Entry(obj).State = EntityState.Modified;
            _context.SaveChanges();

            return obj;
        }

        // Add a location to a mission
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

        // Helper method to check if a mission with a given ID exists
        public async Task<bool> MissionExistsAsync(int id)
        {
            return await _context.Missions.AnyAsync(m => m.Id == id);
        }

        // Helper method to check if a location with a given ID exists
        public async Task<bool> LocationExistsAsync(int id)
        {
            return await _context.Locations.AnyAsync(m => m.Id == id);
        }
    }
}
