using AppChiaSeCongThucNauAnBackend.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AppChiaSeCongThucNauAnBackend.Features.User.Queries.GetFollowStatus;

public class GetFollowStatusQueryHandler : IRequestHandler<GetFollowStatusQuery, bool>
{
    private readonly AppDbContext _context;

    public GetFollowStatusQueryHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(GetFollowStatusQuery request, CancellationToken cancellationToken)
    {
        var existingFollow = await _context.UserFollows
            .AnyAsync(uf => uf.FollowerId == request.FollowerId && uf.FollowingId == request.FollowingId, cancellationToken);

        return existingFollow;
    }
}