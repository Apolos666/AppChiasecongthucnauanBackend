using AppChiaSeCongThucNauAnBackend.Features.User.Dtos;
using MediatR;

namespace AppChiaSeCongThucNauAnBackend.Features.User.Queries.GetUsers;

public record GetUsersQuery() : IRequest<List<UserDto>>; 