using AppChiaSeCongThucNauAnBackend.Data;
using MediatR;

namespace AppChiaSeCongThucNauAnBackend.Features.Comment.Commands.DeleteComment;

public class DeleteCommentCommandHandler(AppDbContext context) : IRequestHandler<DeleteCommentCommand, bool>
{
    public async Task<bool> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
    {
        var comment = await context.Comments.FindAsync(request.CommentId, cancellationToken);
        
        if (comment == null)
            return false;

        context.Comments.Remove(comment);
        await context.SaveChangesAsync(cancellationToken);
        
        return true;
    }
}