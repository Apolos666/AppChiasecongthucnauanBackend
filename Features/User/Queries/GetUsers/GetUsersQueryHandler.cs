using AppChiaSeCongThucNauAnBackend.Data;
using AppChiaSeCongThucNauAnBackend.Features.User.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AppChiaSeCongThucNauAnBackend.Features.User.Queries.GetUsers;

public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, List<UserDto>>
{
    private readonly AppDbContext _context;

    public GetUsersQueryHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<UserDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        return await _context.Users
            .Select(u => new UserDto
            {
                Id = u.Id,
                Email = u.Email,
                Name = u.Name,
                Bio = u.Bio,
                SocialMedia = u.SocialMedia,
                Recipes = u.Recipes.Select(r => new RecipeDto
                {
                    Id = r.Id,
                    Title = r.Title,
                    MediaUrls = r.RecipeMedia.Select(rm => rm.MediaUrl).ToList()
                }).ToList(),
                Following = u.Following.Select(f => new UserFollowDto
                {
                    UserId = f.FollowingId,
                    Name = f.Following.Name
                }).ToList(),
                Followers = u.Followers.Select(f => new UserFollowDto
                {
                    UserId = f.FollowerId,
                    Name = f.Follower.Name
                }).ToList()
            })
            .ToListAsync(cancellationToken);
    }
} 