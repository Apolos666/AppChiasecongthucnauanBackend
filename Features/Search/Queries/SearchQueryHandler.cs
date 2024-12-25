using AppChiaSeCongThucNauAnBackend.Data;
using AppChiaSeCongThucNauAnBackend.Features.Search.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;



namespace AppChiaSeCongThucNauAnBackend.Features.Search.Queries;
public class SearchQueryHandler : IRequestHandler<SearchQuery, SearchResultDto>
{
    private readonly AppDbContext _context;
    public SearchQueryHandler(AppDbContext context)
    {
        _context = context;
    }
    public async Task<SearchResultDto> Handle(SearchQuery request, CancellationToken cancellationToken)
    {
        var searchTerm = request.SearchTerm.ToLower();
        var result = new SearchResultDto();

        // Tìm kiếm users
        result.Users = await _context.Users
            .Where(u => u.Name.ToLower().Contains(searchTerm))
            .Select(u => new UserSearchResultDto
            {
                Id = u.Id,
                Name = u.Name
            })
            .Take(5)
            .ToListAsync(cancellationToken);

        // Tìm kiếm recipes
        result.Recipes = await _context.Recipes
            .Include(r => r.User)
            .Include(r => r.RecipeMedia)
            .Where(r => r.Title.ToLower().Contains(searchTerm) ||
                   r.Ingredients.ToLower().Contains(searchTerm))
            .Select(r => new RecipeSearchResultDto
            {
                Id = r.Id,
                Title = r.Title,
                ChefName = r.User.Name,
                IsApproved = r.IsApproved,
                ThumbnailUrl = r.RecipeMedia
                .Select(m => m.MediaUrl)
                .FirstOrDefault()
            })
            .Take(5)
            .ToListAsync(cancellationToken);

        return result;
    }
}