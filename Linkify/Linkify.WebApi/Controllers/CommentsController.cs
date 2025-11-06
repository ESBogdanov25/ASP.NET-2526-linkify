using MediatR;
using Microsoft.AspNetCore.Mvc;
using Linkify.Application.Features.Comments.Commands;
using Linkify.Application.Features.Comments.Queries;

namespace Linkify.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CommentsController : ControllerBase
{
    private readonly IMediator _mediator;

    public CommentsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("post/{postId}")]
    public async Task<IActionResult> GetCommentsByPost(int postId)
    {
        var query = new GetCommentsByPostQuery { PostId = postId };
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateComment(CreateCommentCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateComment(int id, UpdateCommentCommand command)
    {
        command.CommentId = id;
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteComment(int id, [FromQuery] int userId)
    {
        var command = new DeleteCommentCommand { CommentId = id, UserId = userId };
        var result = await _mediator.Send(command);
        return Ok(new { deleted = result });
    }
}