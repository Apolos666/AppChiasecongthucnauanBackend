using AppChiaSeCongThucNauAnBackend.Features.User.Commands.FollowUser;
using AppChiaSeCongThucNauAnBackend.Features.User.Commands.UnfollowUser;
using AppChiaSeCongThucNauAnBackend.Features.User.Queries.GetFollowStatus;
using Carter;
using MediatR;
using System.Security.Claims;

namespace AppChiaSeCongThucNauAnBackend.Features.User;

public class UserFollowModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/users-follow").WithTags("Users Follow");

        group.MapPost("/{id}/follow", FollowUser)
            .WithName("FollowUser")
            .RequireAuthorization();

        group.MapDelete("/{id}/unfollow", UnfollowUser)
            .WithName("UnfollowUser")
            .RequireAuthorization();

        group.MapGet("/{id}/follow-status", GetFollowStatus)
            .WithName("GetFollowStatus")
            .RequireAuthorization();
    }

    private async Task<IResult> FollowUser(Guid id, IMediator mediator, HttpContext httpContext)
    {
        var followerId = Guid.Parse(httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        var command = new FollowUserCommand(followerId, id);
        var result = await mediator.Send(command);

        return result ? Results.Ok() : Results.BadRequest("Đã theo dõi người dùng này rồi");
    }

    private async Task<IResult> UnfollowUser(Guid id, IMediator mediator, HttpContext httpContext)
    {
        var followerId = Guid.Parse(httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        var command = new UnfollowUserCommand(followerId, id);
        var result = await mediator.Send(command);

        return result ? Results.Ok() : Results.BadRequest("Chưa theo dõi người dùng này");
    }

    private async Task<IResult> GetFollowStatus(Guid id, IMediator mediator, HttpContext httpContext)
    {
        var followerId = Guid.Parse(httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        var query = new GetFollowStatusQuery(followerId, id);
        var result = await mediator.Send(query);

        return Results.Ok(new { isFollowing = result });
    }
}