using CropSmartAPI.Server.Commands.Crop;
using CropSmartAPI.Server.Commands.Field;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CropSmartAPI.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class CropController : ControllerBase
{
    private readonly IMediator _mediator;

    public CropController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("get")]
    public async Task<IActionResult> GetCrop([FromQuery] GetCropQuery query)
    {
        var result = await _mediator.Send(query);

        if (result.IsFailure)
        {
            return NotFound(result.Error);
        }

        return Ok(result.Value);
    }


    [HttpGet("getbyfieldid")]
    public async Task<IActionResult> GetCropsByField([FromQuery] GetCropsByFieldQuery query)
    {
        var result = await _mediator.Send(query);

        if (result.IsFailure)
        {
            return NotFound(result.Error);
        }

        return Ok(result.Value);
    }

    [HttpGet("add")]
    public async Task<IActionResult> AddCrop([FromQuery] AddCropQuery query)
    {
        var result = await _mediator.Send(query);

        if (result.IsFailure)
        {
            return NotFound(result.Error);
        }

        return Ok(result.Value);
    }

    [HttpGet("update")]
    public async Task<IActionResult> UpdateCrop([FromQuery] UpdateCropQuery query)
    {
        var result = await _mediator.Send(query);

        if (result.IsFailure)
        {
            return NotFound(result.Error);
        }

        return Ok(result.Value);
    }

    [HttpGet("delete")]
    public async Task<IActionResult> DeleteCrop([FromQuery] DeleteCropQuery query)
    {
        var result = await _mediator.Send(query);

        if (result.IsFailure)
        {
            return NotFound(result.Error);
        }

        return Ok(result.Value);
    }

}
