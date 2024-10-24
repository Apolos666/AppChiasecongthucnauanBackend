using MediatR;

namespace AppChiaSeCongThucNauAnBackend.Features.Comment.Commands.CreateComment;

public record CreateCommentCommand(Guid UserId, Guid RecipeId, string Content) : IRequest<CommentDto>;

public record CommentDto(Guid Id, Guid UserId, string UserName, string Content, DateTime CreatedAt);
