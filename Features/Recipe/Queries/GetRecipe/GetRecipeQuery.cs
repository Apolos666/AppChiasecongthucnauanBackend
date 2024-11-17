using AppChiaSeCongThucNauAnBackend.Features.Recipe.Dtos;
using MediatR;

namespace AppChiaSeCongThucNauAnBackend.Features.Recipe.Queries.GetRecipe;

public record GetRecipeQuery(Guid RecipeId, Guid? CurrentUserId = null) : IRequest<RecipeDto>;

