using AppChiaSeCongThucNauAnBackend.Data;
using AppChiaSeCongThucNauAnBackend.Features.Recipe.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AppChiaSeCongThucNauAnBackend.Features.Recipe.Queries.GetRecipes;

public class GetRecipesQueryHandler : IRequestHandler<GetRecipesQuery, List<RecipeDto>>
{
    private readonly AppDbContext _context;

    public GetRecipesQueryHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<RecipeDto>> Handle(GetRecipesQuery request, CancellationToken cancellationToken)
    {
        var recipes = await _context.Recipes
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
            .ToListAsync(cancellationToken);

        return recipes;
    }
}
