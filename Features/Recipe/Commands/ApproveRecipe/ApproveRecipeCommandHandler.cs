using AppChiaSeCongThucNauAnBackend.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AppChiaSeCongThucNauAnBackend.Features.Recipe.Commands.ApproveRecipe;

public class ApproveRecipeCommandHandler(AppDbContext context) : IRequestHandler<ApproveRecipeCommand, bool>
{
    public async Task<bool> Handle(ApproveRecipeCommand request, CancellationToken cancellationToken)
    {
        var recipe = await context.Recipes.FirstOrDefaultAsync(r => r.Id == request.RecipeId, cancellationToken);
        
        if (recipe == null)
        {
            return false;
        }

        recipe.IsApproved = true;
        await context.SaveChangesAsync(cancellationToken);
        
        return true;
    }
} 