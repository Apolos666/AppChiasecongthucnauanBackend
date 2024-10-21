using AppChiaSeCongThucNauAnBackend.Features.Recipe.Dtos;
using MediatR;

namespace AppChiaSeCongThucNauAnBackend.Features.Recipe.Queries.GetRecipes;

public record GetRecipesQuery : IRequest<List<RecipeDto>>;

