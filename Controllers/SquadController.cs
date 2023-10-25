using AutoMapper;
using HvZ_backend.Data.DTOs.Games;
using HvZ_backend.Data.DTOs.Squads;
using HvZ_backend.Data.Entities;
using HvZ_backend.Data.Exceptions;
using HvZ_backend.Services.Squads;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace HvZ_backend.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Produces(MediaTypeNames.Application.Json)]
    [ApiConventionType(typeof(DefaultApiConventions))]
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
        /// <returns>An action result containing a list of SquadDTO objects representing all squads.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SquadDTO>>> GetSquads()
        {
            var squads = await _squadService.GetAllAsync();
            var squadDTOs = _mapper.Map<IEnumerable<SquadDTO>>(squads);
            return Ok(squadDTOs);
        }

        /// <summary>
        /// Get a list of squads filtered by a specific game ID.
        /// </summary>
        /// <param name="gameId">The unique identifier of the game used as a filter.</param>
        /// <returns>An action result containing a list of SquadDTO objects filtered by the game ID.</returns>
        [HttpGet("filterbygameid/{gameId}")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<GameDTO>>> GetSquadsByGameId(int gameId)
        {
            var squads = await _squadService.GetSquadsByGameAsync(gameId);
            var squadDTOs = _mapper.Map<IEnumerable<SquadDTO>>(squads);
            return Ok(squadDTOs);
        }


        /// <summary>
        /// Get a squad by ID.
        /// </summary>
        /// <param name="id">The unique identifier of the squad to retrieve.</param>
        /// <returns>An action result containing a SquadDTO representing the requested squad.</returns>
        /// <exception cref="EntityNotFoundException">Thrown when the requested squad is not found.</exception>
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

        /// <summary>
        /// Add a player to a squad.
        /// </summary>
        /// <param name="id">The unique identifier of the squad.</param>
        /// <param name="playerId">The player identifier to add.</param>
        /// <returns>An action result indicating success or failure.</returns>
        /// <exception cref="EntityNotFoundException">Thrown when the requested squad or player is not found.</exception>
        /// <exception cref="EntityValidationException">Thrown when the player addition is not valid.</exception>
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

        /// <summary>
        /// Remove a player from a squad.
        /// </summary>
        /// <param name="id">The unique identifier of the squad.</param>
        /// <param name="playerId">The player identifier to remove.</param>
        /// <returns>An action result indicating success or failure.</returns>
        /// <exception cref="EntityNotFoundException">Thrown when the requested squad or player is not found.</exception>
        /// <exception cref="EntityValidationException">Thrown when the player removal is not valid.</exception>
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
        /// Add a game to a squad.
        /// </summary>
        /// <param name="id">The unique identifier of the squad.</param>
        /// <param name="gameId">The game identifier to add.</param>
        /// <returns>An action result indicating success or failure.</returns>
        /// <exception cref="EntityNotFoundException">Thrown when the requested squad or game is not found.</exception>
        /// <exception cref="EntityValidationException">Thrown when the game addition is not valid.</exception>
        [HttpPut("{id}/add-game/{gameId}")]
        public async Task<IActionResult> AddGameAsync(int id, int gameId)
        {
            try
            {
                await _squadService.AddGameToSquadAsync(id, gameId);
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
        /// <param name="squadPostDTO">The SquadPostDTO representing the squad to be created.</param>
        /// <returns>An action result containing a SquadDTO representing the newly created squad.</returns>
        [HttpPost]
        public async Task<ActionResult<SquadDTO>> AddSquad(SquadPostDTO squadPostDTO)
        {
            var newSquad = await _squadService.AddAsync(_mapper.Map<Squad>(squadPostDTO));
            return CreatedAtAction("GetSquadById", new { id = newSquad.Id }, _mapper.Map<SquadDTO>(newSquad));
        }

        /// <summary>
        /// Update an existing squad.
        /// </summary>
        /// <param name="id">The unique identifier of the squad to update.</param>
        /// <param name="squadPutDTO">The SquadPutDTO containing the updated squad information.</param>
        /// <returns>An action result indicating success or failure of the update operation.</returns>
        /// <exception cref="EntityNotFoundException">Thrown when the requested squad is not found.</exception>
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
        /// <param name="id">The unique identifier of the squad to delete.</param>
        /// <returns>An action result indicating success or failure of the delete operation.</returns>
        /// <exception cref="EntityNotFoundException">Thrown when the requested squad is not found.</exception>
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
        /// <param name="minSize">The minimum size of squads to filter by.</param>
        /// <param name="maxSize">The maximum size of squads to filter by.</param>
        /// <returns>An action result containing a list of SquadDTO objects representing squads within the specified size range.</returns>
        [HttpGet("size")]
        public async Task<ActionResult<IEnumerable<SquadDTO>>> GetSquadsBySize(int minSize, int maxSize)
        {
            var squads = await _squadService.GetSquadsBySizeAsync(minSize, maxSize);
            var squadDTOs = _mapper.Map<IEnumerable<SquadDTO>>(squads);
            return Ok(squadDTOs);
        }

    }
}



