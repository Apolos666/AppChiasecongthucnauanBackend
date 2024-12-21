using AppChiaSeCongThucNauAnBackend.Data;
using AppChiaSeCongThucNauAnBackend.Models;
using AppChiaSeCongThucNauAnBackend.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AppChiaSeCongThucNauAnBackend.Features.Recipe.Commands.UpdateRecipe;

public class UpdateRecipeCommandHandler : IRequestHandler<UpdateRecipeCommand, bool>
{
    private readonly AppDbContext _context;

    public UpdateRecipeCommandHandler(AppDbContext context, S3Service s3Service)
    {
        _context = context;
    }

    public async Task<bool> Handle(UpdateRecipeCommand request, CancellationToken cancellationToken)
    {
        var recipe = await _context.Recipes
            .Include(r => r.RecipeMedia)
            .FirstOrDefaultAsync(r => r.Id == request.RecipeId, cancellationToken);

        if (recipe == null)
        {
            return false;
        }

        recipe.Title = request.RecipeDto.Title;
        recipe.Ingredients = request.RecipeDto.Ingredients;
        recipe.Instructions = request.RecipeDto.Instructions;
        recipe.IsApproved = request.RecipeDto.IsApproved;

        foreach (var removedUrl in request.RecipeDto.RemovedMediaUrls)
        {
            var mediaToRemove = recipe.RecipeMedia.FirstOrDefault(m => m.MediaUrl == removedUrl);
            if (mediaToRemove != null)
            {
                _context.RecipeMedia.Remove(mediaToRemove);
            }
        }

        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}

