using AutoMapper;
using HvZ_backend.Data.DTOs.Games;
using HvZ_backend.Data.Entities;
using HvZ_backend.Data.Exceptions;
using HvZ_backend.Services.Games;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace HvZ_backend.Controllers
{
    /// <summary>
    /// Controller for managing games.
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class GameController : ControllerBase
    {
        private readonly IGameService _gameService;
        private readonly IMapper _mapper;

        public GameController(IGameService gameService, IMapper mapper, ILogger<GameController> logger)
        {
            _gameService = gameService;
            _mapper = mapper;
        }

        /// <summary>
        /// Get a list of all games.
        /// </summary>
        /// <returns>An action result containing a list of games (DTOs).</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GameDTO>>> GetGames()
        {
            var games = await _gameService.GetAllAsync();
            var gameDTOs = _mapper.Map<IEnumerable<GameDTO>>(games);
            return Ok(gameDTOs);
        }

        /// <summary>
        /// Get a game by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the game to retrieve.</param>
        /// <returns>An action result containing the game (DTO).</returns>
        /// <exception cref="EntityNotFoundException">Thrown when the requested game is not found.</exception>
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

        /// <summary>
        /// Get games filtered by their state (e.g., active, completed, etc.).
        /// </summary>
        /// <param name="gamestate">The game state to filter by.</param>
        /// <returns>An action result containing a list of filtered games (DTOs).</returns>
        [HttpGet("filterbystates/{gamestate}")]
        public async Task<ActionResult<IEnumerable<GameDTO>>> GetGamesByState(GameStatus gamestate)
        {
            var games = await _gameService.GetGamesByStateAsync(gamestate);
            var gameDTOs = _mapper.Map<IEnumerable<GameDTO>>(games);
            return Ok(gameDTOs);
        }

        /// <summary>
        /// Create a new game.
        /// </summary>
        /// <param name="game">The game data for the creation.</param>
        /// <returns>An action result containing the created game (DTO).</returns>
        [HttpPost]
        public async Task<ActionResult<GameDTO>> AddGame(GamePostDTO game)
        {
            var newGame = await _gameService.AddAsync(_mapper.Map<Game>(game));

            return CreatedAtAction("GetGameById", new { id = newGame.Id }, _mapper.Map<GameDTO>(newGame));
        }

        /// <summary>
        /// Add a rule to a game.
        /// </summary>
        /// <param name="id">The unique identifier of the game.</param>
        /// <param name="ruleId">The rule identifier to add.</param>
        /// <returns>An action result indicating success or failure.</returns>
        /// <exception cref="EntityNotFoundException">Thrown when the requested game is not found.</exception>
        /// <exception cref="EntityValidationException">Thrown when the rule addition is not valid.</exception>
        [HttpPut("{id}/add-rule/{ruleId}")]
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

        /// <summary>
        /// Add a player to a game.
        /// </summary>
        /// <param name="id">The unique identifier of the game.</param>
        /// <param name="playerId">The player identifier to add.</param>
        /// <returns>An action result indicating success or failure.</returns>
        /// <exception cref="EntityNotFoundException">Thrown when the requested game or player is not found.</exception>
        /// <exception cref="EntityValidationException">Thrown when the player addition is not valid.</exception>
        [HttpPut("{id}/add-player/{playerId}")]
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

        /// <summary>
        /// Add a mission to a game.
        /// </summary>
        /// <param name="id">The unique identifier of the game.</param>
        /// <param name="missionId">The mission identifier to add.</param>
        /// <returns>An action result indicating success or failure.</returns>
        /// <exception cref="EntityNotFoundException">Thrown when the requested game or mission is not found.</exception>
        /// <exception cref="EntityValidationException">Thrown when the mission addition is not valid.</exception>
        [HttpPut("{id}/add-mission/{missionId}")]
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

        /// <summary>
        /// Add a conversation to a game.
        /// </summary>
        /// <param name="id">The unique identifier of the game.</param>
        /// <param name="conversationId">The conversation identifier to add.</param>
        /// <returns>An action result indicating success or failure.</returns>
        /// <exception cref="EntityNotFoundException">Thrown when the requested game or conversation is not found.</exception>
        /// <exception cref="EntityValidationException">Thrown when the conversation addition is not valid.</exception>
        [HttpPut("{id}/add-conversation/{conversationId}")]
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

        /// <summary>
        /// Update a game's information.
        /// </summary>
        /// <param name="id">The unique identifier of the game.</param>
        /// <param name="game">The updated game data.</param>
        /// <returns>An action result indicating success or failure.</returns>
        /// <exception cref="EntityNotFoundException">Thrown when the requested game is not found.</exception>
        [HttpPut("{id}")]
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

        /// <summary>
        /// Update the list of conversations for a game.
        /// </summary>
        /// <param name="id">The unique identifier of the game.</param>
        /// <param name="conversations">An array of conversation identifiers to update the game's conversations.</param>
        /// <returns>An action result indicating success or failure.</returns>
        /// <exception cref="EntityNotFoundException">Thrown when the requested game is not found.</exception>
        /// <exception cref="EntityValidationException">Thrown when the conversation update is not valid.</exception>
        [HttpPut("{id}/update-conversations")]
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

        /// <summary>
        /// Update the list of rules for a game.
        /// </summary>
        /// <param name="id">The unique identifier of the game.</param>
        /// <param name="rules">An array of rule identifiers to update the game's rules.</param>
        /// <returns>An action result indicating success or failure.</returns>
        /// <exception cref="EntityNotFoundException">Thrown when the requested game is not found.</exception>
        /// <exception cref="EntityValidationException">Thrown when the rule update is not valid.</exception>
        [HttpPut("{id}/update-rules")]
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

        /// <summary>
        /// Update the list of missions for a game.
        /// </summary>
        /// <param name="id">The unique identifier of the game.</param>
        /// <param name="missions">An array of mission identifiers to update the game's missions.</param>
        /// <returns>An action result indicating success or failure.</returns>
        /// <exception cref="EntityNotFoundException">Thrown when the requested game is not found.</exception>
        /// <exception cref="EntityValidationException">Thrown when the mission update is not valid.</exception>
        [HttpPut("{id}/update-missions")]
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

        /// <summary>
        /// Update the list of players for a game.
        /// </summary>
        /// <param name="id">The unique identifier of the game.</param>
        /// <param name="players">An array of player identifiers to update the game's players.</param>
        /// <returns>An action result indicating success or failure.</returns>
        /// <exception cref="EntityNotFoundException">Thrown when the requested game is not found.</exception>
        /// <exception cref="EntityValidationException">Thrown when the player update is not valid.</exception>
        [HttpPut("{id}/update-players")]
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

        /// <summary>
        /// Remove a mission from a game.
        /// </summary>
        /// <param name="id">The unique identifier of the game.</param>
        /// <param name="missionId">The mission identifier to remove.</param>
        /// <returns>An action result indicating success or failure.</returns>
        /// <exception cref="EntityNotFoundException">Thrown when the requested game or mission is not found.</exception>
        /// <exception cref="EntityValidationException">Thrown when the mission removal is not valid.</exception>
        [HttpPut("{id}/remove-mission/{missionId}")]
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

        /// <summary>
        /// Remove a rule from a game.
        /// </summary>
        /// <param name="id">The unique identifier of the game.</param>
        /// <param name="ruleId">The rule identifier to remove.</param>
        /// <returns>An action result indicating success or failure.</returns>
        /// <exception cref="EntityNotFoundException">Thrown when the requested game or rule is not found.</exception>
        /// <exception cref="EntityValidationException">Thrown when the rule removal is not valid.</exception>
        [HttpPut("{id}/remove-rule/{ruleId}")]
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

        /// <summary>
        /// Remove a conversation from a game.
        /// </summary>
        /// <param name="id">The unique identifier of the game.</param>
        /// <param name="conversationId">The conversation identifier to remove.</param>
        /// <returns>An action result indicating success or failure.</returns>
        /// <exception cref="EntityNotFoundException">Thrown when the requested game or conversation is not found.</exception>
        /// <exception cref="EntityValidationException">Thrown when the conversation removal is not valid.</exception>
        [HttpPut("{id}/remove-conversation/{conversationId}")]
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

        /// <summary>
        /// Remove a player from a game.
        /// </summary>
        /// <param name="id">The unique identifier of the game.</param>
        /// <param name="playerId">The player identifier to remove.</param>
        /// <returns>An action result indicating success or failure.</returns>
        /// <exception cref="EntityNotFoundException">Thrown when the requested game or player is not found.</exception>
        /// <exception cref="EntityValidationException">Thrown when the player removal is not valid.</exception>
        [HttpPut("{id}/remove-player/{playerId}")]
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

        /// <summary>
        /// Delete a game by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the game to delete.</param>
        /// <returns>An action result indicating success or failure.</returns>
        /// <exception cref="EntityNotFoundException">Thrown when the requested game is not found.</exception>
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
