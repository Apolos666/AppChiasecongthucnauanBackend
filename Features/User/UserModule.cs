using AppChiaSeCongThucNauAnBackend.Features.User.Commands.UpdateUser;
using AppChiaSeCongThucNauAnBackend.Features.User.Queries.GetUserById;
using AppChiaSeCongThucNauAnBackend.Features.User.Queries.GetUsers;
using Carter;
using MediatR;
using System.Security.Claims;
using AppChiaSeCongThucNauAnBackend.Features.User.Commands.DeleteUser;

namespace AppChiaSeCongThucNauAnBackend.Features.User;

public class UserModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/users").WithTags("Users");

        group.MapGet("/{id}", GetUserById)
            .WithName("GetUserById")
            .RequireAuthorization();

        group.MapPut("/{id}", UpdateUser)
            .WithName("UpdateUser")
            .RequireAuthorization();

        group.MapGet("/", GetUsers)
            .WithName("GetUsers");

        group.MapDelete("/{id}", DeleteUser)
            .WithName("DeleteUser");
    }

    private async Task<IResult> GetUserById(Guid id, IMediator mediator)
    {
        var query = new GetUserByIdQuery(id);
        var result = await mediator.Send(query);
        return Results.Ok(result);
    }

    private async Task<IResult> UpdateUser(Guid id, UpdateUserDto userDto, IMediator mediator, HttpContext httpContext)
    {
        var userId = Guid.Parse(httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        if (userId != id)
        {
            return Results.Forbid();
        }

        var command = new UpdateUserCommand(id, userDto);
        var result = await mediator.Send(command);

        if (result)
        {
            return Results.NoContent();
        }

        return Results.NotFound();
    }

    private async Task<IResult> GetUsers(IMediator mediator)
    {
        var query = new GetUsersQuery();
        var result = await mediator.Send(query);
        return Results.Ok(result);
    }

    private async Task<IResult> DeleteUser(Guid id, IMediator mediator)
    {
        var command = new DeleteUserCommand(id);
        var result = await mediator.Send(command);

        if (result)
        {
            return Results.NoContent();
        }

        return Results.NotFound();
    }
}
