using System.Collections.Generic;
using System.Threading.Tasks;
using HvZ_backend.Data.DTOs.Kills;
using HvZ_backend.Data.Entities;

namespace HvZ_backend.Services.Kills
{
    public interface IKillService
    {
        Task<IEnumerable<Kill>> GetAllAsync();
        Task<Kill> GetByIdAsync(int id);
        Task<Kill> CreateKillAsync(KillPostDTO killPostDTO);
        Task<Kill> UpdateKillAsync(int id, KillPutDTO killPutDTO);
        Task<bool> DeleteKillAsync(int id);
    }
}


