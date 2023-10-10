using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HvZ_backend.Data.DTOs.Kills;
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

        
        // Get all kills from the database
        public async Task<IEnumerable<Kill>> GetAllAsync()
        {
            // Using Entity Framework to fetch all Kill entities
            return await _context.Kills
               .ToListAsync();
        }

        // Get a kill by its ID
        public async Task<Kill> GetByIdAsync(int id)
        {
            // Using Entity Framework to fetch a specific Kill entity by ID
            var kill = await _context.Kills
                .FirstOrDefaultAsync(k => k.Id == id);

            // Throw an exception if the kill is not found
            if (kill == null)
            {
                throw new EntityNotFoundException("Kill", id);
            }

            return kill;
        }

        // Create a new kill in the database
        public async Task<Kill> CreateKillAsync(KillPostDTO killPostDTO)
        {
       
            var kill = _mapper.Map<Kill>(killPostDTO);

            // Adding the new Kill entity to the DbContext
            _context.Kills.Add(kill);

            await _context.SaveChangesAsync();

            return kill;
        }

        // Update an existing kill in the database
        public async Task<Kill> UpdateKillAsync(int id, KillPutDTO killPutDTO)
        {
            // Fetching the existing Kill entity by ID
            var existingKill = await _context.Kills.FindAsync(id);

   
            if (existingKill == null)
            {
                throw new EntityNotFoundException("Kill", id);
            }

            // Mapping data from the DTO to the existing Kill entity
            _mapper.Map(killPutDTO, existingKill);

            // Saving changes to the database asynchronously
            await _context.SaveChangesAsync();

            return existingKill;
        }

        // Delete a kill by its ID
        public async Task<bool> DeleteKillAsync(int id)
        {
            var existingKill = await _context.Kills.FindAsync(id);

            if (existingKill == null)
            {
                return false;
            }

            // Removing the Kill entity from the DbContext
            _context.Kills.Remove(existingKill);

            await _context.SaveChangesAsync();

            // Return true to indicate successful deletion
            return true;
        }
    }
}
