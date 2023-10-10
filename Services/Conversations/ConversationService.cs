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

        public async Task<Conversation> AddAsync(Conversation obj)
        {
            await _context.Conversations.AddAsync(obj);
            await _context.SaveChangesAsync();
            return obj;
        }

        public Task DeleteByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Conversation>> GetAllAsync()
        {
            return await _context.Conversations.Include(c => c.Messages).ToListAsync();
        }

        public async Task<Conversation> GetByIdAsync(int id)
        {
            if (!await ConversationExistsAsync(id))
                throw new EntityNotFoundException(nameof(Conversation), id);

            var conversation = await _context.Conversations.Where(c => c.Id == id)
                .Include(c => c.Messages)
                .FirstAsync();

            return conversation;
        }

        public Task<Conversation> UpdateAsync(Conversation obj)
        {
            throw new NotImplementedException();
        }

        private async Task<bool> ConversationExistsAsync(int id)
        {
            return await _context.Conversations.AnyAsync(u => u.Id == id);
        }
    }
}
