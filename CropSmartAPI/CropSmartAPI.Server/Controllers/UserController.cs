using CropSmartAPI.Core.Dto;
using CropSmartAPI.Core.Filters;
using CropSmartAPI.Server.Commands.Fertilizer;
using CropSmartAPI.Server.Commands.User;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CropSmartAPI.Server.Controllers;


[ApiController]
[ServiceFilter(typeof(AccessCheckFilter))]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{Id}")]
    public async Task<IActionResult> GetUser([FromRoute] GetUserQuery query)
    {
        var result = await _mediator.Send(query);

        if (result.IsFailure)
        {
            return NotFound(result.Error);
        }

        return Ok(result.Value);
    }

    // TODO прибрати пароль з апдейту бо це відповідальність AuthController-а
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(int id, [FromBody] UpdateUserDto body)
    {
        var query = new UpdateUserQuery { Id = id, Body = body };
        var result = await _mediator.Send(query);

        if (result.IsFailure)
        {
            return NotFound(result.Error);
        }

        return Ok(result.Value);
    }

    // TODO delete має повертати void
    [HttpDelete("{Id}")]
    public async Task<IActionResult> DeleteUser([FromRoute] DeleteUserQuery query)
    {
        var result = await _mediator.Send(query);

        if (result.IsFailure)
        {
            return NotFound(result.Error);
        }

        return Ok(result.Value);
    }
}