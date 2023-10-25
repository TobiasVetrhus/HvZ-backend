using AutoMapper;
using HvZ_backend.Data.DTOs.Missions;
using HvZ_backend.Data.Entities;
using HvZ_backend.Data.Exceptions;
using HvZ_backend.Services.Missions;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace HvZ_backend.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class MissionController : ControllerBase
    {
        private readonly IMissionService _missionService;
        private readonly IMapper _mapper;

        public MissionController(IMissionService missionService, IMapper mapper)
        {
            _missionService = missionService;
            _mapper = mapper;
        }

        /// <summary>
        /// Retrieve a list of all missions.
        /// </summary>
        /// <returns>An action result containing a list of MissionDTO objects.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MissionDTO>>> GetMissions()
        {
            var missions = await _missionService.GetAllAsync();
            var locationDTOs = _mapper.Map<IEnumerable<MissionDTO>>(missions);
            return Ok(locationDTOs);
        }

        /// <summary>
        /// Retrieve a specific mission by its ID.
        /// </summary>
        /// <param name="id">The unique identifier of the mission.</param>
        /// <returns>An action result containing a MissionDTO representing the requested mission.</returns>
        /// <exception cref="EntityNotFoundException">Thrown when the requested mission is not found.</exception>
        [HttpGet("{id}")]
        public async Task<ActionResult<MissionDTO>> GetMissionById(int id)
        {
            try
            {
                var mission = await _missionService.GetByIdAsync(id);
                return Ok(_mapper.Map<MissionDTO>(mission));
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Create and post a new mission.
        /// </summary>
        /// <param name="mission">A DTO representing the mission to be added.</param>
        /// <returns>An action result containing a MissionDTO for the newly added mission.</returns>
        [HttpPost]
        public async Task<ActionResult<MissionDTO>> AddMission(MissionPostDTO mission)
        {
            var newMission = await _missionService.AddAsync(_mapper.Map<Mission>(mission));

            return CreatedAtAction("GetMissionById", new { id = newMission.Id }, _mapper.Map<MissionDTO>(newMission));
        }

        /// <summary>
        /// Add a location to a mission by its ID.
        /// </summary>
        /// <param name="id">The unique identifier of the mission.</param>
        /// <param name="locationId">The unique identifier of the location to add.</param>
        /// <returns>An action result indicating success or failure of the operation.</returns>
        /// <exception cref="EntityNotFoundException">Thrown when the mission or location is not found.</exception>
        /// <exception cref="EntityValidationException">Thrown when there is a validation issue.</exception>
        [HttpPut("{id}/add-location/{locationId}")]
        public async Task<IActionResult> AddMissionAsync(int id, int locationId)
        {
            try
            {
                await _missionService.AddLocationToMissionAsync(id, locationId);
                return NoContent();
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (EntityValidationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Update an existing mission with the specified ID.
        /// </summary>
        /// <param name="id">The unique identifier of the mission to update.</param>
        /// <param name="mission">A DTO representing the updated mission information.</param>
        /// <returns>An action result indicating success or failure of the update.</returns>
        /// <exception cref="EntityNotFoundException">Thrown when the requested mission is not found.</exception>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMission(int id, MissionPutDTO mission)
        {
            if (id != mission.Id)
            {
                return BadRequest();
            }

            try
            {
                var updatedMission = await _missionService.UpdateAsync(_mapper.Map<Mission>(mission));
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }

            return NoContent();
        }

        /// <summary>
        /// Delete a mission by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the mission to delete.</param>
        /// <returns>An action result indicating success or failure of the deletion.</returns>
        /// <exception cref="EntityNotFoundException">Thrown when the requested mission is not found.</exception>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMission(int id)
        {
            try
            {
                await _missionService.DeleteByIdAsync(id);
                return NoContent();
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }


    }
}
