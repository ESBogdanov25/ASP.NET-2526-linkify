using MediatR;
using Linkify.Domain.Interfaces;

namespace Linkify.Application.Features.Comments.Commands;

public class DeleteCommentCommand : IRequest<bool>
{
    public int CommentId { get; set; }
    public int UserId { get; set; } // To verify ownership
}

public class DeleteCommentCommandHandler : IRequestHandler<DeleteCommentCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteCommentCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
    {
        var comment = await _unitOfWork.Comments.GetByIdAsync(request.CommentId);

        if (comment == null)
        {
            throw new Exception("Comment not found.");
        }

        if (comment.UserId != request.UserId)
        {
            throw new Exception("You can only delete your own comments.");
        }

        _unitOfWork.Comments.Delete(comment);
        await _unitOfWork.SaveChangesAsync();

        return true;
    }
}