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
            .FirstOrDefaultAsync(r => r.Id == request.RecipeId, cancellationToken);

        if (recipe == null)
        {
            return null;
        }

        return new RecipeDto
        {
            Id = recipe.Id,
            UserId = recipe.UserId,
            Title = recipe.Title,
            Ingredients = recipe.Ingredients,
            Instructions = recipe.Instructions,
            RecipeCategoryId = recipe.RecipeCategoryId,
            CreatedAt = recipe.CreatedAt,
            MediaUrls = recipe.RecipeMedia.Select(rm => rm.MediaUrl).ToList()
        };
    }
}

