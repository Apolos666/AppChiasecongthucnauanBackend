using AppChiaSeCongThucNauAnBackend.Data;
using AppChiaSeCongThucNauAnBackend.Features.Recipes.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AppChiaSeCongThucNauAnBackend.Features.Recipes.Queries.GetTrendingRecipes
{
    public class GetTrendingRecipesQueryHandler : IRequestHandler<GetTrendingRecipesQuery, IEnumerable<RecipesDto>>
    {
        private readonly AppDbContext _context;

        public GetTrendingRecipesQueryHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<RecipesDto>> Handle(GetTrendingRecipesQuery request, CancellationToken cancellationToken)
        {
            return await _context.Recipes
                .Include(r => r.User)
                .Include(r => r.RecipeLikes)
                .Include(r => r.RecipeMedia)
                .OrderByDescending(r => r.RecipeLikes.Count)
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
                    IsTrending = true
                })
                .ToListAsync(cancellationToken);
        }
    }
}