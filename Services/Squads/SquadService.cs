using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HvZ_backend.Data.Entities;
using HvZ_backend.Data.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace HvZ_backend.Services.Squads
{
    public class SquadService : ISquadService
    {
        private readonly HvZDbContext _context;
        private readonly IMapper _mapper;

        public SquadService(HvZDbContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        // Get all squads from the database
        public async Task<IEnumerable<Squad>> GetAllAsync()
        {
            return await _context.Squads
                .Include(s => s.Players)
                .ToListAsync();
        }

        // Get a squad by its ID
        public async Task<Squad> GetByIdAsync(int id)
        {
            var squad = await _context.Squads
                .Include(s => s.Players) // Include related Players
                .FirstOrDefaultAsync(s => s.Id == id);

            if (squad == null)
            {
                throw new EntityNotFoundException("Squad", id);
            }

            return squad;
        }

        // Add a new squad to the database
        public async Task<Squad> AddAsync(Squad squad)
        {
            _context.Squads.Add(squad);
            await _context.SaveChangesAsync(); 
            return squad;
        }

        // Update an existing squad in the database
        public async Task<Squad> UpdateAsync(Squad squad)
        {
            if (!_context.Squads.Any(s => s.Id == squad.Id))
            {
               
                throw new EntityNotFoundException("Squad", squad.Id);
            }

            _context.Entry(squad).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
               
                throw;
            }

            return squad;
        }

        // Delete a squad by its ID
        public async Task DeleteByIdAsync(int id)
        {
            var squad = await _context.Squads.FindAsync(id);

            if (squad == null)
            {
                // Throw an exception if the squad with the specified ID is not found for deletion
                throw new EntityNotFoundException("Squad", id);
            }

            _context.Squads.Remove(squad);
            await _context.SaveChangesAsync(); 
        }

        // Update players in a squad by squad ID
        public async Task UpdatePlayersAsync(int squadId, int[] playerIds)
        {
            var squad = await _context.Squads
                .Include(s => s.Players)
                .FirstOrDefaultAsync(s => s.Id == squadId);

            if (squad == null)
            {
                throw new EntityNotFoundException("Squad", squadId);
            }

            squad.Players.Clear();

            foreach (int id in playerIds)
            {
                if (!await PlayerExistsAsync(id))
                {
                    throw new EntityNotFoundException("Player", id);
                }

                var player = await _context.Players.FindAsync(id);
                squad.Players.Add(player);
            }

            await _context.SaveChangesAsync();
        }

        // Get all squads within a size range
        public async Task<ICollection<Squad>> GetSquadsBySizeAsync(int minSize, int maxSize)
        {
            return await _context.Squads
                .Where(s => s.NumberOfMembers >= minSize && s.NumberOfMembers <= maxSize)
                .ToListAsync();
        }

        // Get players in a squad by squad ID
        public async Task<ICollection<Player>> GetSquadPlayersAsync(int squadId)
        {
            var squad = await _context.Squads
                .Include(s => s.Players)
                .FirstOrDefaultAsync(s => s.Id == squadId);

            if (squad == null)
            {
                throw new EntityNotFoundException("Squad", squadId);
            }

            return squad.Players.ToList();
        }

        // Helper method to check if a player with the given ID exists
        private async Task<bool> PlayerExistsAsync(int id)
        {
            return await _context.Players.AnyAsync(p => p.Id == id);
        }
    }
}
