using AutoMapper;
using HvZ_backend.Data.DTOs.Kills;
using HvZ_backend.Data.Entities;
using HvZ_backend.Data.Exceptions;
using HvZ_backend.Services.Kills;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace HvZ_backend.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class KillController : ControllerBase
    {
        private readonly IKillService _killService;
        private readonly IMapper _mapper;

        public KillController(IKillService killService, IMapper mapper)
        {
            _killService = killService;
            _mapper = mapper;
        }



        /// <summary>
        /// Retrieve a list of all kills.
        /// </summary>
        /// <returns>An action result containing a list of KillDTO objects.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<KillDTO>>> GetKills()
        {
            var kills = await _killService.GetAllAsync();
            var killDTOs = _mapper.Map<IEnumerable<KillDTO>>(kills);
            return Ok(killDTOs);
        }

        /// <summary>
        /// Retrieve a specific kill by its ID.
        /// </summary>
        /// <param name="id">The unique identifier of the kill.</param>
        /// <returns>An action result containing a KillDTO representing the requested kill.</returns>
        /// <exception cref="EntityNotFoundException">Thrown when the requested kill is not found.</exception>
        [HttpGet("{id}")]
        public async Task<ActionResult<KillDTO>> GetKillById(int id)
        {
            try
            {
                // Retrieve a kill by ID and return it
                return Ok(_mapper.Map<KillDTO>(await _killService.GetByIdAsync(id)));
            }
            catch (EntityNotFoundException ex)
            {
                // Handle the case where the kill with the specified ID was not found
                return NotFound(ex.Message);
            }

        }

        /// <summary>
        /// Add a new kill to the system.
        /// </summary>
        /// <param name="killPostDTO">A DTO representing the kill to be added.</param>
        /// <returns>An action result containing a KillDTO for the newly added kill.</returns>
        [HttpPost]
        public async Task<ActionResult<KillDTO>> AddKill(KillPostDTO killPostDTO)
        {
            var newKill = await _killService.AddAsync(_mapper.Map<Kill>(killPostDTO));

            return CreatedAtAction("GetKillById", new { id = newKill.Id }, _mapper.Map<KillDTO>(newKill));
        }


        /// <summary>
        /// Update an existing kill with the specified ID.
        /// </summary>
        /// <param name="id">The unique identifier of the kill to update.</param>
        /// <param name="killPutDTO">A DTO representing the updated kill information.</param>
        /// <returns>An action result indicating success or failure of the update.</returns>
        /// <exception cref="EntityNotFoundException">Thrown when the requested kill is not found.</exception>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateKill(int id, KillPutDTO killPutDTO)
        {
            if (id != killPutDTO.Id)
            {
                return BadRequest();
            }

            try
            {
                var updatedGame = await _killService.UpdateAsync(_mapper.Map<Kill>(killPutDTO));
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }

            return NoContent();
        }

        /// <summary>
        /// Add a location to a kill.
        /// </summary>
        /// <param name="id">The unique identifier of the kill.</param>
        /// <param name="locationId">The location identifier to add to the kill.</param>
        /// <returns>An action result indicating success or failure of the operation.</returns>
        /// <exception cref="EntityNotFoundException">Thrown when the requested kill or location is not found.</exception>
        /// <exception cref="EntityValidationException">Thrown when the operation is not valid.</exception>
        [HttpPut("{id}/add-location/{locationId}")]
        public async Task<IActionResult> AddLocationAsync(int id, int locationId)
        {
            try
            {
                await _killService.AddLocationToKillAsync(id, locationId);
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
        /// Delete a kill by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the kill to delete.</param>
        /// <returns>An action result indicating success or failure of the deletion.</returns>
        /// <exception cref="EntityNotFoundException">Thrown when the requested kill is not found.</exception>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKill(int id)
        {
            try
            {
                await _killService.DeleteByIdAsync(id);
                return NoContent();
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
