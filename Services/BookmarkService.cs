using Microsoft.EntityFrameworkCore;
using AppChiaSeCongThucNauAnBackend.Data;
using AppChiaSeCongThucNauAnBackend.Models;

namespace AppChiaSeCongThucNauAnBackend.Services;

public class BookmarkService : IBookmarkService
{
    private readonly AppDbContext _context;

    public BookmarkService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> AddBookmarkAsync(Guid userId, Guid recipeId)
    {
        var existingBookmark = await _context.Bookmarks
            .FirstOrDefaultAsync(b => b.UserId == userId && b.RecipeId == recipeId);

        if (existingBookmark != null)
        {
            return false;
        }

        var bookmark = new Bookmark
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            RecipeId = recipeId,
            CreatedAt = DateTime.UtcNow
        };

        _context.Bookmarks.Add(bookmark);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> RemoveBookmarkAsync(Guid userId, Guid recipeId)
    {
        var bookmark = await _context.Bookmarks
            .FirstOrDefaultAsync(b => b.UserId == userId && b.RecipeId == recipeId);

        if (bookmark == null)
        {
            return false;
        }

        _context.Bookmarks.Remove(bookmark);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<object>> GetBookmarksAsync(Guid userId)
    {
        return await _context.Bookmarks
            .Where(b => b.UserId == userId)
            .Include(b => b.Recipe)
                .ThenInclude(r => r.RecipeMedia)
            .Include(b => b.Recipe)
                .ThenInclude(r => r.User)
            .Select(b => new
            {
                b.Id,
                b.RecipeId,
                b.Recipe.Title,
                CreatorName = b.Recipe.User.Name,
                MediaUrl = b.Recipe.RecipeMedia
                    .OrderBy(m => m.Id)
                    .Select(m => m.MediaUrl)
                    .FirstOrDefault(),
                b.CreatedAt
            })
            .ToListAsync();
    }

    public async Task<bool> CheckBookmarkStatusAsync(Guid userId, Guid recipeId)
    {
        return await _context.Bookmarks
            .AnyAsync(b => b.UserId == userId && b.RecipeId == recipeId);
    }
} 