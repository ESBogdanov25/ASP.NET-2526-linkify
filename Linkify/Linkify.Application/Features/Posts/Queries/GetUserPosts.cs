using AutoMapper;
using MediatR;
using Linkify.Application.DTOs;
using Linkify.Domain.Interfaces;

namespace Linkify.Application.Features.Posts.Queries;

public class GetUserPostsQuery : IRequest<IEnumerable<PostDto>>
{
    public int UserId { get; set; }
}

public class GetUserPostsQueryHandler : IRequestHandler<GetUserPostsQuery, IEnumerable<PostDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetUserPostsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<PostDto>> Handle(GetUserPostsQuery request, CancellationToken cancellationToken)
    {
        var posts = await _unitOfWork.Posts.GetUserPostsAsync(request.UserId);
        return _mapper.Map<IEnumerable<PostDto>>(posts);
    }
}