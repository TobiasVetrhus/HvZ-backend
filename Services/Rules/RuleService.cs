using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HvZ_backend.Data.Entities;
using HvZ_backend.Data.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace HvZ_backend.Services.Rules
{
    public class RuleService : IRuleService
    {
        private readonly HvZDbContext _context;
        private readonly IMapper _mapper;

        public RuleService(HvZDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // Get all rules from the database
        public async Task<IEnumerable<Rule>> GetAllAsync()
        {
            return await _context.Rules
               .Include(r => r.Games)
               .ToListAsync();
        }

        // Get a rule by its ID with associated GameIds
        public async Task<Rule> GetByIdAsync(int id)
        {
            var rule = await _context.Rules
                .Include(r => r.Games)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (rule == null)
            {
                throw new EntityNotFoundException("Rule", id);
            }

            // Extract the GameIds from the related Games entities
            var gameIds = rule.Games.Select(game => game.Id).ToList();
            return rule;
        }

        // Add a new rule to the database
        public async Task<Rule> AddAsync(Rule rule)
        {
            _context.Rules.Add(rule);
            await _context.SaveChangesAsync(); // Save changes asynchronously
            return rule;
        }

        // Update an existing rule in the database
        public async Task<Rule> UpdateAsync(Rule rule)
        {
            if (!_context.Rules.Any(r => r.Id == rule.Id))
            {
                // Throw an exception if the rule with the specified ID is not found for updating
                throw new EntityNotFoundException("Rule", rule.Id);
            }

            _context.Entry(rule).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync(); 
            }
            catch (DbUpdateConcurrencyException)
            {
                // Handle concurrency exception if needed
                throw;
            }

            return rule;
        }

        // Delete a rule by its ID
        public async Task DeleteByIdAsync(int id)
        {
            var rule = await _context.Rules.FindAsync(id);

            if (rule == null)
            {
                // Throw an exception if the rule with the specified ID is not found for deletion
                throw new EntityNotFoundException("Rule", id);
            }

            _context.Rules.Remove(rule);
            await _context.SaveChangesAsync(); // Save changes asynchronously
        }
    }
}
