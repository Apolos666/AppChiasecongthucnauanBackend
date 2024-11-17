using AppChiaSeCongThucNauAnBackend.Features.Recipes.Dtos;
using MediatR;

namespace AppChiaSeCongThucNauAnBackend.Features.Recipes.Queries.GetRecentRecipes
{
    public record GetRecentRecipesQuery : IRequest<IEnumerable<RecipesDto>>
    {
        public int Limit { get; init; } = 10;
    }
} 