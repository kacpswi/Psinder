using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Psinder.Data;
using Psinder.Dtos.MessageDtos;
using Psinder.Dtos.ShelterDtos;
using Psinder.Exceptions;
using Psinder.Helpers;
using Psinder.Repositories.Interfaces;
using Psinder.Services.Interfaces;

namespace Psinder.Services
{
    public class MessageService : IMessageService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        public MessageService(IUnitOfWork uow, IMapper mapper, UserManager<User> userManager)
        {
            _mapper = mapper;
            _uow = uow;
            _userManager = userManager;
        }

        public async Task DeleteMessageAsync(int messageId, int userId)
        {
            var message = await _uow.MessageRepository.GetMessageAsync(messageId);

            if (message == null)
            {
                throw new NotFoundException("Message not found");
            }

            var user = await _uow.UserRepository.GetUserByIdAsync(userId);

            if (message == null)
            {
                throw new NotFoundException("User not found");
            }

            if (message.SenderEmail != user.Email && message.RecipientEmail != user.Email)
            {
                throw new UnauthorizedException("");
            }

            if (message.SenderEmail == user.Email) message.SenderDeleted = true;
            if (message.RecipientEmail == user.Email) message.RecipientDeleted = true;

            if (message.SenderDeleted && message.RecipientDeleted)
            {
                _uow.MessageRepository.DeleteMessageAsync(message);
            }

            await _uow.Complete();
        }

        public async Task<PagedResult<MessageDto>> GetMessagesForUserAsync(int userId, PageQuery query)
        {
            var paginationResult = await _uow.MessageRepository.GetMessagesAsync(userId, query);

            var sheltersDto = _mapper.Map<List<MessageDto>>(paginationResult.Data);

            var result = new PagedResult<MessageDto>(sheltersDto, paginationResult.TotalCount, query.PageSize, query.PageNumber);

            return result;

        }

        public async Task SendMessageAsync(CreateMessageDto messageDto, int senderId)
        {
            var sender = await _uow.UserRepository.GetUserByIdAsync(senderId);

            if (sender.Email == messageDto.RecipientEmail)
            {
                throw new BadRequestException("You cannot send messages to yourself.");
            }

            var recipient = await _uow.UserRepository.GetUserByEmailAsync(messageDto.RecipientEmail);

            if (recipient == null)
            {
                throw new NotFoundException("Recipient not found.");
            }

            var recipientRoles = await _userManager.GetRolesAsync(recipient);
            var senderRoles = await _userManager.GetRolesAsync(sender);

            if((!senderRoles.Contains("User") || (!recipientRoles.Contains("ShelterOwner") && !recipientRoles.Contains("ShelterWorker")))&&
                !senderRoles.Contains("ShelterOwner") && !senderRoles.Contains("ShelterWorker") || !recipientRoles.Contains("User"))
            {
                throw new BadRequestException("You cannot message a user.");
            }
            var message = new Message
            {
                Sender = sender,
                Recipient = recipient,
                SenderEmail = sender.UserName,
                RecipientEmail = recipient.UserName,
                Content = messageDto.Content
            };

            await _uow.MessageRepository.AddMessageAsync(message);
            await _uow.Complete();
        }
    }
}
