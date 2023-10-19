using HvZ_backend.Data.Entities;

namespace HvZ_backend.Services.Players
{
    public interface IPlayerService : ICrudService<Player, int>
    {
<<<<<<< HEAD
        Task<Player> UpdateZombieStateAsync(string biteCode);
=======
        Task<Player> UpdateZombieStateAsync(int playerId, bool zombie, string biteCode);
<<<<<<< HEAD
<<<<<<< HEAD
=======
        Task DeletePlayerAsync(int playerId);
>>>>>>> b0ea978 (Rebase stuff)
>>>>>>> 7be16a6 (Rebase stuff)
=======
>>>>>>> 01cb3ac (locationhub)
        Task<Player> GetPlayerByBiteCodeAsync(string biteCode);
        Task<bool> updatePlayerLocationAsync(int playerId, int x, int y);
    }
}

