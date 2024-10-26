using MediatR;

namespace AppChiaSeCongThucNauAnBackend.Features.Chat.Commands.StartConversation;

public record StartConversationCommand(Guid InitiatorId, Guid RecipientId) : IRequest<Guid>;
