using AutoMapper;
using HvZ_backend.Data.DTOs.Squads;
using HvZ_backend.Data.Entities;
using HvZ_backend.Data.Exceptions;
using HvZ_backend.Services.Squads;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HvZ_backend.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize]
    public class SquadController : ControllerBase
    {
        private readonly ISquadService _squadService;
        private readonly IMapper _mapper;

        public SquadController(ISquadService squadService, IMapper mapper)
        {
            _squadService = squadService ?? throw new ArgumentNullException(nameof(squadService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Get a list of all squads.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SquadDTO>>> GetSquads()
        {
            var squads = await _squadService.GetAllAsync();
            var squadDTOs = _mapper.Map<IEnumerable<SquadDTO>>(squads);
            return Ok(squadDTOs);
        }
        /// <summary>
        /// Get a squad by ID.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<SquadDTO>> GetSquadById(int id)
        {
            try
            {
                var squad = await _squadService.GetByIdAsync(id);
                return Ok(_mapper.Map<SquadDTO>(squad));
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("{id}/add-player/{playerId}")]
        public async Task<IActionResult> AddPlayerAsync(int id, int playerId)
        {
            try
            {
                await _squadService.AddPlayerAsync(id, playerId);
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

        [HttpPut("{id}/remove-player/{playerId}")]
        public async Task<IActionResult> RemovePlayerAsync(int id, int playerId)
        {
            try
            {
                await _squadService.RemovePlayerAsync(id, playerId);
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
        /// Create a new squad.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<SquadDTO>> AddSquad(SquadPostDTO squadPostDTO)
        {
            var newSquad = await _squadService.AddAsync(_mapper.Map<Squad>(squadPostDTO));
            return CreatedAtAction("GetSquadById", new { id = newSquad.Id }, _mapper.Map<SquadDTO>(newSquad));
        }
        /// <summary>
        /// Update an existing squad.
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSquad(int id, SquadPutDTO squadPutDTO)
        {
            if (id != squadPutDTO.Id)
            {
                return BadRequest();
            }

            try
            {
                var updatedSquad = await _squadService.UpdateAsync(_mapper.Map<Squad>(squadPutDTO));
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }

            return NoContent();
        }
        /// <summary>
        /// Delete a squad by ID.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSquad(int id)
        {
            try
            {
                await _squadService.DeleteByIdAsync(id);
                return NoContent();
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
        /// <summary>
        /// Get all squads within a size range.
        /// </summary>
        [HttpGet("size")]
        public async Task<ActionResult<IEnumerable<SquadDTO>>> GetSquadsBySize(int minSize, int maxSize)
        {
            var squads = await _squadService.GetSquadsBySizeAsync(minSize, maxSize);
            var squadDTOs = _mapper.Map<IEnumerable<SquadDTO>>(squads);
            return Ok(squadDTOs);
        }

    }
}



