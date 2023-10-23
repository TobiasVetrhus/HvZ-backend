using AutoMapper;
using HvZ_backend.Data.DTOs.Conversations;
using HvZ_backend.Data.Entities;
using HvZ_backend.Data.Exceptions;
using HvZ_backend.Services.Conversations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HvZ_backend.Controllers
{

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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ConversationDTO>>> GetConversations()
        {
            return Ok(_mapper.Map<IEnumerable<ConversationDTO>>(await _conversationService.GetAllAsync()));
        }


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

        [HttpPost]
        public async Task<ActionResult<ConversationDTO>> PostConversation(ConversationPostDTO conversation)
        {
            var newConversation = await _conversationService.AddAsync(_mapper.Map<Conversation>(conversation));

            return CreatedAtAction("GetConversation",
                new { id = newConversation.Id },
                _mapper.Map<ConversationDTO>(newConversation));
        }

        [HttpPut("{id}")]
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

        [HttpDelete("{id}")]
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
