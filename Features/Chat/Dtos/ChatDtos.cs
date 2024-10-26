namespace AppChiaSeCongThucNauAnBackend.Features.Chat.Dtos;

public class ConversationDto
{
    public Guid Id { get; set; }
    public Guid OtherUserId { get; set; }
    public string OtherUserName { get; set; }
    public MessageDto LastMessage { get; set; }
}

public class MessageDto
{
    public Guid Id { get; set; }
    public Guid SenderId { get; set; }
    public string Content { get; set; }
    public DateTime SentAt { get; set; }
}
