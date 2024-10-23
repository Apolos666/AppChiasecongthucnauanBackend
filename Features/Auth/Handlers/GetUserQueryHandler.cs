using AppChiaSeCongThucNauAnBackend.Data;
using AppChiaSeCongThucNauAnBackend.Features.Auth.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AppChiaSeCongThucNauAnBackend.Features.User.Queries.GetUser;

public class GetUserQueryHandler : IRequestHandler<GetUserQuery, UserDto>
{
    private readonly AppDbContext _context;

    public GetUserQueryHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<UserDto> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .Where(u => u.Id == request.UserId)
            .Select(u => new UserDto
            {
                Id = u.Id,
                Email = u.Email,
                Name = u.Name,
                Bio = u.Bio,
                SocialMedia = u.SocialMedia
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (user == null)
        {
            throw new Exception("Không tìm thấy người dùng");
        }

        return user;
    }
}