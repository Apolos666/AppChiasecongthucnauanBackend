using MediatR;

namespace AppChiaSeCongThucNauAnBackend.Features.User.Commands.DeleteUser;

public record DeleteUserCommand(Guid UserId) : IRequest<bool>; 