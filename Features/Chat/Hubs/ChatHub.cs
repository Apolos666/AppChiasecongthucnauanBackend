using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace AppChiaSeCongThucNauAnBackend.Features.Chat.Hubs;

[Authorize]
public class ChatHub : Hub<IChatClient>
{
    private readonly ILogger<ChatHub> _logger;

    public ChatHub(ILogger<ChatHub> logger)
    {
        _logger = logger;
    }

    public async Task JoinConversation(string conversationId)
    {
        var userId = Context.UserIdentifier;

        _logger.LogInformation("Người dùng {UserId} tham gia cuộc trò chuyện {ConversationId}", userId, conversationId);

        await Groups.AddToGroupAsync(Context.ConnectionId, conversationId);
    }

    public async Task LeaveConversation(string conversationId)
    {
        var userId = Context.UserIdentifier;

        _logger.LogInformation("Người dùng {UserId} rời khỏi cuộc trò chuyện {ConversationId}", userId, conversationId);

        await Groups.RemoveFromGroupAsync(Context.ConnectionId, conversationId);
    }
}
