using AppChiaSeCongThucNauAnBackend.Data;
using AppChiaSeCongThucNauAnBackend.Features.Chat.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AppChiaSeCongThucNauAnBackend.Features.Chat.Queries.GetMessages;

public class GetMessagesQueryHandler : IRequestHandler<GetMessagesQuery, List<MessageDto>>
{
    private readonly AppDbContext _context;

    public GetMessagesQueryHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<MessageDto>> Handle(GetMessagesQuery request, CancellationToken cancellationToken)
    {
        var conversation = await _context.Conversations
            .Include(c => c.UserConversations)
            .FirstOrDefaultAsync(c => c.Id == request.ConversationId, cancellationToken);

        if (conversation == null || !conversation.UserConversations.Any(uc => uc.UserId == request.UserId))
        {
            throw new Exception("Cuộc trò chuyện không tồn tại hoặc người dùng không có quyền truy cập");
        }

        var messages = await _context.Messages
            .Where(m => m.ConversationId == request.ConversationId)
            .OrderBy(m => m.SentAt)
            .Select(m => new MessageDto
            {
                Id = m.Id,
                SenderId = m.SenderId,
                Content = m.Content,
                SentAt = m.SentAt
            })
            .ToListAsync(cancellationToken);

        return messages;
    }
}
