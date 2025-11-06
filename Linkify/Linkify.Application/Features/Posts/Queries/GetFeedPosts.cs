using AutoMapper;
using MediatR;
using Linkify.Application.DTOs;
using Linkify.Domain.Interfaces;

namespace Linkify.Application.Features.Posts.Queries;

public class GetFeedPostsQuery : IRequest<IEnumerable<PostDto>>
{
    public int UserId { get; set; }
}

public class GetFeedPostsQueryHandler : IRequestHandler<GetFeedPostsQuery, IEnumerable<PostDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetFeedPostsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<PostDto>> Handle(GetFeedPostsQuery request, CancellationToken cancellationToken)
    {
        var posts = await _unitOfWork.Posts.GetFeedPostsAsync(request.UserId);
        return _mapper.Map<IEnumerable<PostDto>>(posts);
    }
}