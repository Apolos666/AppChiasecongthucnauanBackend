using AppChiaSeCongThucNauAnBackend.Features.Recipe.Dtos;
using MediatR;

namespace AppChiaSeCongThucNauAnBackend.Features.Recipe.Commands.UpdateRecipe;

public record UpdateRecipeCommand(Guid RecipeId, UpdateRecipeDto RecipeDto, Guid UserId) : IRequest<bool>;

