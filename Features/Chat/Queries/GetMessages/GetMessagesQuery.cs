using AppChiaSeCongThucNauAnBackend.Features.Chat.Dtos;
using MediatR;

namespace AppChiaSeCongThucNauAnBackend.Features.Chat.Queries.GetMessages;

public record GetMessagesQuery(Guid ConversationId, Guid UserId) : IRequest<List<MessageDto>>;
