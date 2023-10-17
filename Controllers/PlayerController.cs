﻿using AutoMapper;
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
        public async Task<ActionResult<IEnumerable<PlayerDTO>>> GetAllPlayers()
        {
            var players = await _playerService.GetAllPlayersAsync();
            var playerDTOs = _mapper.Map<IEnumerable<PlayerDTO>>(players);
            return Ok(playerDTOs);
        }

        /// <summary>
        /// Get a player by ID.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<PlayerDTO>> GetPlayerById(int id)
        {
            var player = await _playerService.GetPlayerByIdAsync(id);

            if (player == null)
            {
                return NotFound();
            }

            var playerDTO = _mapper.Map<PlayerDTO>(player);
            return Ok(playerDTO);
        }

        /// <summary>
        /// Create a new player.
        /// </summary>

        [HttpPost]
        public async Task<ActionResult<PlayerDTO>> CreatePlayer(PlayerPostDTO playerPostDTO)
        {
            if (playerPostDTO == null)
            {
                return BadRequest();
            }

            string subject = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            Guid userId = Guid.Parse(subject);

            var player = _mapper.Map<Player>(playerPostDTO);
            player.UserId = userId;

            var createdPlayer = await _playerService.CreatePlayerAsync(player);
            var playerDTO = _mapper.Map<PlayerDTO>(createdPlayer);

            return CreatedAtAction(nameof(GetPlayerById), new { id = playerDTO.Id }, playerDTO);
        }

        /// <summary>
        /// Update an existing player.
        /// </summary>

        [HttpPut("{id}")]
        public async Task<ActionResult<PlayerDTO>> UpdatePlayer(int id, PlayerPutDTO playerPutDTO)
        {
            if (playerPutDTO == null || id != playerPutDTO.Id)
            {
                return BadRequest();
            }

            var existingPlayer = await _playerService.GetPlayerByIdAsync(id);

            if (existingPlayer == null)
            {
                return NotFound();
            }

            var updatedPlayer = _mapper.Map(playerPutDTO, existingPlayer);
            var player = await _playerService.UpdatePlayerAsync(updatedPlayer);

            var playerDTO = _mapper.Map<PlayerDTO>(player);
            return Ok(playerDTO);
        }


        [HttpPut("{id}/update-zombiestate")]
        public async Task<IActionResult> UpdateZombieStateAsync(int id, bool zombie, string biteCode)
        {
            try
            {
                await _playerService.UpdateZombieStateAsync(id, zombie, biteCode);
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
        /// Delete a player by ID.
        /// </summary>

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlayer(int id)
        {
            try
            {
                var player = await _playerService.GetPlayerByIdAsync(id);

                if (player == null)
                {
                    return NotFound();
                }

                await _playerService.DeletePlayerAsync(id);

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
    }
}
