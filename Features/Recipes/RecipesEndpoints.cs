using AppChiaSeCongThucNauAnBackend.Features.Recipes.Queries.GetRecentRecipes;
using AppChiaSeCongThucNauAnBackend.Features.Recipes.Queries.GetTrendingRecipes;
using Carter;
using MediatR;

namespace AppChiaSeCongThucNauAnBackend.Features.Recipes;

public class RecipesEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/recipes").WithTags("Recipes");

        group.MapGet("/trending", GetTrendingRecipes)
            .WithName("GetTrendingRecipes");

        group.MapGet("/recent", GetRecentRecipes)
            .WithName("GetRecentRecipes");
    }

    private async Task<IResult> GetTrendingRecipes(ISender sender, int? limit = 10)
    {
        var query = new GetTrendingRecipesQuery { Limit = limit ?? 10 };
        var recipes = await sender.Send(query);
        return Results.Ok(recipes);
    }

    private async Task<IResult> GetRecentRecipes(ISender sender, int? limit = 10)
    {
        var query = new GetRecentRecipesQuery { Limit = limit ?? 10 };
        var recipes = await sender.Send(query);
        return Results.Ok(recipes);
    }
} 