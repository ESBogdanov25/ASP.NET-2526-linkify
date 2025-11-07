using AutoMapper;
using Linkify.Application.DTOs;
using Linkify.Application.Interfaces;
using Linkify.Domain.Entities;
using Linkify.Domain.Interfaces;
using MediatR;

namespace Linkify.Application.Features.Comments.Commands;

public class CreateCommentCommand : IRequest<CommentDto>
{
    public string Content { get; set; } = string.Empty;
    public int PostId { get; set; }
    public int UserId { get; set; }
}

public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, CommentDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly INotificationService _notificationService;

    public CreateCommentCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, INotificationService notificationService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _notificationService = notificationService;
    }

    public async Task<CommentDto> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.Users.GetByIdAsync(request.UserId);
        var post = await _unitOfWork.Posts.GetByIdAsync(request.PostId);

        if (user == null || post == null)
        {
            throw new Exception("User or Post not found.");
        }

        var comment = new Comment
        {
            Content = request.Content,
            UserId = request.UserId,
            PostId = request.PostId,
            CreatedAt = DateTime.UtcNow
        };

        await _unitOfWork.Comments.AddAsync(comment);
        await _unitOfWork.SaveChangesAsync();

        // Send notification to post owner (if not commenting on own post)
        if (post.UserId != request.UserId)
        {
            await _notificationService.NotifyNewCommentAsync(post.UserId, request.PostId, request.UserId);
        }

        // Reload the comment with user data
        var createdComment = await _unitOfWork.Comments.GetByIdAsync(comment.Id);
        return _mapper.Map<CommentDto>(createdComment!);
    }
}