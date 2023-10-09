using HvZ_backend.Data.Entities;
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

        public async Task<Message> AddAsync(Message obj)
        {
            await _context.Messages.AddAsync(obj);
            await _context.SaveChangesAsync();
            return obj;
        }

        public async Task<IEnumerable<Message>> GetAllAsync()
        {
            return await _context.Messages.ToListAsync();
        }

        public Task<Message> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Message> UpdateAsync(Message obj)
        {
            throw new NotImplementedException();
        }
        public Task DeleteByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

    }
}
