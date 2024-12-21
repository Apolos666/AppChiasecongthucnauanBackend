using AppChiaSeCongThucNauAnBackend.Features.Comment.Commands.CreateComment;
using Carter;
using MediatR;
using System.Security.Claims;
using AppChiaSeCongThucNauAnBackend.Features.Comment.Commands.DeleteComment;

namespace AppChiaSeCongThucNauAnBackend.Features.Comment;

public class CommentEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/comments").WithTags("Comments");

        group.MapPost("/", CreateComment)
            .RequireAuthorization();

        group.MapDelete("/{id}", DeleteComment);    
    }

    private async Task<IResult> CreateComment(
        CreateCommentRequest request,
        HttpContext httpContext,
        IMediator mediator)
    {
        var userId = Guid.Parse(httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        var command = new CreateCommentCommand(userId, request.RecipeId, request.Content);
        var result = await mediator.Send(command);
        return Results.Ok(result);
    }

    private static async Task<IResult> DeleteComment(Guid id, IMediator mediator)
    {
        var command = new DeleteCommentCommand(id);
        var result = await mediator.Send(command);
        
        return result ? Results.Ok() : Results.NotFound("Không tìm thấy bình luận");
    }
}

public record CreateCommentRequest(Guid RecipeId, string Content);
