using Psinder.Data;

namespace Psinder.Repositories.Interfaces
{
    public interface IMessageRepository
    {
        Task AddMessageAsync(Message message);
        Task<List<Message>> GetMessagesAsync(int userId);
        Task<Message> GetMessageAsync(int id);
        void DeleteMessageAsync(Message message);
    }
}
