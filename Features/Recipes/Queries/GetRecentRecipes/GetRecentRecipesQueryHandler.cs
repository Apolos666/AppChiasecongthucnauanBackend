using AppChiaSeCongThucNauAnBackend.Data;
using AppChiaSeCongThucNauAnBackend.Features.Recipes.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AppChiaSeCongThucNauAnBackend.Features.Recipes.Queries.GetRecentRecipes
{
    public class GetRecentRecipesQueryHandler : IRequestHandler<GetRecentRecipesQuery, IEnumerable<RecipesDto>>
    {
        private readonly AppDbContext _context;

        public GetRecentRecipesQueryHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<RecipesDto>> Handle(GetRecentRecipesQuery request, CancellationToken cancellationToken)
        {
            return await _context.Recipes
                .Include(r => r.User)
                .Include(r => r.RecipeLikes)
                .Include(r => r.RecipeMedia)
                .OrderByDescending(r => r.CreatedAt)
                .Take(request.Limit)
                .Select(r => new RecipesDto
                {
                    Id = r.Id,
                    Title = r.Title,
                    ChefName = r.User.Name,
                    LikesCount = r.RecipeLikes.Count,
                    CreatedAt = r.CreatedAt,
                    IsApproved = r.IsApproved,
                    ImageUrl = r.RecipeMedia.Select(m => m.MediaUrl).FirstOrDefault(),
                    IsTrending = false
                })
                .ToListAsync(cancellationToken);
        }
    }
}
