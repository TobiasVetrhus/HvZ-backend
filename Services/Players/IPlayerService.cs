using HvZ_backend.Data.Entities;

namespace HvZ_backend.Services.Players
{
    public interface IPlayerService
    {
        Task<IEnumerable<Player>> GetAllPlayersAsync();
        Task<Player> GetPlayerByIdAsync(int playerId);
        Task<Player> CreatePlayerAsync(Player player);
        Task<Player> UpdatePlayerAsync(Player player);
        Task<Player> UpdateZombieStateAsync(int playerId, bool zombie, string biteCode);
        Task DeletePlayerAsync(int playerId);
        Task<Player> GetPlayerByBiteCodeAsync(string biteCode);
    }
}

