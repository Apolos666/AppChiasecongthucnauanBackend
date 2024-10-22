using AppChiaSeCongThucNauAnBackend.Data;
using AppChiaSeCongThucNauAnBackend.Features.RecipeCategory.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AppChiaSeCongThucNauAnBackend.Features.RecipeCategory.Queries.GetRecipeCategories;

public class GetRecipeCategoriesQueryHandler : IRequestHandler<GetRecipeCategoriesQuery, List<RecipeCategoryDto>>
{
    private readonly AppDbContext _context;

    public GetRecipeCategoriesQueryHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<RecipeCategoryDto>> Handle(GetRecipeCategoriesQuery request, CancellationToken cancellationToken)
    {
        var categories = await _context.RecipeCategories
            .Select(c => new RecipeCategoryDto
            {
                Id = c.Id,
                CategoryName = c.CategoryName
            })
            .ToListAsync(cancellationToken);

        return categories;
    }
}
