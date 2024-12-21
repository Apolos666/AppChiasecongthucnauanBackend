using AppChiaSeCongThucNauAnBackend.Features.Recipe.Commands.CreateRecipe;
using AppChiaSeCongThucNauAnBackend.Features.Recipe.Commands.UpdateRecipe;
using AppChiaSeCongThucNauAnBackend.Features.Recipe.Commands.DeleteRecipe;
using AppChiaSeCongThucNauAnBackend.Features.Recipe.Queries.GetRecipe;
using AppChiaSeCongThucNauAnBackend.Features.Recipe.Queries.GetRecipes;
using AppChiaSeCongThucNauAnBackend.Features.Recipe.Dtos;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using AppChiaSeCongThucNauAnBackend.Features.Recipe.Commands.LikeRecipe;
using AppChiaSeCongThucNauAnBackend.Features.Recipe.Commands.UnlikeRecipe;

namespace AppChiaSeCongThucNauAnBackend.Features.Recipe;

public class RecipeEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/recipes").WithTags("Recipes");

        group.MapPost("/", CreateRecipe)
            .WithName("CreateRecipe")
            .RequireAuthorization()
            .DisableAntiforgery();

        group.MapPut("/{id}", UpdateRecipe)
            .WithName("UpdateRecipe")
            .RequireAuthorization()
            .DisableAntiforgery();

        group.MapDelete("/{id}", DeleteRecipe)
            .WithName("DeleteRecipe")
            .RequireAuthorization()
            .DisableAntiforgery();

        group.MapGet("/{id}", GetRecipe)
            .WithName("GetRecipe")
            .RequireAuthorization();

        group.MapGet("/", GetRecipes)
            .WithName("GetRecipes")
            .AllowAnonymous();

        group.MapPost("/{id}/like", LikeRecipe)
            .WithName("LikeRecipe")
            .RequireAuthorization();

        group.MapDelete("/{id}/unlike", UnlikeRecipe)
            .WithName("UnlikeRecipe")
            .RequireAuthorization();
    }

    private async Task<IResult> CreateRecipe(
        [FromForm] CreateRecipeDto recipeDto,
        [FromForm] IFormFileCollection files,
        HttpContext httpContext,
        ISender sender)
    {
        var userId = Guid.Parse(httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        var command = new CreateRecipeCommand(recipeDto, files, userId);
        var recipeId = await sender.Send(command);
        return Results.Created($"/api/recipes/{recipeId}", recipeId);
    }

    private async Task<IResult> UpdateRecipe(
        Guid id,
        [FromBody] UpdateRecipeDto recipeDto, // Thay đổi [FromForm] thành [FromBody]
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

    private async Task<IResult> GetRecipe(Guid id, HttpContext httpContext, ISender sender)
    {
        Guid? currentUserId = null;
        if (httpContext.User.Identity?.IsAuthenticated == true)
        {
            currentUserId = Guid.Parse(httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        }
        var query = new GetRecipeQuery(id, currentUserId);
        var recipe = await sender.Send(query);
        return recipe != null ? Results.Ok(recipe) : Results.NotFound();
    }

    private async Task<IResult> GetRecipes(ISender sender)
    {
        var query = new GetRecipesQuery();
        var recipes = await sender.Send(query);
        return Results.Ok(recipes);
    }

    private async Task<IResult> LikeRecipe(
        Guid id,
        HttpContext httpContext,
        ISender sender)
    {
        var userId = Guid.Parse(httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        var command = new LikeRecipeCommand(userId, id);
        var result = await sender.Send(command);
        return result ? Results.Ok() : Results.BadRequest("Đã like recipe này rồi");
    }

    private async Task<IResult> UnlikeRecipe(
        Guid id,
        HttpContext httpContext,
        ISender sender)
    {
        var userId = Guid.Parse(httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        var command = new UnlikeRecipeCommand(userId, id);
        var result = await sender.Send(command);
        return result ? Results.Ok() : Results.BadRequest("Chưa like recipe này");
    }
}
