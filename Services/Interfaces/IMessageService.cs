using Psinder.Dtos.MessageDtos;

namespace Psinder.Services.Interfaces
{
    public interface IMessageService
    {
        Task SendMessageAsync(CreateMessageDto messageDto, int senderId);
        Task<List<MessageDto>> GetMessagesForUserAsync(int userId);
        Task DeleteMessageAsync(int messageId, int userId);
    }
}
