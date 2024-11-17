using MediatR;

namespace AppChiaSeCongThucNauAnBackend.Features.Recipe.Commands.LikeRecipe;

public record LikeRecipeCommand(Guid UserId, Guid RecipeId) : IRequest<bool>;