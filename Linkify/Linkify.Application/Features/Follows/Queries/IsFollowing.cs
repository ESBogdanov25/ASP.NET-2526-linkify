using MediatR;
using Linkify.Domain.Interfaces;

namespace Linkify.Application.Features.Follows.Queries;

public class IsFollowingQuery : IRequest<bool>
{
    public int FollowerId { get; set; }
    public int FollowedId { get; set; }
}

public class IsFollowingQueryHandler : IRequestHandler<IsFollowingQuery, bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public IsFollowingQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(IsFollowingQuery request, CancellationToken cancellationToken)
    {
        return await _unitOfWork.Follows.IsFollowingAsync(request.FollowerId, request.FollowedId);
    }
}