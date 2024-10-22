using AppChiaSeCongThucNauAnBackend.Data;
using MediatR;

namespace AppChiaSeCongThucNauAnBackend.Features.RecipeCategory.Commands.CreateRecipeCategory;

public class CreateRecipeCategoryCommandHandler : IRequestHandler<CreateRecipeCategoryCommand, Guid>
{
    private readonly AppDbContext _context;

    public CreateRecipeCategoryCommandHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateRecipeCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = new Models.RecipeCategory
        {
            Id = Guid.NewGuid(),
            CategoryName = request.CategoryDto.CategoryName
        };

        _context.RecipeCategories.Add(category);
        await _context.SaveChangesAsync(cancellationToken);

        return category.Id;
    }
}
