using AppChiaSeCongThucNauAnBackend.Features.RecipeCategory.Dtos;
using MediatR;

namespace AppChiaSeCongThucNauAnBackend.Features.RecipeCategory.Queries.GetRecipeCategories;

public record GetRecipeCategoriesQuery : IRequest<List<RecipeCategoryDto>>;
