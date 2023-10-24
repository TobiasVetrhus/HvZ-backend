using HvZ_backend.Data.Entities;

namespace HvZ_backend.Services.Missions
{
    /// <summary>
    /// Interface for performing CRUD (Create, Read, Update, Delete) operations on Mission entities.
    /// </summary>
    public interface IMissionService : ICrudService<Mission, int>
    {
        /// <summary>
        /// Associates a location with a mission.
        /// </summary>
        /// <param name="missionId">The ID of the mission to associate with a location.</param>
        /// <param name="locationId">The ID of the location to associate with the mission.</param>
        Task AddLocationToMissionAsync(int missionId, int location);
    }
}