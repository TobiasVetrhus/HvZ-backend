using AutoMapper;
using HvZ_backend.Data.DTOs.Users;
using HvZ_backend.Data.Entities;
using HvZ_backend.Data.Exceptions;
using HvZ_backend.Services.Users;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using System.Security.Claims;

namespace HvZ_backend.Controllers
{
    /// <summary>
    /// Controller for managing application users.
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class AppUserController : ControllerBase
    {
        private readonly IAppUserService _userService;
        private readonly IMapper _mapper;

        public AppUserController(IAppUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        /// <summary>
        /// Get the subject (user identifier) for the authenticated user.
        /// </summary>
        /// <returns>An action result containing the user's subject (identifier).</returns>
        [HttpGet("subject")]
        public ActionResult GetSubject()
        {
            var subject = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return Ok(new { Subject = subject });
        }

        /// <summary>
        /// Check if a user with a specific identifier exists.
        /// </summary>
        /// <returns>An action result indicating whether the user exists or not.</returns>
        [HttpGet("exists")]
        public async Task<IActionResult> GetIfExists()
        {
            string subject = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var user = await _userService.GetUserIfExists(new Guid(subject));
            return user is null ? NotFound() : Ok(user);
        }

        /// <summary>
        /// Get a list of all application users.
        /// </summary>
        /// <returns>An action result containing a list of application users (DTOs).</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppUserDTO>>> GetAppUsers()
        {
            return Ok(_mapper.Map<IEnumerable<AppUserDTO>>(await _userService.GetAllAsync()));
        }

        /// <summary>
        /// Get an application user by their unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the user to retrieve.</param>
        /// <returns>An action result containing the application user (DTO).</returns>
        /// <exception cref="EntityNotFoundException">Thrown when the requested user is not found.</exception>
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


        /// <summary>
        /// Update an existing application user.
        /// </summary>
        /// <param name="id">The unique identifier of the user to update.</param>
        /// <param name="user">The user data for the update.</param>
        /// <returns>An action result indicating success or failure.</returns>
        /// <exception cref="EntityNotFoundException">Thrown when the requested user is not found.</exception>
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

        /// <summary>
        /// Register a new application user.
        /// </summary>
        /// <returns>An action result containing the registered application user (DTO).</returns>
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

        /// <summary>
        /// Delete an application user by their unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the user to delete.</param>
        /// <returns>An action result indicating success or failure.</returns>
        /// <exception cref="EntityNotFoundException">Thrown when the requested user is not found.</exception>
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

        /// <summary>
        /// Add a player to a user's collection of players.
        /// </summary>
        /// <param name="id">The unique identifier of the user.</param>
        /// <param name="playerId">The player identifier to add.</param>
        /// <returns>An action result indicating success or failure.</returns>
        /// <exception cref="EntityNotFoundException">Thrown when the requested user is not found.</exception>
        /// <exception cref="EntityValidationException">Thrown when the player addition is not valid.</exception>
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

        /// <summary>
        /// Update a user's collection of players.
        /// </summary>
        /// <param name="id">The unique identifier of the user.</param>
        /// <param name="players">An array of player identifiers for the update.</param>
        /// <returns>An action result indicating success or failure.</returns>
        /// <exception cref="EntityNotFoundException">Thrown when the requested user is not found.</exception>
        /// <exception cref="EntityValidationException">Thrown when the player update is not valid.</exception>
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

        /// <summary>
        /// Remove a player from a user's collection of players.
        /// </summary>
        /// <param name="userId">The unique identifier of the user.</param>
        /// <param name="playerId">The player identifier to remove.</param>
        /// <returns>An action result indicating success or failure.</returns>
        /// <exception cref="EntityNotFoundException">Thrown when the requested user is not found.</exception>
        /// <exception cref="EntityValidationException">Thrown when the player removal is not valid.</exception>
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
