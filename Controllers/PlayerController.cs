using AutoMapper;
using HvZ_backend.Data.DTOs.Player;
using HvZ_backend.Data.Entities;
using HvZ_backend.Data.Exceptions;
using HvZ_backend.Services.Players;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HvZ_backend.Controllers
{
    [ApiController]
    [Route("api/v1/players")]
    public class PlayerController : ControllerBase
    {
        private readonly IPlayerService _playerService;
        private readonly IMapper _mapper;

        public PlayerController(IPlayerService playerService, IMapper mapper)
        {
            _playerService = playerService ?? throw new ArgumentNullException(nameof(playerService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Get a list of all players.

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlayerDTO>>> GetPlayers()
        {
            var players = await _playerService.GetAllAsync();
            var playerDTOs = _mapper.Map<IEnumerable<PlayerDTO>>(players);
            return Ok(playerDTOs);
        }

        /// <summary>
        /// Get a player by ID.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<PlayerDTO>> GetPlayerById(int id)
        {
            var player = await _playerService.GetByIdAsync(id);

            var playerDTO = _mapper.Map<PlayerDTO>(player);
            return Ok(playerDTO);
        }

        /// <summary>
        /// Create a new player.
        /// </summary>

        [HttpPost]
        public async Task<ActionResult<PlayerDTO>> CreatePlayer(PlayerPostDTO playerPostDTO)
        {
            string subject = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            Guid userId = Guid.Parse(subject);

            var player = _mapper.Map<Player>(playerPostDTO);
            player.UserId = userId;

            var createdPlayer = await _playerService.AddAsync(player);
            var playerDTO = _mapper.Map<PlayerDTO>(createdPlayer);

            return CreatedAtAction(nameof(GetPlayerById), new { id = playerDTO.Id }, playerDTO);
        }

        /// <summary>
        /// Update an existing player.
        /// </summary>

        [HttpPut("{id}")]
        public async Task<ActionResult<PlayerDTO>> UpdatePlayer(int id, PlayerPutDTO playerPutDTO)
        {
            if (id != playerPutDTO.Id)
            {
                return BadRequest();
            }

            var existingPlayer = await _playerService.GetByIdAsync(id);

            var updatedPlayer = _mapper.Map(playerPutDTO, existingPlayer);
            var player = await _playerService.UpdateAsync(updatedPlayer);

            var playerDTO = _mapper.Map<PlayerDTO>(player);
            return Ok(playerDTO);
        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlayer(int id)
        {
            try
            {
                var player = await _playerService.GetByIdAsync(id);

                await _playerService.DeleteByIdAsync(id);

                return NoContent();
            }
            catch (EntityNotFoundException)
            {
                return NotFound();
            }
            catch (Exception)
            {
                return StatusCode(500); // Internal Server Error
            }
        }

        /// <summary>
        /// Get a player by bitecode and set zombie to true
        /// </summary>
        [HttpPut("by-bitecode/{biteCode}")]
        public async Task<ActionResult<PlayerDTO>> SetPlayerToZombieByBiteCode(string biteCode)
        {
            var player = await _playerService.UpdateZombieStateAsync(biteCode);

            if (player == null)
            {
                return NotFound();
            }

            var playerDTO = _mapper.Map<PlayerDTO>(player);
            return Ok(playerDTO);
        }

        [HttpGet("by-bitecode/{biteCode}")]
        public async Task<ActionResult<PlayerDTO>> GetPlayerByBiteCode(string biteCode)
        {
            var player = await _playerService.GetPlayerByBiteCodeAsync(biteCode);

            await _playerService.UpdateAsync(player);

            var playerDTO = _mapper.Map<PlayerDTO>(player);
            return Ok(playerDTO);
        }
    }
}
