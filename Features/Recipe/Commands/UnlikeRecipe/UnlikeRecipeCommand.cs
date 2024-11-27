using MediatR;

namespace AppChiaSeCongThucNauAnBackend.Features.Recipe.Commands.UnlikeRecipe
{
    public record UnlikeRecipeCommand(Guid UserId, Guid RecipeId) : IRequest<bool>;
}