using MediatR;
using Linkify.Domain.Entities;
using Linkify.Domain.Interfaces;

namespace Linkify.Application.Features.Follows.Commands;

public class ToggleFollowCommand : IRequest<bool> // Returns true if followed, false if unfollowed
{
    public int FollowerId { get; set; }  // The user who is following
    public int FollowedId { get; set; }  // The user to follow/unfollow
}

public class ToggleFollowCommandHandler : IRequestHandler<ToggleFollowCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public ToggleFollowCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(ToggleFollowCommand request, CancellationToken cancellationToken)
    {
        // Prevent users from following themselves
        if (request.FollowerId == request.FollowedId)
        {
            throw new Exception("You cannot follow yourself.");
        }

        var existingFollow = await _unitOfWork.Follows.GetFollowAsync(request.FollowerId, request.FollowedId);

        if (existingFollow != null)
        {
            // Unfollow - remove the follow relationship
            _unitOfWork.Follows.Remove(existingFollow);
            await _unitOfWork.SaveChangesAsync();
            return false;
        }
        else
        {
            // Follow - create new follow relationship
            var follow = new Follow
            {
                FollowerId = request.FollowerId,
                FollowedId = request.FollowedId,
                CreatedAt = DateTime.UtcNow
            };

            await _unitOfWork.Follows.AddAsync(follow);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}