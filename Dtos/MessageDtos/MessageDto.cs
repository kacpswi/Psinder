namespace Psinder.Dtos.MessageDtos
{
    public class MessageDto
    {
        public int Id { get; set; }
        public int SenderId { get; set; }
        public string SenderEmail { get; set; }
        public int RecipientId { get; set; }
        public string RecipientEmail { get; set; }
        public string Content { get; set; }
        public DateTime MessageSend { get; set; }
    }
}
