using AutoMapper;
using HvZ_backend.Services.PlayerKillRoles;
using Microsoft.AspNetCore.Mvc;

namespace HvZ_backend.Controllers
{
    public class PlayerKillRoleController : ControllerBase
    {
        private readonly IPlayerKillRoleService _playerKillRoleService;
        private readonly IMapper _mapper;

        public PlayerKillRoleController(IPlayerKillRoleService playerKillRoleService, IMapper mapper)
        {
            _playerKillRoleService = playerKillRoleService;
            _mapper = mapper;
        }
    }
}
