﻿namespace Psinder.Data
{
    public class Message
    {
        public int Id { get; set; }
        public int SenderId { get; set; }
        public string SenderEmail { get; set; }
        public User Sender { get; set; }
        public int RecipientId { get; set; }
        public string RecipientEmail { get; set; }
        public User Recipient { get; set; }
        public string Content { get; set; }
        public DateTime MessageSend { get; set; } = DateTime.UtcNow;
        public bool SenderDeleted { get; set; }
        public bool RecipientDeleted { get; set; }
    }
}
