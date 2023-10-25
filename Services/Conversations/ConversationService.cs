using HvZ_backend.Data.Entities;
using HvZ_backend.Data.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace HvZ_backend.Services.Conversations
{
    public class ConversationService : IConversationService
    {
        private readonly HvZDbContext _context;

        public ConversationService(HvZDbContext context)
        {
            _context = context;
        }

        // Add a conversation to the database
        public async Task<Conversation> AddAsync(Conversation obj)
        {
            await _context.Conversations.AddAsync(obj);
            await _context.SaveChangesAsync();
            return obj;
        }

        // Delete a conversation by ID
        public async Task DeleteByIdAsync(int id)
        {
            if (!await ConversationExistsAsync(id))
                throw new EntityNotFoundException(nameof(Conversation), id);

            // Remove the conversation and its messages
            var conversation = await _context.Conversations
                .Where(m => m.Id == id)
                .Include(c => c.Messages)
                .FirstAsync();

            conversation.Messages.Clear();

            _context.Conversations.Remove(conversation);
            await _context.SaveChangesAsync();
        }

        // Get all conversations with their messages
        public async Task<IEnumerable<Conversation>> GetAllAsync()
        {
            return await _context.Conversations.Include(c => c.Messages).ToListAsync();
        }

        // Get a conversation by ID with its messages
        public async Task<Conversation> GetByIdAsync(int id)
        {
            if (!await ConversationExistsAsync(id))
                throw new EntityNotFoundException(nameof(Conversation), id);

            var conversation = await _context.Conversations.Where(c => c.Id == id)
                .Include(c => c.Messages)
                .FirstAsync();

            return conversation;
        }

        // Update an existing conversation
        public async Task<Conversation> UpdateAsync(Conversation updatedConversation)
        {
            var existingConversation = await _context.Conversations.FindAsync(updatedConversation.Id);

            if (existingConversation == null)
                throw new EntityNotFoundException(nameof(Conversation), updatedConversation.Id);

            updatedConversation.GameId = existingConversation.GameId;

            // Update values and save changes
            _context.Entry(existingConversation).CurrentValues.SetValues(updatedConversation);
            await _context.SaveChangesAsync();

            return existingConversation;
        }

        // Check if a conversation with a given ID exists
        private async Task<bool> ConversationExistsAsync(int id)
        {
            return await _context.Conversations.AnyAsync(u => u.Id == id);
        }
    }
}
