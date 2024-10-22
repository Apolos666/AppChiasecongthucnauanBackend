using AppChiaSeCongThucNauAnBackend.Features.RecipeCategory.Dtos;
using MediatR;

namespace AppChiaSeCongThucNauAnBackend.Features.RecipeCategory.Commands.UpdateRecipeCategory;

public record UpdateRecipeCategoryCommand(Guid CategoryId, UpdateRecipeCategoryDto CategoryDto) : IRequest<bool>;
