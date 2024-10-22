using AppChiaSeCongThucNauAnBackend.Features.RecipeCategory.Dtos;
using MediatR;

namespace AppChiaSeCongThucNauAnBackend.Features.RecipeCategory.Queries.GetRecipeCategory;

public record GetRecipeCategoryQuery(Guid CategoryId) : IRequest<RecipeCategoryDto>;
