using Microsoft.EntityFrameworkCore;
using Psinder.Data;
using Psinder.Repositories.Interfaces;

namespace Psinder.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly PsinderDb _context;
        public MessageRepository(PsinderDb context)
        {
            _context = context;
        }

        public async Task AddMessageAsync(Message message)
        {
            await _context.Messages.AddAsync(message);
        }

        public void DeleteMessageAsync(Message message)
        {
            _context.Messages.Remove(message);
        }

        public async Task<Message> GetMessageAsync(int id)
        {
            return await _context.Messages.Where(m => m.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<Message>> GetMessagesAsync(int userId)
        {
            return await _context.Messages
                .OrderByDescending(x => x.MessageSend)
                .Where(m => (m.RecipientId == userId || m.SenderId == userId))
                .Where(m => m.RecipientId == userId && m.RecipientDeleted == false ||
                        m.SenderId == userId && m.SenderDeleted == false)
                .ToListAsync();
        }
    }
}
