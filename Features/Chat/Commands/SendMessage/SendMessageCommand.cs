using MediatR;

namespace AppChiaSeCongThucNauAnBackend.Features.Chat.Commands.SendMessage;

public record SendMessageCommand(Guid SenderId, string senderUserName, Guid ConversationId, string Content) : IRequest<Guid>;
