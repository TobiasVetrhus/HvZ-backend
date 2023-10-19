using HvZ_backend.Data.Entities;

namespace HvZ_backend.Services.Players
{
    public interface IPlayerService : ICrudService<Player, int>
    {
        Task<Player> UpdateZombieStateAsync(string biteCode);
        Task<Player> GetPlayerByBiteCodeAsync(string biteCode);
        Task<bool> updatePlayerLocationAsync(int playerId, int x, int y);
    }
}

