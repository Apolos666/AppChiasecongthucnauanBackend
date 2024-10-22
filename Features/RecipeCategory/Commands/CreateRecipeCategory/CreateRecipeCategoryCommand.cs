using AppChiaSeCongThucNauAnBackend.Features.RecipeCategory.Dtos;
using MediatR;

namespace AppChiaSeCongThucNauAnBackend.Features.RecipeCategory.Commands.CreateRecipeCategory;

public record CreateRecipeCategoryCommand(CreateRecipeCategoryDto CategoryDto) : IRequest<Guid>;
