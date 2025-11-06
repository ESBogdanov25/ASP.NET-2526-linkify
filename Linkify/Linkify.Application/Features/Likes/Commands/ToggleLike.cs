using MediatR;
using Linkify.Domain.Entities;
using Linkify.Domain.Interfaces;

namespace Linkify.Application.Features.Likes.Commands;

public class ToggleLikeCommand : IRequest<bool> // Returns true if liked, false if unliked
{
    public int UserId { get; set; }
    public int PostId { get; set; }
}

public class ToggleLikeCommandHandler : IRequestHandler<ToggleLikeCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public ToggleLikeCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
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
            return true;
        }
    }
}