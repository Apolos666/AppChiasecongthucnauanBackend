namespace AppChiaSeCongThucNauAnBackend.Features.Chat.Hubs;

public interface IChatClient
{
    Task ReceiveMessage(string senderId, string senderName, string message, DateTime sentAt);
}
