using AutoMapper;
using HvZ_backend.Data.DTOs.Users;
using HvZ_backend.Data.Entities;
using HvZ_backend.Data.Exceptions;
using HvZ_backend.Services.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HvZ_backend.Controllers
{
    [Authorize]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AppUserController : ControllerBase
    {
        private readonly IAppUserService _userService;
        private readonly IMapper _mapper;

        public AppUserController(IAppUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet("subject")]
        public ActionResult GetSubject()
        {
            var subject = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return Ok(new { Subject = subject });
        }

        [HttpGet("exists")]
        public async Task<IActionResult> GetIfExists()
        {
            string subject = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var user = await _userService.GetUserIfExists(new Guid(subject));
            return user is null ? NotFound() : Ok(user);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppUserDTO>>> GetAppUsers()
        {
            return Ok(_mapper.Map<IEnumerable<AppUserDTO>>(await _userService.GetAllAsync()));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AppUserDTO>> GetAppUser(Guid id)
        {
            try
            {
                return Ok(_mapper.Map<AppUserDTO>(await _userService.GetByIdAsync(id)));
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAppUser(Guid id, AppUserPutDTO user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }
            try
            {
                var updatedUser = await _userService.UpdateAsync(_mapper.Map<AppUser>(user));
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }

            return NoContent();
        }


        [HttpPost("register")]
        public async Task<ActionResult<AppUserDTO>> PostAppUser()
        {
            string subject = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            Guid userId = Guid.Parse(subject);

            var newUser = new AppUser
            {
                Id = userId
            };

            newUser.Id = userId;

            newUser = await _userService.AddAsync(newUser);

            var userDto = _mapper.Map<AppUserDTO>(newUser);

            return CreatedAtAction("GetAppUser", new { id = userDto.Id }, userDto);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAppUser(Guid id)
        {
            try
            {
                await _userService.DeleteByIdAsync(id);
                return NoContent();
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }


        [HttpPut("{id}/add-player/{playerId}")]
        public async Task<IActionResult> AddPlayerAsync(Guid id, int playerId)
        {
            try
            {
                await _userService.AddPlayerAsync(id, playerId);
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

        [HttpPut("{id}/update-players")]
        public async Task<IActionResult> UpdatePlayersAsync(Guid id, [FromBody] int[] players)
        {
            try
            {
                await _userService.UpdatePlayersAsync(id, players);
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
        public async Task<IActionResult> RemovePlayerAsync(Guid userId, int playerId)
        {
            try
            {
                await _userService.RemovePlayerAsync(userId, playerId);
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
    }
}
