using AutoMapper;
using HvZ_backend.Data.DTOs.Games;
using HvZ_backend.Data.Entities;
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


        [HttpPost]
        public async Task<ActionResult<GameDTO>> AddGame(GamePostDTO game)
        {
            var newGame = await _gameService.AddAsync(_mapper.Map<Game>(game));
            return null;
        }
    }
}
