using AppChiaSeCongThucNauAnBackend.Features.User.Commands.UpdateUser;
using AppChiaSeCongThucNauAnBackend.Features.User.Queries.GetUserById;
using Carter;
using MediatR;
using System.Security.Claims;

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
}
