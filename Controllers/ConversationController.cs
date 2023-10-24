using AutoMapper;
using HvZ_backend.Consts;
using HvZ_backend.Data.DTOs.Conversations;
using HvZ_backend.Data.Entities;
using HvZ_backend.Data.Exceptions;
using HvZ_backend.Services.Conversations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HvZ_backend.Controllers
{
    /// <summary>
    /// Controller for managing conversations.
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class ConversationController : ControllerBase
    {
        private readonly IConversationService _conversationService;
        private readonly IMapper _mapper;

        public ConversationController(IConversationService conversationService, IMapper mapper)
        {
            _conversationService = conversationService;
            _mapper = mapper;
        }

        /// <summary>
        /// Get a list of all conversations.
        /// </summary>
        /// <returns>An action result containing a list of conversations (DTOs).</returns
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ConversationDTO>>> GetConversations()
        {
            return Ok(_mapper.Map<IEnumerable<ConversationDTO>>(await _conversationService.GetAllAsync()));
        }

        /// <summary>
        /// Get a conversation by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the conversation to retrieve.</param>
        /// <returns>An action result containing the conversation (DTO).</returns
        /// <exception cref="EntityNotFoundException">Thrown when the requested conversation is not found.</exception
        [HttpGet("{id}")]
        public async Task<ActionResult<ConversationDTO>> GetConversation(int id)
        {
            try
            {
                return Ok(_mapper.Map<ConversationDTO>(await _conversationService.GetByIdAsync(id)));
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Create a new conversation.
        /// </summary>
        /// <param name="conversation">The conversation data for the creation.</param>
        /// <returns>An action result containing the created conversation (DTO).</returns
        [HttpPost]
        [Authorize(Roles = RoleConstants.Admin)]
        public async Task<ActionResult<ConversationDTO>> PostConversation(ConversationPostDTO conversation)
        {
            var newConversation = await _conversationService.AddAsync(_mapper.Map<Conversation>(conversation));

            return CreatedAtAction("GetConversation",
                new { id = newConversation.Id },
                _mapper.Map<ConversationDTO>(newConversation));
        }

        /// <summary>
        /// Update an existing conversation.
        /// </summary>
        /// <param name="id">The unique identifier of the conversation to update.</param>
        /// <param name="conversation">The conversation data for the update.</param>
        /// <returns>An action result indicating success or failure.</returns
        /// <exception cref="EntityNotFoundException">Thrown when the requested conversation is not found.</exception
        [HttpPut("{id}")]
        [Authorize(Roles = RoleConstants.Admin)]
        public async Task<IActionResult> PutConversation(int id, ConversationPutDTO conversation)
        {
            if (id != conversation.Id)
            {
                return BadRequest();
            }
            try
            {
                var updatedConversation = await _conversationService.UpdateAsync(_mapper.Map<Conversation>(conversation));
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }

            return NoContent();
        }

        /// <summary>
        /// Delete a conversation by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the conversation to delete.</param>
        /// <returns>An action result indicating success or failure.</returns
        /// <exception cref="EntityNotFoundException">Thrown when the requested conversation is not found.</exception
        [HttpDelete("{id}")]
        [Authorize(Roles = RoleConstants.Admin)]
        public async Task<IActionResult> DeleteConversation(int id)
        {
            try
            {
                await _conversationService.DeleteByIdAsync(id);
                return NoContent();
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
