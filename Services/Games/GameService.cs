using HvZ_backend.Data.Entities;
using HvZ_backend.Data.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace HvZ_backend.Services.Games
{
    public class GameService : IGameService
    {
        //Dependency Injection
        private readonly HvZDbContext _context;

        public GameService(HvZDbContext context)
        {
            _context = context;
        }

        public async Task<Game> AddAsync(Game obj)
        {
            await _context.Games.AddAsync(obj);
            await _context.SaveChangesAsync();
            return obj;
        }

        public async Task DeleteByIdAsync(int id)
        {
            if (!await GameExistsAsync(id))
                throw new EntityNotFoundException(nameof(Game), id);

            var game = await _context.Games
                .Where(g => g.Id == id)
                .FirstAsync();

            _context.Games.Remove(game);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Game>> GetAllAsync()
        {
            return await _context.Games
                .Include(g => g.Players)
                .Include(g => g.Rules)
                .Include(g => g.Missions)
                .Include(g => g.Conversations)
                .ToListAsync();
        }

        public async Task<Game> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<Conversation>> GetGameConversationsAsync(int gameId)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<Mission>> GetGameMissionsAsync(int gameId)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<Player>> GetGamePlayersAsync(int gameId)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<Rule>> GetGameRulesAsync(int gameId)
        {
            throw new NotImplementedException();
        }

        public async Task<Game> UpdateAsync(Game obj)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateConversationsAsync(int gameId, int[] conversations)
        {
            throw new NotImplementedException();
        }

        public Task UpdateMissionsAsync(int gameId, int[] missionIds)
        {
            throw new NotImplementedException();
        }

        public Task UpdatePlayersAsync(int gameId, int[] playerIds)
        {
            throw new NotImplementedException();
        }

        public Task UpdateRulesAsync(int gameId, int[] ruleIds)
        {
            throw new NotImplementedException();
        }

        //Helper methods
        public async Task<bool> GameExistsAsync(int id)
        {
            return await _context.Games.AnyAsync(g => g.Id == id);
        }
    }
}
