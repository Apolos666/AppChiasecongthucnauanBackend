using AppChiaSeCongThucNauAnBackend.Features.RecipeCategory.Commands.CreateRecipeCategory;
using AppChiaSeCongThucNauAnBackend.Features.RecipeCategory.Commands.UpdateRecipeCategory;
using AppChiaSeCongThucNauAnBackend.Features.RecipeCategory.Commands.DeleteRecipeCategory;
using AppChiaSeCongThucNauAnBackend.Features.RecipeCategory.Queries.GetRecipeCategory;
using AppChiaSeCongThucNauAnBackend.Features.RecipeCategory.Queries.GetRecipeCategories;
using AppChiaSeCongThucNauAnBackend.Features.RecipeCategory.Dtos;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Authorization;

namespace AppChiaSeCongThucNauAnBackend.Features.RecipeCategory;

public class RecipeCategoryEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/recipe-categories").WithTags("Recipe Categories");

        group.MapPost("/", CreateRecipeCategory)
            .WithName("CreateRecipeCategory");

        group.MapPut("/{id}", UpdateRecipeCategory)
            .WithName("UpdateRecipeCategory")
            .RequireAuthorization();

        group.MapDelete("/{id}", DeleteRecipeCategory)
            .WithName("DeleteRecipeCategory")
            .RequireAuthorization();

        group.MapGet("/{id}", GetRecipeCategory)
            .WithName("GetRecipeCategory")
            .AllowAnonymous();

        group.MapGet("/", GetRecipeCategories)
            .WithName("GetRecipeCategories")
            .AllowAnonymous();
    }

    private async Task<IResult> CreateRecipeCategory(
        CreateRecipeCategoryDto categoryDto,
        ISender sender)
    {
        var command = new CreateRecipeCategoryCommand(categoryDto);
        var categoryId = await sender.Send(command);
        return Results.Created($"/api/recipe-categories/{categoryId}", categoryId);
    }

    private async Task<IResult> UpdateRecipeCategory(
        Guid id,
        UpdateRecipeCategoryDto categoryDto,
        ISender sender)
    {
        var command = new UpdateRecipeCategoryCommand(id, categoryDto);
        var result = await sender.Send(command);
        return result ? Results.NoContent() : Results.NotFound();
    }

    private async Task<IResult> DeleteRecipeCategory(
        Guid id,
        ISender sender)
    {
        var command = new DeleteRecipeCategoryCommand(id);
        var result = await sender.Send(command);
        return result ? Results.NoContent() : Results.NotFound();
    }

    private async Task<IResult> GetRecipeCategory(Guid id, ISender sender)
    {
        var query = new GetRecipeCategoryQuery(id);
        var category = await sender.Send(query);
        return category != null ? Results.Ok(category) : Results.NotFound();
    }

    private async Task<IResult> GetRecipeCategories(ISender sender)
    {
        var query = new GetRecipeCategoriesQuery();
        var categories = await sender.Send(query);
        return Results.Ok(categories);
    }
}
