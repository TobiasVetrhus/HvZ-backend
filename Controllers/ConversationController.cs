using AutoMapper;
using HvZ_backend.Data.DTOs.Conversations;
using HvZ_backend.Data.Exceptions;
using HvZ_backend.Services.Conversations;
using Microsoft.AspNetCore.Mvc;

namespace HvZ_backend.Controllers
{

    [Route("api/v1/[controller]")]
    [ApiController]
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
    }
}
