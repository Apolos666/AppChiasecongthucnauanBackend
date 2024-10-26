using AppChiaSeCongThucNauAnBackend.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AppChiaSeCongThucNauAnBackend.Features.User.Commands.UnfollowUser;

public class UnfollowUserCommandHandler : IRequestHandler<UnfollowUserCommand, bool>
{
    private readonly AppDbContext _context;

    public UnfollowUserCommandHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(UnfollowUserCommand request, CancellationToken cancellationToken)
    {
        var existingFollow = await _context.UserFollows
            .FirstOrDefaultAsync(uf => uf.FollowerId == request.FollowerId && uf.FollowingId == request.FollowingId, cancellationToken);

        if (existingFollow == null)
        {
            return false; // Chưa theo dõi
        }

        _context.UserFollows.Remove(existingFollow);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}