using AppChiaSeCongThucNauAnBackend.Features.User.Dtos;
using MediatR;

namespace AppChiaSeCongThucNauAnBackend.Features.User.Commands.UpdateUser;

public record UpdateUserCommand(Guid UserId, UpdateUserDto UserDto) : IRequest<bool>;
