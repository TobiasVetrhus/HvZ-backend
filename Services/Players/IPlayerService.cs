using HvZ_backend.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HvZ_backend.Services.Players
{
    public interface IPlayerService
    {
        Task<IEnumerable<Player>> GetAllPlayersAsync();
        Task<Player> GetPlayerByIdAsync(int playerId);
        Task<Player> CreatePlayerAsync(Player player);
        Task<Player> UpdatePlayerAsync(Player player);
        Task DeletePlayerAsync(int playerId);
    }
}

