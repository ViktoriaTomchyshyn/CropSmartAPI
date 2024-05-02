using CropSmartAPI.Core.Filters;
using CropSmartAPI.Server.Commands.Crop;
using CropSmartAPI.Server.Commands.Field;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CropSmartAPI.Server.Controllers;

[ApiController]
[ServiceFilter(typeof(AccessCheckFilter))]
[Route("crops")]
public class CropController : ControllerBase
{
    private readonly IMediator _mediator;

    public CropController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{Id}")]
    public async Task<IActionResult> GetCrop([FromRoute] GetCropQuery query)
    {
        var result = await _mediator.Send(query);

        if (result.IsFailure)
        {
            return NotFound(result.Error);
        }

        return Ok(result.Value);
    }


    [HttpGet]
    public async Task<IActionResult> GetCropsByField([FromQuery] GetCropsByFieldQuery query)
    {
        var result = await _mediator.Send(query);

        if (result.IsFailure)
        {
            return NotFound(result.Error);
        }

        return Ok(result.Value);
    }

    [HttpPost]
    public async Task<IActionResult> AddCrop([FromBody] AddCropQuery query)
    {
        var result = await _mediator.Send(query);

        if (result.IsFailure)
        {
            return NotFound(result.Error);
        }

        return Ok(result.Value);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateCrop([FromBody] UpdateCropQuery query)
    {
        var result = await _mediator.Send(query);

        if (result.IsFailure)
        {
            return NotFound(result.Error);
        }

        return Ok(result.Value);
    }

    [HttpDelete("{Id}")]
    public async Task<IActionResult> DeleteCrop([FromRoute] DeleteCropQuery query)
    {
        var result = await _mediator.Send(query);

        if (result.IsFailure)
        {
            return NotFound(result.Error);
        }

        return Ok(result.Value);
    }

}
