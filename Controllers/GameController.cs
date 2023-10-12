using AutoMapper;
using HvZ_backend.Consts;
using HvZ_backend.Data.DTOs.Games;
using HvZ_backend.Data.Entities;
using HvZ_backend.Data.Exceptions;
using HvZ_backend.Services.Games;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HvZ_backend.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class GameController : ControllerBase
    {
        private readonly IGameService _gameService;
        private readonly IMapper _mapper;

        public GameController(IGameService gameService, IMapper mapper, ILogger<GameController> logger)
        {
            _gameService = gameService;
            _mapper = mapper;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<GameDTO>>> GetGames()
        {
            var games = await _gameService.GetAllAsync();
            var gameDTOs = _mapper.Map<IEnumerable<GameDTO>>(games);
            return Ok(gameDTOs);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<GameDTO>> GetGameById(int id)
        {
            try
            {
                var game = await _gameService.GetByIdAsync(id);
                return Ok(_mapper.Map<GameDTO>(game));
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("filterbystates/{gamestate}")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<GameDTO>>> GetGamesByState(GameStatus gamestate)
        {
            var games = await _gameService.GetGamesByStateAsync(gamestate);
            var gameDTOs = _mapper.Map<IEnumerable<GameDTO>>(games);
            return Ok(gameDTOs);
        }

        [HttpPost]
        [Authorize(Roles = RoleConstants.Admin)]
        public async Task<ActionResult<GameDTO>> AddGame(GamePostDTO game)
        {
            var newGame = await _gameService.AddAsync(_mapper.Map<Game>(game));

            return CreatedAtAction("GetGameById", new { id = newGame.Id }, _mapper.Map<GameDTO>(newGame));
        }

        [HttpPut("{id}/add-rule/{ruleId}")]
        [Authorize(Roles = RoleConstants.Admin)]
        public async Task<IActionResult> AddRuleAsync(int id, int ruleId)
        {
            try
            {
                await _gameService.AddRuleAsync(id, ruleId);
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

        [HttpPut("{id}/add-player/{playerId}")]
        [Authorize(Roles = RoleConstants.Admin)]
        public async Task<IActionResult> AddPlayerAsync(int id, int playerId)
        {
            try
            {
                await _gameService.AddPlayerAsync(id, playerId);
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

        [HttpPut("{id}/add-mission/{missionId}")]
        [Authorize(Roles = RoleConstants.Admin)]
        public async Task<IActionResult> AddMissionAsync(int id, int missionId)
        {
            try
            {
                await _gameService.AddMissionAsync(id, missionId);
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

        [HttpPut("{id}/add-conversation/{conversationId}")]
        [Authorize(Roles = RoleConstants.Admin)]
        public async Task<IActionResult> AddConversationAsync(int id, int conversationId)
        {
            try
            {
                await _gameService.AddConversationAsync(id, conversationId);
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

        [HttpPut("{id}")]
        [Authorize(Roles = RoleConstants.Admin)]
        public async Task<IActionResult> UpdateGame(int id, GamePutDTO game)
        {
            if (id != game.Id)
            {
                return BadRequest();
            }

            try
            {
                var updatedGame = await _gameService.UpdateAsync(_mapper.Map<Game>(game));
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }

            return NoContent();
        }

        [HttpPut("{id}/update-conversations")]
        [Authorize(Roles = RoleConstants.Admin)]
        public async Task<IActionResult> UpdateConversationsAsync(int id, [FromBody] int[] conversations)
        {
            try
            {
                await _gameService.UpdateConversationsAsync(id, conversations);
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

        [HttpPut("{id}/update-rules")]
        [Authorize(Roles = RoleConstants.Admin)]
        public async Task<IActionResult> UpdateRulesAsync(int id, [FromBody] int[] rules)
        {
            try
            {
                await _gameService.UpdateRulesAsync(id, rules);
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

        [HttpPut("{id}/update-missions")]
        [Authorize(Roles = RoleConstants.Admin)]
        public async Task<IActionResult> UpdateMissionsAsync(int id, [FromBody] int[] missions)
        {
            try
            {
                await _gameService.UpdateMissionsAsync(id, missions);
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
        [Authorize(Roles = RoleConstants.Admin)]
        public async Task<IActionResult> UpdatePlayersAsync(int id, [FromBody] int[] players)
        {
            try
            {
                await _gameService.UpdatePlayersAsync(id, players);
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

        [HttpPut("{id}/remove-mission/{missionId}")]
        [Authorize(Roles = RoleConstants.Admin)]
        public async Task<IActionResult> RemoveMissionAsync(int id, int missionId)
        {
            try
            {
                await _gameService.RemoveMissionAsync(id, missionId);
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

        [HttpPut("{id}/remove-rule/{ruleId}")]
        [Authorize(Roles = RoleConstants.Admin)]
        public async Task<IActionResult> RemoveRuleAsync(int id, int ruleId)
        {
            try
            {
                await _gameService.RemoveRuleAsync(id, ruleId);
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

        [HttpPut("{id}/remove-conversation/{conversationId}")]
        [Authorize(Roles = RoleConstants.Admin)]
        public async Task<IActionResult> RemoveConversationAsync(int id, int conversationId)
        {
            try
            {
                await _gameService.RemoveConversationAsync(id, conversationId);
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
        [Authorize(Roles = RoleConstants.Admin)]
        public async Task<IActionResult> RemovePlayerAsync(int id, int playerId)
        {
            try
            {
                await _gameService.RemovePlayerAsync(id, playerId);
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

        [HttpDelete("{id}")]
        [Authorize(Roles = RoleConstants.Admin)]
        public async Task<IActionResult> DeleteGame(int id)
        {
            try
            {
                await _gameService.DeleteByIdAsync(id);
                return NoContent();
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
