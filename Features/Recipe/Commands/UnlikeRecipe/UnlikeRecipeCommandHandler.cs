using AppChiaSeCongThucNauAnBackend.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AppChiaSeCongThucNauAnBackend.Features.Recipe.Commands.UnlikeRecipe;

public class UnlikeRecipeCommandHandler(AppDbContext context) : IRequestHandler<UnlikeRecipeCommand, bool>
{
    public async Task<bool> Handle(UnlikeRecipeCommand request, CancellationToken cancellationToken)
    {
        var existingLike = await context.RecipeLikes
            .FirstOrDefaultAsync(rl => rl.UserId == request.UserId && rl.RecipeId == request.RecipeId, cancellationToken);

        if (existingLike == null)
        {
            return false; // Chưa like
        }

        context.RecipeLikes.Remove(existingLike);
        await context.SaveChangesAsync(cancellationToken);

        return true;
    }
}