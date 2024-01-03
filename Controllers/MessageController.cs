using Microsoft.AspNetCore.Mvc;
using Psinder.Dtos.MessageDtos;
using Psinder.Extensions;
using Psinder.Helpers;
using Psinder.Services.Interfaces;

namespace Psinder.Controllers
{
    [ApiController]
    [Route("api/messages")]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _messageService;

        public MessageController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        [HttpPost]
        public async Task<ActionResult> SendMessage([FromBody] CreateMessageDto message)
        {
            await _messageService.SendMessageAsync(message, User.GetUserId());
            return Ok();
        }

        [HttpGet()]
        public async Task<ActionResult<List<MessageDto>>> GetMessages([FromQuery] PageQuery query)
        {
            var result = await _messageService.GetMessagesForUserAsync(User.GetUserId(), query);
            return Ok(result);
        }

        [HttpDelete("{messageId}")]
        public async Task<ActionResult> DeleteMessage([FromRoute]int messageId)
        {
            await _messageService.DeleteMessageAsync(messageId, User.GetUserId());
            return NoContent();
        }

    }
}
