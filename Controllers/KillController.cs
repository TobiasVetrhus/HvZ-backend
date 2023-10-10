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
        
        // GET: api/v1/Kill/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<KillDTO>> GetKill(int id)
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
        
        // POST: api/v1/Kill
        [HttpPost]
        public async Task<ActionResult<KillDTO>> PostKill(KillPostDTO killPostDTO)
        {
            var newKill = await _killService.CreateKillAsync(killPostDTO);

            return CreatedAtAction("GetKill",
                new { id = newKill.Id },
                _mapper.Map<KillDTO>(newKill));
        }
        // DELETE: api/v1/Kill/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKill(int id)
        {
            try
            {
                // Delete a kill by ID and return NoContent on success
                var isDeleted = await _killService.DeleteKillAsync(id);
                if (isDeleted)
                {
                    return NoContent();
                }
                else
                {
                    return NotFound("Kill not found.");
                }
            }
            catch (EntityNotFoundException ex)
            {
                // Handle the case where the kill with the specified ID was not found
                return NotFound(ex.Message);
            }
        }
    }
}

    

