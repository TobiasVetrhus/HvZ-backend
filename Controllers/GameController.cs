using AutoMapper;
using HvZ_backend.Data.DTOs.Games;
using HvZ_backend.Data.Entities;
using HvZ_backend.Data.Exceptions;
using HvZ_backend.Services.Games;
using Microsoft.AspNetCore.Mvc;

namespace HvZ_backend.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IGameService _gameService;
        private readonly IMapper _mapper;

        public GameController(IGameService gameService, IMapper mapper)
        {
            _gameService = gameService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GameDTO>>> GetGames()
        {
            var games = await _gameService.GetAllAsync();
            var gameDTOs = _mapper.Map<IEnumerable<GameDTO>>(games);
            return Ok(gameDTOs);
        }

        [HttpGet("{id}")]
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


        [HttpPost]
        public async Task<ActionResult<GameDTO>> AddGame(GamePostDTO game)
        {
            var newGame = await _gameService.AddAsync(_mapper.Map<Game>(game));

            await _gameService.UpdateConversationsAsync(newGame.Id, game.ConversationIds);
            await _gameService.UpdatePlayersAsync(newGame.Id, game.PlayerIds);
            await _gameService.UpdateMissionsAsync(newGame.Id, game.MissionIds);
            await _gameService.UpdateRulesAsync(newGame.Id, game.RuleIds);

            return CreatedAtAction("GetGameById", new { id = newGame.Id }, _mapper.Map<GameDTO>(newGame));
        }

        [HttpDelete("{id}")]
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
