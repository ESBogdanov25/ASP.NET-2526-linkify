using MediatR;
using Microsoft.AspNetCore.Mvc;
using Linkify.Application.Features.Users.Queries;
using Microsoft.AspNetCore.Http;

namespace Linkify.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var query = new GetUserByIdQuery { UserId = id };
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
