using AutoMapper;
using HvZ_backend.Data.DTOs.Messages;
using HvZ_backend.Data.Entities;
using HvZ_backend.Data.Exceptions;
using HvZ_backend.Services.Messages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HvZ_backend.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _messageService;
        private readonly IMapper _mapper;

        public MessageController(IMessageService messageService, IMapper mapper)
        {
            _messageService = messageService;
            _mapper = mapper;
        }

        /// <summary>
        /// Retrieve a list of all messages.
        /// </summary>
        /// <returns>An action result containing a list of MessageDTO objects.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MessageDTO>>> GetMessages()
        {
            return Ok(_mapper.Map<IEnumerable<MessageDTO>>(await _messageService.GetAllAsync()));
        }

        /// <summary>
        /// Retrieve a specific message by its ID.
        /// </summary>
        /// <param name="id">The unique identifier of the message.</param>
        /// <returns>An action result containing a MessageDTO representing the requested message.</returns>
        /// <exception cref="EntityNotFoundException">Thrown when the requested message is not found.</exception>
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

        /// <summary>
        /// Create and post a new message.
        /// </summary>
        /// <param name="message">A DTO representing the message to be added.</param>
        /// <returns>An action result containing a MessageDTO for the newly added message.</returns>
        [HttpPost]
        public async Task<ActionResult<MessageDTO>> PostMessage(MessagePostDTO message)
        {
            var newMessage = await _messageService.AddAsync(_mapper.Map<Message>(message));

            return CreatedAtAction("GetMessage",
                new { id = newMessage.Id },
                _mapper.Map<MessageDTO>(newMessage));
        }

        /// <summary>
        /// Update an existing message with the specified ID.
        /// </summary>
        /// <param name="id">The unique identifier of the message to update.</param>
        /// <param name="message">A DTO representing the updated message information.</param>
        /// <returns>An action result indicating success or failure of the update.</returns>
        /// <exception cref="EntityNotFoundException">Thrown when the requested message is not found.</exception>
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

        /// <summary>
        /// Delete a message by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the message to delete.</param>
        /// <returns>An action result indicating success or failure of the deletion.</returns>
        /// <exception cref="EntityNotFoundException">Thrown when the requested message is not found.</exception>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMessage(int id)
        {
            try
            {
                await _messageService.DeleteByIdAsync(id);
                return NoContent();
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

    }
}
