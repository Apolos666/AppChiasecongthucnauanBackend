using AppChiaSeCongThucNauAnBackend.Features.Recipes.Dtos;
using MediatR;

namespace AppChiaSeCongThucNauAnBackend.Features.Recipes.Queries.GetTrendingRecipes
{
    public record GetTrendingRecipesQuery : IRequest<IEnumerable<RecipesDto>>
    {
        public int Limit { get; init; } = 10;
    }
} 