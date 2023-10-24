using HvZ_backend.Data.Entities;
using HvZ_backend.Data.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace HvZ_backend.Services.Locations
{
    public class LocationService : ILocationService
    {
        private readonly HvZDbContext _context;

        public LocationService(HvZDbContext context)
        {
            _context = context;
        }

        // Add a new location to the database
        public async Task<Location> AddAsync(Location obj)
        {
            await _context.Locations.AddAsync(obj);
            await _context.SaveChangesAsync();
            return obj;
        }

        // Delete a location by its ID
        public async Task DeleteByIdAsync(int id)
        {
            if (!await LocationExistsAsync(id))
                throw new EntityNotFoundException(nameof(Location), id);

            var location = await _context.Locations
                .Where(l => l.Id == id)
                .FirstAsync();

            _context.Locations.Remove(location);
            await _context.SaveChangesAsync();
        }

        // Get all locations
        public async Task<IEnumerable<Location>> GetAllAsync()
        {
            return await _context.Locations.ToListAsync();
        }

        // Get a location by its ID
        public async Task<Location> GetByIdAsync(int id)
        {
            if (!await LocationExistsAsync(id))
                throw new EntityNotFoundException(nameof(Location), id);

            var location = await _context.Locations.Where(l => l.Id == id).FirstAsync();

            return location;
        }

        // Update an existing location
        public async Task<Location> UpdateAsync(Location obj)
        {
            if (!await LocationExistsAsync(obj.Id))
                throw new EntityNotFoundException(nameof(Location), obj.Id);

            _context.Entry(obj).State = EntityState.Modified;
            _context.SaveChanges();

            return obj;
        }

        // Helper method to check if a location with a given ID exists
        public async Task<bool> LocationExistsAsync(int id)
        {
            return await _context.Locations.AnyAsync(l => l.Id == id);
        }
    }
}
