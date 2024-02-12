using Psinder.Dtos.MessageDtos;
using Psinder.Helpers;

namespace Psinder.Services.Interfaces
{
    public interface IMessageService
    {
        Task SendMessageAsync(CreateMessageDto messageDto, int senderId);
        Task<PagedResult<MessageDto>> GetMessagesForUserAsync(int userId, PageQuery query);
        Task DeleteMessageAsync(int messageId, int userId);
    }
}
