namespace AppChiaSeCongThucNauAnBackend.Models;

public class UserConversation
{
    public Guid UserId { get; set; }
    public Guid ConversationId { get; set; }

    public User User { get; set; }
    public Conversation Conversation { get; set; }
}
