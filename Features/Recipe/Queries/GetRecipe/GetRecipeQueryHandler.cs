using AppChiaSeCongThucNauAnBackend.Data;
using AppChiaSeCongThucNauAnBackend.Features.Recipe.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AppChiaSeCongThucNauAnBackend.Features.Recipe.Queries.GetRecipe;

public class GetRecipeQueryHandler : IRequestHandler<GetRecipeQuery, RecipeDto>
{
    private readonly AppDbContext _context;

    public GetRecipeQueryHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<RecipeDto> Handle(GetRecipeQuery request, CancellationToken cancellationToken)
    {
        var recipe = await _context.Recipes
            .Include(r => r.RecipeMedia)
            .Include(r => r.RecipeLikes)
            .Include(r => r.Comments)
                .ThenInclude(c => c.User)
            .Include(r => r.User)
            .AsSplitQuery()
            .Select(r => new RecipeDto
            {
                Id = r.Id,
                UserId = r.UserId,
                UserName = r.User.Name,
                Title = r.Title,
                Ingredients = r.Ingredients,
                Instructions = r.Instructions,
                RecipeCategoryId = r.RecipeCategoryId,
                CreatedAt = r.CreatedAt,
                MediaUrls = r.RecipeMedia.Select(rm => rm.MediaUrl).ToList(),
                LikesCount = r.RecipeLikes.Count,
                Comments = r.Comments.Select(cm => new CommentDto
                {
                    UserId = cm.UserId,
                    UserName = cm.User.Name,
                    Content = cm.Content,
                    CreatedAt = cm.CreatedAt
                }).ToList()
            })
            .FirstOrDefaultAsync(r => r.Id == request.RecipeId, cancellationToken);

        return recipe;
    }
}
