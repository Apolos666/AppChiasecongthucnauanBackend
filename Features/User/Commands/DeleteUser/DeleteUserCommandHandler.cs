using AppChiaSeCongThucNauAnBackend.Data;
using MediatR;

namespace AppChiaSeCongThucNauAnBackend.Features.User.Commands.DeleteUser;

public class DeleteUserCommandHandler(AppDbContext context) : IRequestHandler<DeleteUserCommand, bool>
{
    public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await context.Users.FindAsync(request.UserId, cancellationToken);

        if (user == null)
        {
            return false;
        }

        var userFollows = context.UserFollows.Where(uf => uf.FollowerId == request.UserId || uf.FollowingId == request.UserId);
        context.UserFollows.RemoveRange(userFollows);

        context.Users.Remove(user);
        await context.SaveChangesAsync(cancellationToken);

        return true;
    }
} 