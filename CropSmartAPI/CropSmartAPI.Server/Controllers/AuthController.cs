using CropSmartAPI.Server.Commands.Sessions;
using CropSmartAPI.Server.Commands.User;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CropSmartAPI.Server.Controllers;

[ApiController]
[Route("/auth")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("login")]
    public async Task<IActionResult> LogIn([FromBody] LogInQuery query)
    {
        var result = await _mediator.Send(query);

        if (result.IsFailure)
        {
            return NotFound(result.Error);
        }

        return Ok(result.Value);
    }

    [HttpPost("/register")]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserQuery query)
    {
        var result = await _mediator.Send(query);

        if (result.IsFailure)
        {
            return NotFound(result.Error);
        }

        return Ok(result.Value);
    }

    [HttpPost("logout")]
    public async Task<IActionResult> LogOut([FromBody] LogOutQuery query)
    {
        var result = await _mediator.Send(query);

        if (result.IsFailure)
        {
            return NotFound(result.Error);
        }

        return Ok(result.Value);
    }
}