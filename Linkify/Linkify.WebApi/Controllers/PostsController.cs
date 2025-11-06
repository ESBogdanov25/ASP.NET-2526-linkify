using MediatR;
using Microsoft.AspNetCore.Mvc;
using Linkify.Application.Features.Posts.Commands;
using Linkify.Application.Features.Posts.Queries;

namespace Linkify.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PostsController : ControllerBase
{
    private readonly IMediator _mediator;

    public PostsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreatePost(CreatePostCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetUserPosts(int userId)
    {
        var query = new GetUserPostsQuery { UserId = userId };
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("feed/{userId}")]
    public async Task<IActionResult> GetFeedPosts(int userId)
    {
        var query = new GetFeedPostsQuery { UserId = userId };
        var result = await _mediator.Send(query);
        return Ok(result);
    }
}