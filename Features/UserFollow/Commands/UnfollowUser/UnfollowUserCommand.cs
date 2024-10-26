using MediatR;

namespace AppChiaSeCongThucNauAnBackend.Features.User.Commands.UnfollowUser;

public record UnfollowUserCommand(Guid FollowerId, Guid FollowingId) : IRequest<bool>;