using MediatR;

namespace AppChiaSeCongThucNauAnBackend.Features.User.Commands.FollowUser;

public record FollowUserCommand(Guid FollowerId, Guid FollowingId) : IRequest<bool>;