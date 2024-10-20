namespace AppChiaSeCongThucNauAnBackend.Models;

public class Conversation
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }

    public ICollection<UserConversation> UserConversations { get; set; }
    public ICollection<Message> Messages { get; set; }
}
