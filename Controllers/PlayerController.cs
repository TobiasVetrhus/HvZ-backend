﻿using AutoMapper;
using HvZ_backend.Data.DTOs.Locations;
using HvZ_backend.Data.DTOs.Player;
using HvZ_backend.Data.Entities;
using HvZ_backend.Data.Exceptions;
using HvZ_backend.Services.Players;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using System.Security.Claims;

namespace HvZ_backend.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [ApiConventionType(typeof(DefaultApiConventions))]
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
        /// </summary>
        /// <returns>An action result containing a list of PlayerDTO objects.</returns>
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
        /// <param name="id">The unique identifier of the player to retrieve.</param>
        /// <returns>An action result containing a PlayerDTO representing the requested player.</returns>
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
        /// <param name="playerPostDTO">A DTO representing the player to be created.</param>
        /// <returns>An action result containing a PlayerDTO for the newly created player.</returns>
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
        /// Update a player's location by ID.
        /// </summary>
        /// <param name="id">The unique identifier of the player whose location to update.</param>
        /// <param name="locationUpdateDTO">A DTO representing the updated player location.</param>
        /// <returns>An action result indicating success or failure of the operation.</returns>
        /// <exception cref="EntityNotFoundException">Thrown when the requested player is not found.</exception>
        [HttpPut("leaveMarker/{id}")]
        public async Task<IActionResult> UpdatePlayerLocation(int id, [FromBody] LocationUpdateDTO locationUpdateDTO)
        {
            try
            {
                var success = await _playerService.updatePlayerLocationAsync(id, locationUpdateDTO.XCoordinate, locationUpdateDTO.YCoordinate);
                return NoContent();
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Update an existing player by ID.
        /// </summary>
        /// <param name="id">The unique identifier of the player to update.</param>
        /// <param name="playerPutDTO">A DTO representing the updated player information.</param>
        /// <returns>An action result containing a PlayerDTO representing the updated player.</returns>
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

        /// <summary>
        /// Delete a player by ID.
        /// </summary>
        /// <param name="id">The unique identifier of the player to delete.</param>
        /// <returns>An action result indicating success or failure of the deletion.</returns>
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
        /// Get a player by bitecode and set zombie to true.
        /// </summary>
        /// <param name="biteCode">The bitecode of the player to search for.</param>
        /// <returns>An action result containing a PlayerDTO representing the requested player.</returns>
        /// <exception cref="Exception">Thrown when an unexpected error occurs.</exception>
        [HttpGet("by-bitecode/{biteCode}")]
        public async Task<ActionResult<PlayerDTO>> GetPlayerByBiteCode(string biteCode)
        {
            try
            {
                var player = await _playerService.GetPlayerByBiteCodeAsync(biteCode);

                await _playerService.GetByIdAsync(player.Id);

                var playerDTO = _mapper.Map<PlayerDTO>(player);
                return Ok(playerDTO);
            }
            catch (Exception ex)
            {
                // Handle the exception here
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }


        /// <summary>
        /// Set a player to zombie by bitecode.
        /// </summary>
        /// <param name="biteCode">The bitecode of the player to convert to a zombie.</param>
        /// <returns>An action result containing a PlayerDTO representing the updated player.</returns>
        /// <exception cref="Exception">Thrown when an unexpected error occurs.</exception>
        [HttpPut("by-bitecode/{biteCode}")]
        public async Task<ActionResult<PlayerDTO>> SetPlayerToZombieByBiteCode(string biteCode)
        {
            try
            {
                var player = await _playerService.UpdateZombieStateAsync(biteCode);

                if (player == null)
                {
                    return NotFound();
                }

                var playerDTO = _mapper.Map<PlayerDTO>(player);
                return Ok(playerDTO);
            }
            catch (Exception ex)
            {
                // Handle the exception here
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        /// <summary>
        /// Update a players state; true (Zombie), false (Human)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="state"></param>
        /// <returns>Player with updated state</returns>
        [HttpPut("{id}/update-state")]
        public async Task<ActionResult<PlayerDTO>> UpdatePlayerState(int id, [FromBody] bool state)
        {
            var player = await _playerService.UpdatePlayerState(id, state);

            if (player == null)
            {
                return NotFound();
            }

            var playerDTO = _mapper.Map<PlayerDTO>(player);
            return Ok(playerDTO);
        }

    }
}
