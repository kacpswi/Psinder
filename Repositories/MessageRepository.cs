using Microsoft.EntityFrameworkCore;
using Psinder.Data;
using Psinder.Helpers;
using Psinder.Repositories.Interfaces;
using System.Linq.Expressions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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

        public async Task<Pagination<Message>> GetMessagesAsync(int userId, PageQuery query)
        {
            var baseQuery = _context.Messages
                .OrderBy(x => x.MessageSend)
                .Where(m => (m.RecipientId == userId || m.SenderId == userId))
                .Where(m => m.RecipientId == userId && m.RecipientDeleted == false ||
                        m.SenderId == userId && m.SenderDeleted == false)
                .Where(r => query.SearchPhrase == null || (r.RecipientEmail.ToLower().Contains(query.SearchPhrase.ToLower())
                                            || r.SenderEmail.ToLower().Contains(query.SearchPhrase.ToLower())));

            var messages = baseQuery
                .Skip(query.PageSize * (query.PageNumber - 1))
                .Take(query.PageSize)
                .ToList();

            var totalItemsCount = baseQuery.Count();

            return new Pagination<Message>(messages, totalItemsCount);
        }
    }
}
