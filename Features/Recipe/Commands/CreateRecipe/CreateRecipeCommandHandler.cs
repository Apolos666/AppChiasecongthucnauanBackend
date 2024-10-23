using AppChiaSeCongThucNauAnBackend.Data;
using AppChiaSeCongThucNauAnBackend.Models;
using AppChiaSeCongThucNauAnBackend.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AppChiaSeCongThucNauAnBackend.Features.Recipe.Commands.CreateRecipe;

public class CreateRecipeCommandHandler : IRequestHandler<CreateRecipeCommand, Guid>
{
    private readonly AppDbContext _context;
    private readonly S3Service _s3Service;

    public CreateRecipeCommandHandler(AppDbContext context, S3Service s3Service)
    {
        _context = context;
        _s3Service = s3Service;
    }

    public async Task<Guid> Handle(CreateRecipeCommand request, CancellationToken cancellationToken)
    {
        var recipe = new Models.Recipe
        {
            Id = Guid.NewGuid(),
            UserId = request.UserId,
            Title = request.RecipeDto.Title,
            Ingredients = request.RecipeDto.Ingredients,
            Instructions = request.RecipeDto.Instructions,
            RecipeCategoryId = request.RecipeDto.RecipeCategoryId,
            CreatedAt = DateTime.UtcNow
        };

        _context.Recipes.Add(recipe);

        foreach (var file in request.Files)
        {
            var mediaUrl = await _s3Service.UploadFileAsync(file);
            var recipeMedia = new RecipeMedia
            {
                Id = Guid.NewGuid(),
                RecipeId = recipe.Id,
                MediaUrl = mediaUrl
            };
            _context.RecipeMedia.Add(recipeMedia);
        }

        await _context.SaveChangesAsync(cancellationToken);

        return recipe.Id;
    }
}
