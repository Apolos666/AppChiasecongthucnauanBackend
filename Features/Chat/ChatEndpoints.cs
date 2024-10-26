using AppChiaSeCongThucNauAnBackend.Features.Chat.Commands.StartConversation;
using AppChiaSeCongThucNauAnBackend.Features.Chat.Commands.SendMessage;
using AppChiaSeCongThucNauAnBackend.Features.Chat.Queries.GetConversations;
using AppChiaSeCongThucNauAnBackend.Features.Chat.Queries.GetMessages;
using Carter;
using MediatR;
using System.Security.Claims;

namespace AppChiaSeCongThucNauAnBackend.Features.Chat;

public class ChatEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/chat").WithTags("Chat").RequireAuthorization();

        group.MapPost("/start", StartConversation);

        group.MapPost("/send", SendMessage);

        group.MapGet("/conversations", GetConversations);

        group.MapGet("/messages/{conversationId}", GetMessages);
    }

    private async Task<IResult> StartConversation(Guid recipientId, HttpContext httpContext, IMediator mediator)
    {
        var userId = Guid.Parse(httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        var command = new StartConversationCommand(userId, recipientId);
        var result = await mediator.Send(command);
        return Results.Ok(result);
    }

    private async Task<IResult> SendMessage(SendMessageRequest request, HttpContext httpContext, IMediator mediator)
    {
        var userId = Guid.Parse(httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        var command = new SendMessageCommand(userId, request.SenderUserName, request.ConversationId, request.Content);
        var result = await mediator.Send(command);
        return Results.Ok(result);
    }

    private async Task<IResult> GetConversations(HttpContext httpContext, IMediator mediator)
    {
        var userId = Guid.Parse(httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        var query = new GetConversationsQuery(userId);
        var result = await mediator.Send(query);
        return Results.Ok(result);
    }

    private async Task<IResult> GetMessages(Guid conversationId, HttpContext httpContext, IMediator mediator)
    {
        var userId = Guid.Parse(httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        var query = new GetMessagesQuery(conversationId, userId);
        var result = await mediator.Send(query);
        return Results.Ok(result);
    }
}

public record SendMessageRequest(string SenderUserName, Guid ConversationId, string Content);
