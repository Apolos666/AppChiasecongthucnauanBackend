using AppChiaSeCongThucNauAnBackend.Services;
using Carter;
using MediatR;
using System.Security.Claims;

namespace AppChiaSeCongThucNauAnBackend.Features.Bookmark;

public class BookmarkEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/bookmarks").WithTags("Bookmarks");

        group.MapPost("/{recipeId}", AddBookmark)
            .WithName("AddBookmark")
            .RequireAuthorization();

        group.MapDelete("/{recipeId}", RemoveBookmark)
            .WithName("RemoveBookmark")
            .RequireAuthorization();

        group.MapGet("/", GetBookmarks)
            .WithName("GetBookmarks")
            .RequireAuthorization();

        group.MapGet("/check/{recipeId}", CheckBookmarkStatus)
            .WithName("CheckBookmarkStatus")
            .RequireAuthorization();
    }

    private async Task<IResult> AddBookmark(
        Guid recipeId,
        HttpContext httpContext,
        IBookmarkService bookmarkService)
    {
        var userId = Guid.Parse(httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        var result = await bookmarkService.AddBookmarkAsync(userId, recipeId);
        return result ? Results.Ok() : Results.BadRequest("Đã bookmark công thức này rồi");
    }

    private async Task<IResult> RemoveBookmark(
        Guid recipeId,
        HttpContext httpContext,
        IBookmarkService bookmarkService)
    {
        var userId = Guid.Parse(httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        var result = await bookmarkService.RemoveBookmarkAsync(userId, recipeId);
        return result ? Results.Ok() : Results.BadRequest("Chưa bookmark công thức này");
    }

    private async Task<IResult> GetBookmarks(
        HttpContext httpContext,
        IBookmarkService bookmarkService)
    {
        var userId = Guid.Parse(httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        var bookmarks = await bookmarkService.GetBookmarksAsync(userId);
        return Results.Ok(bookmarks);
    }

    private async Task<IResult> CheckBookmarkStatus(
        Guid recipeId,
        HttpContext httpContext,
        IBookmarkService bookmarkService)
    {
        var userId = Guid.Parse(httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        var isBookmarked = await bookmarkService.CheckBookmarkStatusAsync(userId, recipeId);
        return Results.Ok(new { isBookmarked });
    }
} 