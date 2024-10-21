using AppChiaSeCongThucNauAnBackend.Data;
using AppChiaSeCongThucNauAnBackend.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AppChiaSeCongThucNauAnBackend.Features.Recipe.Commands.DeleteRecipe;

public class DeleteRecipeCommandHandler : IRequestHandler<DeleteRecipeCommand, bool>
{
    private readonly AppDbContext _context;
    private readonly S3Service _s3Service;

    public DeleteRecipeCommandHandler(AppDbContext context, S3Service s3Service)
    {
        _context = context;
        _s3Service = s3Service;
    }

    public async Task<bool> Handle(DeleteRecipeCommand request, CancellationToken cancellationToken)
    {
        var recipe = await _context.Recipes
            .Include(r => r.RecipeMedia)
            .FirstOrDefaultAsync(r => r.Id == request.RecipeId && r.UserId == request.UserId, cancellationToken);

        if (recipe == null)
        {
            return false;
        }

        foreach (var media in recipe.RecipeMedia)
        {
            await _s3Service.DeleteFileAsync(media.MediaUrl);
        }

        _context.Recipes.Remove(recipe);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}

