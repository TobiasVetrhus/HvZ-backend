using HvZ_backend.Data.Entities;

namespace HvZ_backend.Services.Games
{
    public class GameService : IGameService
    {
        public Task<IEnumerable<Game>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Game> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<Mission>> GetGameMissionsAsync(int gameId)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<Player>> GetGamePlayersAsync(int gameId)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<Rule>> GetGameRulesAsync(int gameId)
        {
            throw new NotImplementedException();
        }

        public Task<Game> UpdateAsync(Game obj)
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

        public Task<Game> DeleteByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
