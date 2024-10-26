using AppChiaSeCongThucNauAnBackend.Data;
using AppChiaSeCongThucNauAnBackend.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AppChiaSeCongThucNauAnBackend.Features.User.Commands.FollowUser;

public class FollowUserCommandHandler : IRequestHandler<FollowUserCommand, bool>
{
    private readonly AppDbContext _context;

    public FollowUserCommandHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(FollowUserCommand request, CancellationToken cancellationToken)
    {
        var existingFollow = await _context.UserFollows
            .FirstOrDefaultAsync(uf => uf.FollowerId == request.FollowerId && uf.FollowingId == request.FollowingId, cancellationToken);

        if (existingFollow != null)
        {
            return false; // Đã theo dõi rồi
        }

        var newFollow = new UserFollow
        {
            FollowerId = request.FollowerId,
            FollowingId = request.FollowingId,
            CreatedAt = DateTime.UtcNow
        };

        _context.UserFollows.Add(newFollow);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}