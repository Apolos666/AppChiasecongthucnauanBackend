using AppChiaSeCongThucNauAnBackend.Data;
using AppChiaSeCongThucNauAnBackend.Features.Chat.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AppChiaSeCongThucNauAnBackend.Features.Chat.Queries.GetConversations;

public class GetConversationsQueryHandler : IRequestHandler<GetConversationsQuery, List<ConversationDto>>
{
    private readonly AppDbContext _context;

    public GetConversationsQueryHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<ConversationDto>> Handle(GetConversationsQuery request, CancellationToken cancellationToken)
    {
        var conversations = await _context.UserConversations
            .Where(uc => uc.UserId == request.UserId)
            .Select(uc => new ConversationDto
            {
                Id = uc.ConversationId,
                OtherUserId = uc.Conversation.UserConversations
                    .Where(otherUc => otherUc.UserId != request.UserId)
                    .Select(otherUc => otherUc.UserId)
                    .FirstOrDefault(),
                OtherUserName = uc.Conversation.UserConversations
                    .Where(otherUc => otherUc.UserId != request.UserId)
                    .Select(otherUc => otherUc.User.Name)
                    .FirstOrDefault(),
                LastMessage = uc.Conversation.Messages
                    .OrderByDescending(m => m.SentAt)
                    .Select(m => new MessageDto
                    {
                        Id = m.Id,
                        SenderId = m.SenderId,
                        Content = m.Content,
                        SentAt = m.SentAt
                    })
                    .FirstOrDefault()
            })
            .ToListAsync(cancellationToken);

        return conversations;
    }
}
