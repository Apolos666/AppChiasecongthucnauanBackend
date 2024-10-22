using AppChiaSeCongThucNauAnBackend.Features.Recipe.Commands.CreateRecipe;
using AppChiaSeCongThucNauAnBackend.Features.Recipe.Commands.UpdateRecipe;
using AppChiaSeCongThucNauAnBackend.Features.Recipe.Commands.DeleteRecipe;
using AppChiaSeCongThucNauAnBackend.Features.Recipe.Queries.GetRecipe;
using AppChiaSeCongThucNauAnBackend.Features.Recipe.Queries.GetRecipes;
using AppChiaSeCongThucNauAnBackend.Features.Recipe.Dtos;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AppChiaSeCongThucNauAnBackend.Features.Recipe;

public class RecipeEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/recipes").WithTags("Recipes");

        group.MapPost("/", CreateRecipe)
            .WithName("CreateRecipe")
            .RequireAuthorization();

        group.MapPut("/{id}", UpdateRecipe)
            .WithName("UpdateRecipe")
            .RequireAuthorization();

        group.MapDelete("/{id}", DeleteRecipe)
            .WithName("DeleteRecipe")
            .RequireAuthorization();

        group.MapGet("/{id}", GetRecipe)
            .WithName("GetRecipe")
            .AllowAnonymous();

        group.MapGet("/", GetRecipes)
            .WithName("GetRecipes")
            .AllowAnonymous();
    }

    private async Task<IResult> CreateRecipe(
        [FromForm] CreateRecipeDto recipeDto,
        HttpContext httpContext,
        ISender sender)
    {
        var userId = Guid.Parse(httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        var command = new CreateRecipeCommand(recipeDto, userId);
        var recipeId = await sender.Send(command);
        return Results.Created($"/api/recipes/{recipeId}", recipeId);
    }

    private async Task<IResult> UpdateRecipe(
        Guid id,
        [FromForm] UpdateRecipeDto recipeDto,
        HttpContext httpContext,
        ISender sender)
    {
        var userId = Guid.Parse(httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        var command = new UpdateRecipeCommand(id, recipeDto, userId);
        var result = await sender.Send(command);
        return result ? Results.NoContent() : Results.NotFound();
    }

    private async Task<IResult> DeleteRecipe(
        Guid id,
        HttpContext httpContext,
        ISender sender)
    {
        var userId = Guid.Parse(httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        var command = new DeleteRecipeCommand(id, userId);
        var result = await sender.Send(command);
        return result ? Results.NoContent() : Results.NotFound();
    }

    private async Task<IResult> GetRecipe(Guid id, ISender sender)
    {
        var query = new GetRecipeQuery(id);
        var recipe = await sender.Send(query);
        return recipe != null ? Results.Ok(recipe) : Results.NotFound();
    }

    private async Task<IResult> GetRecipes(ISender sender)
    {
        var query = new GetRecipesQuery();
        var recipes = await sender.Send(query);
        return Results.Ok(recipes);
    }
}
