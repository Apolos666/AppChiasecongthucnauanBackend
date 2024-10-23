using System.Security.Claims;
using AppChiaSeCongThucNauAnBackend.Features.Auth.Commands;
using AppChiaSeCongThucNauAnBackend.Features.Auth.DTOs;
using AppChiaSeCongThucNauAnBackend.Features.User.Queries.GetUser;
using Carter;
using MediatR;

namespace AppChiaSeCongThucNauAnBackend.Features.Auth;

public class AuthModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/auth").WithTags("Authentication");

        group.MapPost("/register", async (RegisterDto registerDto, IMediator mediator) =>
        {
            var command = new RegisterCommand(registerDto);
            var result = await mediator.Send(command);
            return Results.Ok(result);
        })
        .WithName("Register")
        .Produces<string>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status400BadRequest);

        group.MapPost("/login", async (LoginDto loginDto, IMediator mediator) =>
        {
            var command = new LoginCommand(loginDto);
            var result = await mediator.Send(command);
            return Results.Ok(result);
        })
        .WithName("Login")
        .Produces<string>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status400BadRequest);

        group.MapGet("/user", async (HttpContext httpContext, IMediator mediator) =>
        {
            var userId = Guid.Parse(httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var query = new GetUserQuery(userId);
            var result = await mediator.Send(query);
            return Results.Ok(result);
        })
        .WithName("User")
        .RequireAuthorization();
    }
}
