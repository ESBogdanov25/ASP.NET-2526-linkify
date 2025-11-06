using AutoMapper;
using MediatR;
using Linkify.Application.DTOs;
using Linkify.Domain.Entities;
using Linkify.Domain.Interfaces;

namespace Linkify.Application.Features.Posts.Commands;

public class CreatePostCommand : IRequest<PostDto>
{
    public string Content { get; set; } = string.Empty;
    public string? ImageUrl { get; set; }
    public int UserId { get; set; }
}

public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, PostDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreatePostCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<PostDto> Handle(CreatePostCommand request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.Users.GetByIdAsync(request.UserId);
        if (user == null)
        {
            throw new Exception("User not found.");
        }

        var post = new Post
        {
            Content = request.Content,
            ImageUrl = request.ImageUrl,
            UserId = request.UserId,
            CreatedAt = DateTime.UtcNow
        };

        await _unitOfWork.Posts.AddAsync(post);
        await _unitOfWork.SaveChangesAsync();

        // Reload the post with user data
        var createdPost = await _unitOfWork.Posts.GetByIdAsync(post.Id);
        return _mapper.Map<PostDto>(createdPost);
    }
}