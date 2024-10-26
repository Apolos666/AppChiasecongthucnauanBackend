using AppChiaSeCongThucNauAnBackend.Features.User.Dtos;
using MediatR;

namespace AppChiaSeCongThucNauAnBackend.Features.User.Queries.GetUserById;

public record GetUserByIdQuery(Guid UserId) : IRequest<UserDto>;
