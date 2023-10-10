using AutoMapper;
using HvZ_backend.Data.DTOs.Kills;
using HvZ_backend.Data.Entities;
using HvZ_backend.Data.Exceptions;
using HvZ_backend.Services.Kills;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HvZ_backend.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class KillController : ControllerBase
    {
        private readonly IKillService _killService;
        private readonly IMapper _mapper;

        public KillController(IKillService killService, IMapper mapper)
        {
            _killService = killService;
            _mapper = mapper;
        }
        // GET: api/v1/Kill/GetKills
        [HttpGet("GetKills")]
        public async Task<ActionResult<IEnumerable<KillDTO>>> GetKills()
        {
            var kills = await _killService.GetAllAsync();
            var killDTOs = _mapper.Map<IEnumerable<KillDTO>>(kills);
            return Ok(killDTOs);
        }

    }
}
