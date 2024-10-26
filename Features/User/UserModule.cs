using AppChiaSeCongThucNauAnBackend.Features.User.Queries.GetUserById;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Authorization;

namespace AppChiaSeCongThucNauAnBackend.Features.User;

public class UserModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/users").WithTags("Users");

        group.MapGet("/{id}", GetUserById)
            .WithName("GetUserById")
            .RequireAuthorization();
    }

    private async Task<IResult> GetUserById(Guid id, IMediator mediator)
    {
        var query = new GetUserByIdQuery(id);
        var result = await mediator.Send(query);
        return Results.Ok(result);
    }
}
