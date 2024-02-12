using Psinder.Data;
using Psinder.Helpers;

namespace Psinder.Repositories.Interfaces
{
    public interface IMessageRepository
    {
        Task AddMessageAsync(Message message);
        Task<Pagination<Message>> GetMessagesAsync(int userId, PageQuery query);
        Task<Message> GetMessageAsync(int id);
        void DeleteMessageAsync(Message message);
    }
}
