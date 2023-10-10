using HvZ_backend.Data.Entities;
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

        public Task<Conversation> AddAsync(Conversation obj)
        {
            throw new NotImplementedException();
        }

        public Task DeleteByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Conversation>> GetAllAsync()
        {
            return await _context.Conversations.ToListAsync();
        }

        public Task<Conversation> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Conversation> UpdateAsync(Conversation obj)
        {
            throw new NotImplementedException();
        }
    }
}
