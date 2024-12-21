using MediatR;

namespace AppChiaSeCongThucNauAnBackend.Features.Comment.Commands.DeleteComment;

public record DeleteCommentCommand(Guid CommentId) : IRequest<bool>;