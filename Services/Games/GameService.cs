﻿using HvZ_backend.Data.Entities;
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

        // Adds a new game to the database
        public async Task<Game> AddAsync(Game obj)
        {
            await _context.Games.AddAsync(obj);
            await _context.SaveChangesAsync();
            return obj;
        }

        // Deletes a game by its ID, including associated missions, players, rules, and conversations
        public async Task DeleteByIdAsync(int id)
        {
            if (!await GameExistsAsync(id))
                throw new EntityNotFoundException(nameof(Game), id);

            var game = await _context.Games
                .Where(g => g.Id == id)
                .Include(g => g.Missions)
                .Include(g => g.Players)
                .Include(g => g.Rules)
                .Include(g => g.Conversations)
                .FirstAsync();

            game.Missions.Clear();
            game.Players.Clear();
            game.Rules.Clear();
            game.Conversations.Clear();

            _context.Games.Remove(game);

            await _context.SaveChangesAsync();
        }

        // Gets games by their game status
        public async Task<ICollection<Game>> GetGamesByStateAsync(GameStatus gamestatus)
        {
            return await _context.Games
                .Include(g => g.Missions)
                .Include(g => g.Players)
                .Include(g => g.Rules)
                .Include(g => g.Conversations)
                .Where(g => g.GameState == gamestatus)
                .ToListAsync();
        }

        // Gets all games, including players, rules, missions, and conversations
        public async Task<IEnumerable<Game>> GetAllAsync()
        {
            return await _context.Games
                .Include(g => g.Players)
                .Include(g => g.Rules)
                .Include(g => g.Missions)
                .Include(g => g.Conversations)
                .ToListAsync();
        }

        // Gets a game by its ID, including players, rules, missions, and conversations
        public async Task<Game> GetByIdAsync(int id)
        {
            if (!await GameExistsAsync(id))
                throw new EntityNotFoundException(nameof(Game), id);

            var game = await _context.Games
                .Where(g => g.Id == id)
                .Include(g => g.Players)
                .Include(g => g.Rules)
                .Include(g => g.Missions)
                .Include(g => g.Conversations)
                .FirstAsync();

            return game;
        }

        // Gets conversations associated with a game by its ID
        public async Task<ICollection<Conversation>> GetGameConversationsAsync(int gameId)
        {
            throw new NotImplementedException();
        }

        // Gets missions associated with a game by its ID
        public async Task<ICollection<Mission>> GetGameMissionsAsync(int gameId)
        {
            throw new NotImplementedException();
        }

        // Gets players associated with a game by its ID
        public async Task<ICollection<Player>> GetGamePlayersAsync(int gameId)
        {
            throw new NotImplementedException();
        }

        // Gets rules associated with a game by its ID
        public async Task<ICollection<Rule>> GetGameRulesAsync(int gameId)
        {
            throw new NotImplementedException();
        }

        // Updates an existing game in the database
        public async Task<Game> UpdateAsync(Game obj)
        {
            if (!await GameExistsAsync(obj.Id))
                throw new EntityNotFoundException(nameof(Game), obj.Id);

            _context.Entry(obj).State = EntityState.Modified;
            _context.SaveChanges();

            return obj;
        }

        // Adds a rule to a game by their respective IDs
        public async Task AddRuleAsync(int gameId, int ruleId)
        {
            if (!await GameExistsAsync(gameId))
                throw new EntityNotFoundException(nameof(Game), gameId);

            var game = await _context.Games
                .Include(g => g.Rules)
                .FirstOrDefaultAsync(g => g.Id == gameId);


            if (!await RuleExistsAsync(ruleId))
                throw new EntityNotFoundException(nameof(Rule), ruleId);

            var rule = await _context.Rules.FindAsync(ruleId);

            game.Rules.Add(rule);

            await _context.SaveChangesAsync();
        }

        // Adds a player to a game by their respective IDs
        public async Task AddPlayerAsync(int gameId, int playerId)
        {
            if (!await GameExistsAsync(gameId))
                throw new EntityNotFoundException(nameof(Game), gameId);

            var game = await _context.Games
                .Include(g => g.Players)
                .FirstOrDefaultAsync(g => g.Id == gameId);


            if (!await PlayerExistsAsync(playerId))
                throw new EntityNotFoundException(nameof(Player), playerId);

            var player = await _context.Players.FindAsync(playerId);

            game.Players.Add(player);

            await _context.SaveChangesAsync();
        }

        // Adds a mission to a game by their respective IDs
        public async Task AddMissionAsync(int gameId, int missionId)
        {
            if (!await GameExistsAsync(gameId))
                throw new EntityNotFoundException(nameof(Game), gameId);

            var game = await _context.Games
                .Include(g => g.Missions)
                .FirstOrDefaultAsync(g => g.Id == gameId);


            if (!await MissionExistsAsync(missionId))
                throw new EntityNotFoundException(nameof(Mission), missionId);

            var mission = await _context.Missions.FindAsync(missionId);

            game.Missions.Add(mission);

            await _context.SaveChangesAsync();
        }

        // Adds a conversation to a game by their respective IDs
        public async Task AddConversationAsync(int gameId, int conversationId)
        {
            if (!await GameExistsAsync(gameId))
                throw new EntityNotFoundException(nameof(Game), gameId);

            var game = await _context.Games
                .Include(g => g.Conversations)
                .FirstOrDefaultAsync(g => g.Id == gameId);


            if (!await RuleExistsAsync(conversationId))
                throw new EntityNotFoundException(nameof(Conversation), conversationId);

            var conversation = await _context.Conversations.FindAsync(conversationId);

            game.Conversations.Add(conversation);

            await _context.SaveChangesAsync();
        }

        // Updates conversations associated with a game by their IDs
        public async Task UpdateConversationsAsync(int gameId, int[] conversations)
        {
            var game = await _context.Games
                .Include(g => g.Conversations)
                .FirstOrDefaultAsync(g => g.Id == gameId);

            if (!await GameExistsAsync(gameId))
                throw new EntityNotFoundException(nameof(Game), gameId);

            game.Conversations.Clear();

            foreach (int id in conversations)
            {
                if (!await ConversationExistsAsync(id))
                    throw new EntityNotFoundException(nameof(Conversation), id);

                var conversation = await _context.Conversations.FindAsync(id);
                game.Conversations.Add(conversation);
            }

            await _context.SaveChangesAsync();
        }

        // Updates missions associated with a game by their IDs
        public async Task UpdateMissionsAsync(int gameId, int[] missionIds)
        {
            var game = await _context.Games
                .Include(g => g.Missions)
                .FirstOrDefaultAsync(g => g.Id == gameId);

            if (!await GameExistsAsync(gameId))
                throw new EntityNotFoundException(nameof(Game), gameId);

            game.Missions.Clear();

            foreach (int id in missionIds)
            {
                if (!await MissionExistsAsync(id))
                    throw new EntityNotFoundException(nameof(Mission), id);

                var mission = await _context.Missions.FindAsync(id);
                game.Missions.Add(mission);
            }

            await _context.SaveChangesAsync();
        }

        // Updates players associated with a game by their IDs
        public async Task UpdatePlayersAsync(int gameId, int[] playerIds)
        {
            var game = await _context.Games
                .Include(g => g.Players)
                .FirstOrDefaultAsync(g => g.Id == gameId);

            if (!await GameExistsAsync(gameId))
                throw new EntityNotFoundException(nameof(Game), gameId);

            game.Players.Clear();

            foreach (int id in playerIds)
            {
                if (!await PlayerExistsAsync(id))
                    throw new EntityNotFoundException(nameof(Player), id);

                var player = await _context.Players.FindAsync(id);
                game.Players.Add(player);
            }

            await _context.SaveChangesAsync();
        }

        // Updates rules associated with a game by their IDs
        public async Task UpdateRulesAsync(int gameId, int[] ruleIds)
        {
            var game = await _context.Games
                .Include(g => g.Rules)
                .FirstOrDefaultAsync(g => g.Id == gameId);

            if (!await GameExistsAsync(gameId))
                throw new EntityNotFoundException(nameof(Game), gameId);

            game.Rules.Clear();

            foreach (int id in ruleIds)
            {
                if (!await RuleExistsAsync(id))
                    throw new EntityNotFoundException(nameof(Rule), id);

                var rule = await _context.Rules.FindAsync(id);
                game.Rules.Add(rule);
            }

            await _context.SaveChangesAsync();
        }

        // Removes a rule from a game by their respective IDs
        public async Task RemoveRuleAsync(int gameId, int ruleId)
        {
            var game = await _context.Games
                .Include(g => g.Rules)
                .FirstOrDefaultAsync(g => g.Id == gameId);

            if (!await GameExistsAsync(gameId))
                throw new EntityNotFoundException(nameof(Game), gameId);

            var ruleToRemove = game.Rules.FirstOrDefault(r => r.Id == ruleId);

            if (!await RuleExistsAsync(ruleId))
                throw new EntityNotFoundException(nameof(Rule), ruleId);

            // Remove the rule from the game's Rules collection
            game.Rules.Remove(ruleToRemove);

            await _context.SaveChangesAsync();
        }

        // Removes a mission from a game by their respective IDs
        public async Task RemoveMissionAsync(int gameId, int missionId)
        {
            var game = await _context.Games
                .Include(g => g.Missions)
                .FirstOrDefaultAsync(g => g.Id == gameId);

            if (!await GameExistsAsync(gameId))
                throw new EntityNotFoundException(nameof(Game), gameId);

            var missionToRemove = _context.Missions.FirstOrDefault(m => m.Id == missionId);

            if (!await MissionExistsAsync(missionId))
                throw new EntityNotFoundException(nameof(Mission), missionId);

            missionToRemove.GameId = null;

            await _context.SaveChangesAsync();
        }

        // Removes a conversation from a game by their respective IDs
        public async Task RemoveConversationAsync(int gameId, int conversationId)
        {
            var game = await _context.Games
                .Include(g => g.Conversations)
                .FirstOrDefaultAsync(g => g.Id == gameId);

            if (!await GameExistsAsync(gameId))
                throw new EntityNotFoundException(nameof(Game), gameId);

            var conversationToRemove = _context.Conversations.FirstOrDefault(c => c.Id == conversationId);

            if (!await ConversationExistsAsync(conversationId))
                throw new EntityNotFoundException(nameof(Conversation), conversationId);

            conversationToRemove.GameId = null;

            await _context.SaveChangesAsync();
        }

        // Removes a player from a game by their respective IDs
        public async Task RemovePlayerAsync(int gameId, int playerId)
        {
            var game = await _context.Games
                .Include(g => g.Players)
                .FirstOrDefaultAsync(g => g.Id == gameId);

            if (!await GameExistsAsync(gameId))
                throw new EntityNotFoundException(nameof(Game), gameId);

            var playerToRemove = _context.Players.FirstOrDefault(p => p.Id == playerId);

            if (!await PlayerExistsAsync(playerId))
                throw new EntityNotFoundException(nameof(Player), playerId);

            playerToRemove.GameId = null;

            await _context.SaveChangesAsync();
        }

        // Helper methods to check entity existence
        public async Task<bool> GameExistsAsync(int id)
        {
            return await _context.Games.AnyAsync(g => g.Id == id);
        }

        public async Task<bool> ConversationExistsAsync(int id)
        {
            return await _context.Conversations.AnyAsync(c => c.Id == id);
        }

        public async Task<bool> RuleExistsAsync(int id)
        {
            return await _context.Rules.AnyAsync(r => r.Id == id);
        }

        public async Task<bool> PlayerExistsAsync(int id)
        {
            return await _context.Players.AnyAsync(p => p.Id == id);
        }

        public async Task<bool> MissionExistsAsync(int id)
        {
            return await _context.Missions.AnyAsync(m => m.Id == id);
        }
    }
}
