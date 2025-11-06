using AutoMapper;
using MediatR;
using Linkify.Application.DTOs;
using Linkify.Domain.Entities;
using Linkify.Domain.Interfaces;

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

    public CreateCommentCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
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

        // Reload the comment with user data
        var createdComment = await _unitOfWork.Comments.GetByIdAsync(comment.Id);
        return _mapper.Map<CommentDto>(createdComment!);
    }
}