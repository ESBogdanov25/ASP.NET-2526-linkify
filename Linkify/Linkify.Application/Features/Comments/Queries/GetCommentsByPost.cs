using AutoMapper;
using MediatR;
using Linkify.Application.DTOs;
using Linkify.Domain.Interfaces;

namespace Linkify.Application.Features.Comments.Queries;

public class GetCommentsByPostQuery : IRequest<IEnumerable<CommentDto>>
{
    public int PostId { get; set; }
}

public class GetCommentsByPostQueryHandler : IRequestHandler<GetCommentsByPostQuery, IEnumerable<CommentDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetCommentsByPostQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CommentDto>> Handle(GetCommentsByPostQuery request, CancellationToken cancellationToken)
    {
        var comments = await _unitOfWork.Comments.GetCommentsByPostIdAsync(request.PostId);
        return _mapper.Map<IEnumerable<CommentDto>>(comments);
    }
}