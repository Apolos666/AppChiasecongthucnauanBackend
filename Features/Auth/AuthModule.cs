using AppChiaSeCongThucNauAnBackend.Features.Auth.Commands;
using AppChiaSeCongThucNauAnBackend.Features.Auth.DTOs;
using Carter;
using MediatR;

namespace AppChiaSeCongThucNauAnBackend.Features.Auth;

public class AuthModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/auth/register", async (RegisterDto registerDto, IMediator mediator) =>
        {
            var command = new RegisterCommand(registerDto);
            var result = await mediator.Send(command);
            return Results.Ok(result);
        })
        .WithName("Register")
        .WithTags("Authentication")
        .Produces<string>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status400BadRequest);

        app.MapPost("/auth/login", async (LoginDto loginDto, IMediator mediator) =>
        {
            var command = new LoginCommand(loginDto);
            var result = await mediator.Send(command);
            return Results.Ok(result);
        })
        .WithName("Login")
        .WithTags("Authentication")
        .Produces<string>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status400BadRequest);
    }
}
