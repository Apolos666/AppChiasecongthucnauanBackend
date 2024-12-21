using AppChiaSeCongThucNauAnBackend.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AppChiaSeCongThucNauAnBackend.Features.Recipe.Commands.DeleteRecipe;

public class DeleteRecipeCommandHandler(AppDbContext context) : IRequestHandler<DeleteRecipeCommand, bool>
{
    public async Task<bool> Handle(DeleteRecipeCommand request, CancellationToken cancellationToken)
    {
        var recipe = await context.Recipes
            .FirstOrDefaultAsync(r => r.Id == request.RecipeId, cancellationToken);

        if (recipe == null)
        {
            return false;
        }

        context.Recipes.Remove(recipe);
        await context.SaveChangesAsync(cancellationToken);

        return true;
    }
}

