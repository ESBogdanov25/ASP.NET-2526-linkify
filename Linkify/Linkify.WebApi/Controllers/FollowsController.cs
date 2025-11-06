using MediatR;
using Microsoft.AspNetCore.Mvc;
using Linkify.Application.Features.Follows.Commands;
using Linkify.Application.Features.Follows.Queries;

namespace Linkify.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FollowsController : ControllerBase
{
    private readonly IMediator _mediator;

    public FollowsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("toggle")]
    public async Task<IActionResult> ToggleFollow(ToggleFollowCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(new { following = result });
    }

    [HttpGet("stats/{userId}")]
    public async Task<IActionResult> GetFollowStats(int userId)
    {
        var query = new GetFollowStatsQuery { UserId = userId };
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("followers/{userId}")]
    public async Task<IActionResult> GetFollowers(int userId)
    {
        var query = new GetFollowersQuery { UserId = userId };
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("following/{userId}")]
    public async Task<IActionResult> GetFollowing(int userId)
    {
        var query = new GetFollowingQuery { UserId = userId };
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("is-following/{followerId}/{followedId}")]
    public async Task<IActionResult> IsFollowing(int followerId, int followedId)
    {
        var query = new IsFollowingQuery { FollowerId = followerId, FollowedId = followedId };
        var result = await _mediator.Send(query);
        return Ok(new { isFollowing = result });
    }
}