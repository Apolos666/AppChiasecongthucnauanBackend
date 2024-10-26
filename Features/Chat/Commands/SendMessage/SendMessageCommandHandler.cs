using AppChiaSeCongThucNauAnBackend.Data;
using AppChiaSeCongThucNauAnBackend.Models;
using AppChiaSeCongThucNauAnBackend.Features.Chat.Hubs;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace AppChiaSeCongThucNauAnBackend.Features.Chat.Commands.SendMessage;

public class SendMessageCommandHandler : IRequestHandler<SendMessageCommand, Guid>
{
    private readonly AppDbContext _context;
    private readonly IHubContext<ChatHub, IChatClient> _chatHub;

    public SendMessageCommandHandler(AppDbContext context, IHubContext<ChatHub, IChatClient> chatHub)
    {
        _context = context;
        _chatHub = chatHub;
    }

    public async Task<Guid> Handle(SendMessageCommand request, CancellationToken cancellationToken)
    {
        var conversation = await _context.Conversations
            .Include(c => c.UserConversations)
            .FirstOrDefaultAsync(c => c.Id == request.ConversationId, cancellationToken);

        if (conversation == null || !conversation.UserConversations.Any(uc => uc.UserId == request.SenderId))
        {
            throw new Exception("Cuộc trò chuyện không tồn tại hoặc người dùng không có quyền gửi tin nhắn");
        }

        var message = new Message
        {
            Id = Guid.NewGuid(),
            ConversationId = request.ConversationId,
            SenderId = request.SenderId,
            Content = request.Content,
            SentAt = DateTime.UtcNow
        };

        _context.Messages.Add(message);
        await _context.SaveChangesAsync(cancellationToken);

        await _chatHub.Clients
            .Group(request.ConversationId.ToString())
            .ReceiveMessage(request.SenderId.ToString(), request.senderUserName, request.Content, message.SentAt);

        return message.Id;
    }
}
