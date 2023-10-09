using AutoMapper;
using HvZ_backend.Data.DTOs.PlayerKillRoles;
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
    }
}
