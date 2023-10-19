using HvZ_backend.Data.Entities;

namespace HvZ_backend.Services.Kills
{
    public interface IKillService : ICrudService<Kill, int>
    {
        Task AddLocationToKillAsync(int killId, int locationId);
    }
}


