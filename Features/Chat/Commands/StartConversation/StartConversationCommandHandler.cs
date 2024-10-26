using AppChiaSeCongThucNauAnBackend.Data;
using AppChiaSeCongThucNauAnBackend.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AppChiaSeCongThucNauAnBackend.Features.Chat.Commands.StartConversation;

public class StartConversationCommandHandler : IRequestHandler<StartConversationCommand, Guid>
{
    private readonly AppDbContext _context;

    public StartConversationCommandHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(StartConversationCommand request, CancellationToken cancellationToken)
    {
        var existingConversation = await _context.UserConversations
            .Where(uc => uc.UserId == request.InitiatorId || uc.UserId == request.RecipientId)
            .GroupBy(uc => uc.ConversationId)
            .Where(g => g.Count() == 2)
            .Select(g => g.Key)
            .FirstOrDefaultAsync(cancellationToken);

        if (existingConversation != Guid.Empty)
        {
            return existingConversation;
        }

        var conversation = new Conversation
        {
            Id = Guid.NewGuid(),
            CreatedAt = DateTime.UtcNow
        };

        _context.Conversations.Add(conversation);

        var userConversations = new List<UserConversation>
        {
            new UserConversation { UserId = request.InitiatorId, ConversationId = conversation.Id },
            new UserConversation { UserId = request.RecipientId, ConversationId = conversation.Id }
        };

        _context.UserConversations.AddRange(userConversations);

        await _context.SaveChangesAsync(cancellationToken);

        return conversation.Id;
    }
}
