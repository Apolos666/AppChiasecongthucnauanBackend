using MediatR;

namespace AppChiaSeCongThucNauAnBackend.Features.User.Queries.GetFollowStatus;

public record GetFollowStatusQuery(Guid FollowerId, Guid FollowingId) : IRequest<bool>;