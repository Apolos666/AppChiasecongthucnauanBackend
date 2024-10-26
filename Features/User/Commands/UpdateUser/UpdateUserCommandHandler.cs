using AppChiaSeCongThucNauAnBackend.Data;
using MediatR;

namespace AppChiaSeCongThucNauAnBackend.Features.User.Commands.UpdateUser;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, bool>
{
    private readonly AppDbContext _context;

    public UpdateUserCommandHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _context.Users.FindAsync(request.UserId, cancellationToken);

        if (user == null)
        {
            return false;
        }

        user.Name = request.UserDto.Name;
        user.Bio = request.UserDto.Bio;
        user.SocialMedia = request.UserDto.SocialMedia;

        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
