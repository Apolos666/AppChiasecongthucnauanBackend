namespace AppChiaSeCongThucNauAnBackend.Models;

public class Message
{
    public Guid Id { get; set; }
    public Guid ConversationId { get; set; }
    public Guid SenderId { get; set; }
    public string Content { get; set; }
    public DateTime SentAt { get; set; }

    public Conversation Conversation { get; set; }
    public User Sender { get; set; }
}
