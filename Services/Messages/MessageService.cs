using HvZ_backend.Data.Entities;
using HvZ_backend.Data.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace HvZ_backend.Services.Messages
{
    public class MessageService : IMessageService
    {
        private readonly HvZDbContext _context;

        public MessageService(HvZDbContext context)
        {
            _context = context;
        }

        // Add a new message to the database
        public async Task<Message> AddAsync(Message obj)
        {
            await _context.Messages.AddAsync(obj);
            await _context.SaveChangesAsync();
            return obj;
        }

        // Get all messages
        public async Task<IEnumerable<Message>> GetAllAsync()
        {
            return await _context.Messages.ToListAsync();
        }

        // Get a message by its ID
        public async Task<Message> GetByIdAsync(int id)
        {
            if (!await MessageExistsAsync(id))
                throw new EntityNotFoundException(nameof(Message), id);

            var message = await _context.Messages.Where(m => m.Id == id)
                .FirstAsync();

            return message;
        }

        /*
        public async Task<Message> UpdateAsync(Message obj)
        {
            if (!await MessageExistsAsync(obj.Id))
                throw new EntityNotFoundException(nameof(Message), obj.Id);

            _context.Entry(obj).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return obj;
        }
        */

        // Update an existing message
        public async Task<Message> UpdateAsync(Message updatedMessage)
        {
            var existingMessage = await _context.Messages.FindAsync(updatedMessage.Id);

            if (existingMessage == null)
                throw new EntityNotFoundException(nameof(Message), updatedMessage.Id);

            updatedMessage.ConversationId = existingMessage.ConversationId;
            updatedMessage.PlayerId = existingMessage.PlayerId;

            _context.Entry(existingMessage).CurrentValues.SetValues(updatedMessage);
            await _context.SaveChangesAsync();

            return existingMessage;
        }

        // Delete a message by its ID
        public async Task DeleteByIdAsync(int id)
        {
            if (!await MessageExistsAsync(id))
                throw new EntityNotFoundException(nameof(Message), id);

            var message = await _context.Messages
                .Where(m => m.Id == id)
                .FirstAsync();

            _context.Messages.Remove(message);
            await _context.SaveChangesAsync();
        }

        // Helper method to check if a message with a given ID exists
        private async Task<bool> MessageExistsAsync(int id)
        {
            return await _context.Messages.AnyAsync(u => u.Id == id);
        }
    }
}
