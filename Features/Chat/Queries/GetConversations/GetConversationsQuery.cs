using AppChiaSeCongThucNauAnBackend.Features.Chat.Dtos;
using MediatR;

namespace AppChiaSeCongThucNauAnBackend.Features.Chat.Queries.GetConversations;

public record GetConversationsQuery(Guid UserId) : IRequest<List<ConversationDto>>;
