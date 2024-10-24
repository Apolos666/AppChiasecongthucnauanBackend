using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace AppChiaSeCongThucNauAnBackend.Features.Comment.Hubs;

[Authorize]
public class CommentHub(ILogger<CommentHub> logger) : Hub<ICommentClient>
{
    private readonly ILogger<CommentHub> _logger = logger;

    public async Task JoinRecipeGroup(string recipeId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, $"Recipe_{recipeId}");
        _logger.LogInformation("User {UserId} joined Recipe group {RecipeId}", Context.UserIdentifier, recipeId);
    }

    public async Task LeaveRecipeGroup(string recipeId)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"Recipe_{recipeId}");
        _logger.LogInformation("User {UserId} left Recipe group {RecipeId}", Context.UserIdentifier, recipeId);
    }

    public override async Task OnConnectedAsync()
    {
        _logger.LogInformation("User {UserId} connected to CommentHub", Context.UserIdentifier);
        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        _logger.LogInformation("User {UserId} disconnected from CommentHub", Context.UserIdentifier);
        await base.OnDisconnectedAsync(exception);
    }
}
