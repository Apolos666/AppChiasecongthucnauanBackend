using AppChiaSeCongThucNauAnBackend.Features.Auth.DTOs;
using MediatR;

namespace AppChiaSeCongThucNauAnBackend.Features.User.Queries.GetUser;

public record GetUserQuery(Guid UserId) : IRequest<UserDto>;