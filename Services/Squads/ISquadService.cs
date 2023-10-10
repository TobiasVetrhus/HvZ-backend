using HvZ_backend.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HvZ_backend.Services.Squads
{
    public interface ISquadService
    {
        Task<IEnumerable<Squad>> GetAllAsync();
        Task<Squad> GetByIdAsync(int id);
        Task<Squad> AddAsync(Squad squad);
        Task<Squad> UpdateAsync(Squad squad);
        Task DeleteByIdAsync(int id);
        Task UpdatePlayersAsync(int squadId, int[] playerIds);
        Task<ICollection<Squad>> GetSquadsBySizeAsync(int minSize, int maxSize);
        Task<ICollection<Player>> GetSquadPlayersAsync(int squadId);
    }
}
