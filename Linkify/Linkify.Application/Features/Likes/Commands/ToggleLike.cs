using Linkify.Application.Interfaces;
using Linkify.Domain.Entities;
using Linkify.Domain.Interfaces;
using MediatR;

namespace Linkify.Application.Features.Likes.Commands;

public class ToggleLikeCommand : IRequest<bool> // Returns true if liked, false if unliked
{
    public int UserId { get; set; }
    public int PostId { get; set; }
}

public class ToggleLikeCommandHandler : IRequestHandler<ToggleLikeCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly INotificationService _notificationService;

    public ToggleLikeCommandHandler(IUnitOfWork unitOfWork, INotificationService notificationService)
    {
        _unitOfWork = unitOfWork;
        _notificationService = notificationService;
    }

    public async Task<bool> Handle(ToggleLikeCommand request, CancellationToken cancellationToken)
    {
        var existingLike = await _unitOfWork.Likes.GetLikeAsync(request.UserId, request.PostId);

        if (existingLike != null)
        {
            // Unlike - remove the like
            _unitOfWork.Likes.Remove(existingLike);
            await _unitOfWork.SaveChangesAsync();
            return false;
        }
        else
        {
            // Like - create new like
            var like = new Like
            {
                UserId = request.UserId,
                PostId = request.PostId,
                CreatedAt = DateTime.UtcNow
            };

            await _unitOfWork.Likes.AddAsync(like);
            await _unitOfWork.SaveChangesAsync();

            // Send notification to post owner (if not liking own post)
            var post = await _unitOfWork.Posts.GetByIdAsync(request.PostId);
            if (post != null && post.UserId != request.UserId)
            {
                await _notificationService.NotifyNewLikeAsync(post.UserId, request.PostId, request.UserId);
            }

            return true;
        }
    }
}