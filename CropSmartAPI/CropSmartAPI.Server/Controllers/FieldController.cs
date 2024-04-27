using CropSmartAPI.Core.Filters;
using CropSmartAPI.Server.Commands.Field;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CropSmartAPI.Server.Controllers;

[ApiController]
[ServiceFilter(typeof(AccessCheckFilter))]
[Route("fields")]
public class FieldController : ControllerBase
{
    private readonly IMediator _mediator;

    public FieldController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpGet]
    public async Task<IActionResult> GetFields([FromQuery] GetFieldsByUserQuery query)
    {
        var result = await _mediator.Send(query);

        if (result.IsFailure)
        {
            return NotFound(result.Error);
        }

        return Ok(result.Value);
    }

    [HttpGet("{Id}")]
    public async Task<IActionResult> GetField([FromRoute] GetFieldQuery query)
    {
        var result = await _mediator.Send(query);

        if (result.IsFailure)
        {
            return NotFound(result.Error);
        }

        return Ok(result.Value);
    }


    [HttpPost]
    public async Task<IActionResult> AddField([FromBody] AddFieldQuery query)
    {
        var result = await _mediator.Send(query);

        if (result.IsFailure)
        {
            return NotFound(result.Error);
        }

        return Ok(result.Value);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateField([FromBody] UpdateFieldQuery query)
    {
        var result = await _mediator.Send(query);

        if (result.IsFailure)
        {
            return NotFound(result.Error);
        }

        return Ok(result.Value);
    }

    [HttpDelete("{Id}")]
    public async Task<IActionResult> DeleteField([FromRoute] DeleteFieldQuery query)
    {
        var result = await _mediator.Send(query);

        if (result.IsFailure)
        {
            return NotFound(result.Error);
        }

        return Ok(result.Value);
    }
}