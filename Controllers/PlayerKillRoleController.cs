using AutoMapper;
using HvZ_backend.Data.DTOs.PlayerKillRoles;
using HvZ_backend.Data.Entities;
using HvZ_backend.Data.Exceptions;
using HvZ_backend.Services.PlayerKillRoles;
using Microsoft.AspNetCore.Mvc;

namespace HvZ_backend.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PlayerKillRoleController : ControllerBase
    {
        private readonly IPlayerKillRoleService _playerKillRoleService;
        private readonly IMapper _mapper;

        public PlayerKillRoleController(IPlayerKillRoleService playerKillRoleService, IMapper mapper)
        {
            _playerKillRoleService = playerKillRoleService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlayerKillRoleDTO>>> GetPlayerKillRoles()
        {
            var playerkillroles = await _playerKillRoleService.GetAllAsync();
            var playerkillroleDTOs = _mapper.Map<IEnumerable<PlayerKillRoleDTO>>(playerkillroles);
            return Ok(playerkillroleDTOs);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PlayerKillRoleDTO>> GetPlayerKillRoleById(int id)
        {
            try
            {
                var playerkillrole = await _playerKillRoleService.GetByIdAsync(id);
                return Ok(_mapper.Map<PlayerKillRoleDTO>(playerkillrole));
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePlayerKillRole(int id, PlayerKillRolePutDTO playerkillrole)
        {
            if (id != playerkillrole.Id)
            {
                return BadRequest();
            }

            try
            {
                var updatedPlayerKillRole = await _playerKillRoleService.UpdateAsync(_mapper.Map<PlayerKillRole>(playerkillrole));
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }

            return NoContent();
        }
    }
}