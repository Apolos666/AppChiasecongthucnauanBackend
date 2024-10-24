using AppChiaSeCongThucNauAnBackend.Data;
using AppChiaSeCongThucNauAnBackend.Features.Comment.Hubs;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace AppChiaSeCongThucNauAnBackend.Features.Comment.Commands.CreateComment;

public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, CommentDto>
{
    private readonly AppDbContext _context;
    private readonly IHubContext<CommentHub, ICommentClient> _hubContext;

    public CreateCommentCommandHandler(AppDbContext context, IHubContext<CommentHub, ICommentClient> hubContext)
    {
        _context = context;
        _hubContext = hubContext;
    }

    public async Task<CommentDto> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
    {
        var user = await _context.Users.FindAsync(request.UserId);
        if (user == null)
        {
            throw new Exception("Người dùng không tồn tại");
        }

        var comment = new Models.Comment
        {
            Id = Guid.NewGuid(),
            UserId = request.UserId,
            RecipeId = request.RecipeId,
            Content = request.Content,
            CreatedAt = DateTime.UtcNow
        };

        _context.Comments.Add(comment);
        await _context.SaveChangesAsync(cancellationToken);

        var commentDto = new CommentDto(comment.Id, comment.UserId, user.Name, comment.Content, comment.CreatedAt);

        await _hubContext.Clients.Group($"Recipe_{request.RecipeId}")
            .ReceiveComment(comment.UserId.ToString(), user.Name, comment.Content);

        return commentDto;
    }
}
