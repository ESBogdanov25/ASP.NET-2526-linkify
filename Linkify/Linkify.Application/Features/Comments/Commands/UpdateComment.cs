using AutoMapper;
using MediatR;
using Linkify.Application.DTOs;
using Linkify.Domain.Interfaces;

namespace Linkify.Application.Features.Comments.Commands;

public class UpdateCommentCommand : IRequest<CommentDto>
{
    public int CommentId { get; set; }
    public string Content { get; set; } = string.Empty;
    public int UserId { get; set; } // To verify ownership
}

public class UpdateCommentCommandHandler : IRequestHandler<UpdateCommentCommand, CommentDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateCommentCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<CommentDto> Handle(UpdateCommentCommand request, CancellationToken cancellationToken)
    {
        var comment = await _unitOfWork.Comments.GetByIdAsync(request.CommentId);

        if (comment == null)
        {
            throw new Exception("Comment not found.");
        }

        if (comment.UserId != request.UserId)
        {
            throw new Exception("You can only update your own comments.");
        }

        comment.Content = request.Content;
        comment.UpdatedAt = DateTime.UtcNow;

        _unitOfWork.Comments.Update(comment);
        await _unitOfWork.SaveChangesAsync();

        var updatedComment = await _unitOfWork.Comments.GetByIdAsync(request.CommentId);
        return _mapper.Map<CommentDto>(updatedComment!);
    }
}