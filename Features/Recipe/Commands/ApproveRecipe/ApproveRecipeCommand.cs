using MediatR;

namespace AppChiaSeCongThucNauAnBackend.Features.Recipe.Commands.ApproveRecipe;

public record ApproveRecipeCommand(Guid RecipeId) : IRequest<bool>; 