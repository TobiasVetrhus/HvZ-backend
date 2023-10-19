using HvZ_backend.Data.Entities;

namespace HvZ_backend.Services.Players
{
    public interface IPlayerService : ICrudService<Player, int>
    {
        Task<Player> UpdateZombieStateAsync(int playerId, bool zombie, string biteCode);
        Task<Player> GetPlayerByBiteCodeAsync(string biteCode);
    }
}

