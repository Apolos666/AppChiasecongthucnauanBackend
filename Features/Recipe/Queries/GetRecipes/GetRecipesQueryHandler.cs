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
            .Select(r => new RecipeDto
            {
                Id = r.Id,
                UserId = r.UserId,
                Title = r.Title,
                Ingredients = r.Ingredients,
                Instructions = r.Instructions,
                RecipeCategoryId = r.RecipeCategoryId,
                CreatedAt = r.CreatedAt,
                MediaUrls = r.RecipeMedia.Select(rm => rm.MediaUrl).ToList()
            })
            .ToListAsync(cancellationToken);

        return recipes;
    }
}

