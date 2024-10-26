using AppChiaSeCongThucNauAnBackend.Data;
using AppChiaSeCongThucNauAnBackend.Features.User.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AppChiaSeCongThucNauAnBackend.Features.User.Queries.GetUserById;

public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserDto>
{
    private readonly AppDbContext _context;

    public GetUserByIdQueryHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<UserDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .Where(u => u.Id == request.UserId)
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
            .FirstOrDefaultAsync(cancellationToken);

        if (user == null)
        {
            throw new Exception("Không tìm thấy người dùng");
        }

        return user;
    }
}
