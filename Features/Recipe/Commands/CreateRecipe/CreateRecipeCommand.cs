using AppChiaSeCongThucNauAnBackend.Features.Recipe.Dtos;
using MediatR;

namespace AppChiaSeCongThucNauAnBackend.Features.Recipe.Commands.CreateRecipe;

public record CreateRecipeCommand(CreateRecipeDto RecipeDto, Guid UserId) : IRequest<Guid>;

