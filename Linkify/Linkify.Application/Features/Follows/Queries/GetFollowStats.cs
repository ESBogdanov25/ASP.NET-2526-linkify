using MediatR;
using Linkify.Domain.Interfaces;

namespace Linkify.Application.Features.Follows.Queries;

public class GetFollowStatsQuery : IRequest<FollowStatsDto>
{
    public int UserId { get; set; }
}

public class FollowStatsDto
{
    public int FollowerCount { get; set; }
    public int FollowingCount { get; set; }
}

public class GetFollowStatsQueryHandler : IRequestHandler<GetFollowStatsQuery, FollowStatsDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetFollowStatsQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<FollowStatsDto> Handle(GetFollowStatsQuery request, CancellationToken cancellationToken)
    {
        var followerCount = await _unitOfWork.Follows.GetFollowerCountAsync(request.UserId);
        var followingCount = await _unitOfWork.Follows.GetFollowingCountAsync(request.UserId);

        return new FollowStatsDto
        {
            FollowerCount = followerCount,
            FollowingCount = followingCount
        };
    }
}