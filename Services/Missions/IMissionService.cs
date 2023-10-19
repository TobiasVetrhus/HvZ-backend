using HvZ_backend.Data.Entities;

namespace HvZ_backend.Services.Missions
{
    public interface IMissionService : ICrudService<Mission, int>
    {
        Task AddLocationToMissionAsync(int missionId, int location);
    }
}