using CropSmartAPI.Core.Filters;
using CropSmartAPI.Server.Commands.Fertilizer;
using CropSmartAPI.Server.Commands.Field;
using CropSmartAPI.Server.Commands.User;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace CropSmartAPI.Server.Controllers;


[ApiController]
[ServiceFilter(typeof(AccessCheckFilter))]
[Route("[controller]")]
public class FieldController: ControllerBase
{
    private readonly IMediator _mediator;

    public FieldController(IMediator mediator)
    {
        _mediator = mediator;
    }

    
    [HttpGet("get")]
    public async Task<IActionResult> GetField([FromHeader(Name = "Key")] string key, [FromQuery] GetFieldQuery query)
    {
        var result = await _mediator.Send(query);

        if (result.IsFailure)
        {
            return NotFound(result.Error);
        }

        return Ok(result.Value);
    }


    [HttpGet("getbyuserid")]
    public async Task<IActionResult> GetFieldsByUser([FromHeader(Name = "Key")] string key, [FromQuery] GetFieldsByUserQuery query)
    {
        var result = await _mediator.Send(query);

        if (result.IsFailure)
        {
            return NotFound(result.Error);
        }

        return Ok(result.Value);
    }

    [HttpPost("add")]
    public async Task<IActionResult> AddField([FromHeader(Name = "Key")] string key, [FromQuery] AddFieldQuery query)
    {
        var result = await _mediator.Send(query);

        if (result.IsFailure)
        {
            return NotFound(result.Error);
        }

        return Ok(result.Value);
    }

    [HttpPut("update")]
    public async Task<IActionResult> UpdateField([FromHeader(Name = "Key")] string key, [FromQuery] UpdateFieldQuery query)
    {
        var result = await _mediator.Send(query);

        if (result.IsFailure)
        {
            return NotFound(result.Error);
        }

        return Ok(result.Value);
    }

    [HttpDelete("delete")]
    public async Task<IActionResult> DeleteField([FromHeader(Name = "Key")] string key, [FromQuery] DeleteFieldQuery query)
    {
        var result = await _mediator.Send(query);

        if (result.IsFailure)
        {
            return NotFound(result.Error);
        }

        return Ok(result.Value);
    }

}
