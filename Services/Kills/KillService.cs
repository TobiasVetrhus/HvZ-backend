using AutoMapper;
using HvZ_backend.Data.Entities;
using HvZ_backend.Data.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace HvZ_backend.Services.Kills
{
    public class KillService : IKillService
    {
        private readonly HvZDbContext _context;
        private readonly IMapper _mapper;

        public KillService(HvZDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // Get all kills from the database with related PlayerKillRoles included
        public async Task<IEnumerable<Kill>> GetAllAsync()
        {
            return await _context.Kills
               .Include(k => k.Player)
                    .ThenInclude(p => p.Location)
               .ToListAsync();
        }

        // Get a kill by its ID with related PlayerKillRoles included
        public async Task<Kill> GetByIdAsync(int id)
        {
            if (!await KillExistsAsync(id))
                throw new EntityNotFoundException(nameof(Kill), id);

            var kill = await _context.Kills
                .Where(k => k.Id == id)
                .Include(k => k.Player)
                    .ThenInclude(p => p.Location)
                .FirstOrDefaultAsync();

            return kill;
        }

        // Create a new kill in the database
        public async Task<Kill> AddAsync(Kill obj)
        {
            // Adding the new Kill entity to the DbContext
            await _context.Kills.AddAsync(obj);
            await _context.SaveChangesAsync();
            return obj;
        }

        // Update an existing kill in the database
        public async Task<Kill> UpdateAsync(Kill obj)
        {
            // Fetching the existing Kill entity by ID
            var existingKill = await _context.Kills.FindAsync(obj.Id);

            if (existingKill == null)
            {
                throw new EntityNotFoundException("Kill", obj.Id);
            }

            // Saving changes to the database asynchronously
            await _context.SaveChangesAsync();

            return existingKill;
        }

        // Delete a kill by its ID
        public async Task DeleteByIdAsync(int id)
        {
            if (!await KillExistsAsync(id))
                throw new EntityNotFoundException(nameof(Kill), id);

            var kill = await _context.Kills.FindAsync(id);
            _context.Kills.Remove(kill);
            await _context.SaveChangesAsync();
        }

        // Add a location to a kill
        public async Task AddLocationToKillAsync(int killId, int locationId)
        {
            if (!await KillExistsAsync(killId))
                throw new EntityNotFoundException(nameof(Kill), killId);

            if (!await LocationExistsAsync(locationId))
                throw new EntityNotFoundException(nameof(Location), locationId);

            var kill = await _context.Kills.FirstOrDefaultAsync(k => k.Id == killId);
            var location = await _context.Locations.FindAsync(locationId);

            if (kill != null && location != null)
            {
                kill.Location = location;

                // Save the changes to the database.
                await _context.SaveChangesAsync();
            }
        }

        // Check if a kill with a given ID exists
        public async Task<bool> KillExistsAsync(int id)
        {
            return await _context.Kills.AnyAsync(m => m.Id == id);
        }

        // Check if a location with a given ID exists
        public async Task<bool> LocationExistsAsync(int id)
        {
            return await _context.Locations.AnyAsync(m => m.Id == id);
        }


    }
}
