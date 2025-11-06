using MediatR;
using Microsoft.AspNetCore.Mvc;
using Linkify.Application.Features.Likes.Commands;

namespace Linkify.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LikesController : ControllerBase
{
    private readonly IMediator _mediator;

    public LikesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("toggle")]
    public async Task<IActionResult> ToggleLike(ToggleLikeCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(new { liked = result });
    }
}