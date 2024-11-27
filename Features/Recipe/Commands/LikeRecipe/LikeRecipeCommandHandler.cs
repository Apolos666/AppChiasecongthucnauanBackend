using AppChiaSeCongThucNauAnBackend.Data;
using AppChiaSeCongThucNauAnBackend.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AppChiaSeCongThucNauAnBackend.Features.Recipe.Commands.LikeRecipe;

public class LikeRecipeCommandHandler(AppDbContext context) : IRequestHandler<LikeRecipeCommand, bool>
{
    public async Task<bool> Handle(LikeRecipeCommand request, CancellationToken cancellationToken)
    {
        var existingLike = await context.RecipeLikes
            .FirstOrDefaultAsync(rl => rl.UserId == request.UserId && rl.RecipeId == request.RecipeId, cancellationToken);

        if (existingLike != null)
        {
            return false; // Đã like rồi
        }

        var recipeLike = new RecipeLike
        {
            UserId = request.UserId,
            RecipeId = request.RecipeId
        };

        context.RecipeLikes.Add(recipeLike);
        await context.SaveChangesAsync(cancellationToken);

        return true;
    }
}