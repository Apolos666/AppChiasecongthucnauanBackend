using AppChiaSeCongThucNauAnBackend.Data;
using AppChiaSeCongThucNauAnBackend.Models;
using AppChiaSeCongThucNauAnBackend.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AppChiaSeCongThucNauAnBackend.Features.Recipe.Commands.UpdateRecipe;

public class UpdateRecipeCommandHandler : IRequestHandler<UpdateRecipeCommand, bool>
{
    private readonly AppDbContext _context;
    private readonly S3Service _s3Service;

    public UpdateRecipeCommandHandler(AppDbContext context, S3Service s3Service)
    {
        _context = context;
        _s3Service = s3Service;
    }

    public async Task<bool> Handle(UpdateRecipeCommand request, CancellationToken cancellationToken)
    {
        var recipe = await _context.Recipes
            .Include(r => r.RecipeMedia)
            .FirstOrDefaultAsync(r => r.Id == request.RecipeId && r.UserId == request.UserId, cancellationToken);

        if (recipe == null)
        {
            return false;
        }

        recipe.Title = request.RecipeDto.Title;
        recipe.Ingredients = request.RecipeDto.Ingredients;
        recipe.Instructions = request.RecipeDto.Instructions;
        recipe.RecipeCategoryId = request.RecipeDto.RecipeCategoryId;

        foreach (var deletedMediaUrl in request.RecipeDto.DeletedMediaUrls)
        {
            var mediaToDelete = recipe.RecipeMedia.FirstOrDefault(rm => rm.MediaUrl == deletedMediaUrl);
            if (mediaToDelete != null)
            {
                _context.RecipeMedia.Remove(mediaToDelete);
                await _s3Service.DeleteFileAsync(deletedMediaUrl);
            }
        }

        foreach (var newMediaFile in request.RecipeDto.NewMediaFiles)
        {
            var mediaUrl = await _s3Service.UploadFileAsync(newMediaFile);
            var recipeMedia = new RecipeMedia
            {
                Id = Guid.NewGuid(),
                RecipeId = recipe.Id,
                MediaUrl = mediaUrl
            };
            _context.RecipeMedia.Add(recipeMedia);
        }

        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}

