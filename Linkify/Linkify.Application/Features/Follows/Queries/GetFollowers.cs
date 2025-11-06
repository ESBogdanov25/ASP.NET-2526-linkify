using AutoMapper;
using MediatR;
using Linkify.Application.DTOs;
using Linkify.Domain.Interfaces;

namespace Linkify.Application.Features.Follows.Queries;

public class GetFollowersQuery : IRequest<IEnumerable<UserDto>>
{
    public int UserId { get; set; }
}

public class GetFollowersQueryHandler : IRequestHandler<GetFollowersQuery, IEnumerable<UserDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetFollowersQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<UserDto>> Handle(GetFollowersQuery request, CancellationToken cancellationToken)
    {
        var followers = await _unitOfWork.Follows.GetFollowersAsync(request.UserId);
        return _mapper.Map<IEnumerable<UserDto>>(followers);
    }
}

public class GetFollowingQuery : IRequest<IEnumerable<UserDto>>
{
    public int UserId { get; set; }
}

public class GetFollowingQueryHandler : IRequestHandler<GetFollowingQuery, IEnumerable<UserDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetFollowingQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<UserDto>> Handle(GetFollowingQuery request, CancellationToken cancellationToken)
    {
        var following = await _unitOfWork.Follows.GetFollowingAsync(request.UserId);
        return _mapper.Map<IEnumerable<UserDto>>(following);
    }
}