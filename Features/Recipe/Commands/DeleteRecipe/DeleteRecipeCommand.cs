using MediatR;

namespace AppChiaSeCongThucNauAnBackend.Features.Recipe.Commands.DeleteRecipe;

public record DeleteRecipeCommand(Guid RecipeId, Guid UserId) : IRequest<bool>;

