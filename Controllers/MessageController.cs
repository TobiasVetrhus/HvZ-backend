using AutoMapper;
using HvZ_backend.Data.DTOs.Messages;
using HvZ_backend.Data.Entities;
using HvZ_backend.Data.Exceptions;
using HvZ_backend.Services.Messages;
using Microsoft.AspNetCore.Mvc;

namespace HvZ_backend.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _messageService;
        private readonly IMapper _mapper;

        public MessageController(IMessageService messageService, IMapper mapper)
        {
            _messageService = messageService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MessageDTO>>> GetMessages()
        {
            return Ok(_mapper.Map<IEnumerable<MessageDTO>>(await _messageService.GetAllAsync()));
        }



        [HttpGet("{id}")]
        public async Task<ActionResult<MessageDTO>> GetMessage(int id)
        {
            try
            {
                return Ok(_mapper.Map<MessageDTO>(await _messageService.GetByIdAsync(id)));
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }


        [HttpPost]
        public async Task<ActionResult<MessageDTO>> PostMessage(MessagePostDTO message)
        {
            var newMessage = await _messageService.AddAsync(_mapper.Map<Message>(message));

            return CreatedAtAction("GetMessage",
                new { id = newMessage.Id },
                _mapper.Map<MessageDTO>(newMessage));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutMessage(int id, MessagePutDTO message)
        {
            if (id != message.Id)
            {
                return BadRequest();
            }
            try
            {
                var updatedMessage = await _messageService.UpdateAsync(_mapper.Map<Message>(message));
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }

            return NoContent();
        }

    }
}
