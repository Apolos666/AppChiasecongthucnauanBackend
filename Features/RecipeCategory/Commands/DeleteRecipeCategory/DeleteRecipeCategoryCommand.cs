using MediatR;

namespace AppChiaSeCongThucNauAnBackend.Features.RecipeCategory.Commands.DeleteRecipeCategory;

public record DeleteRecipeCategoryCommand(Guid CategoryId) : IRequest<bool>;
