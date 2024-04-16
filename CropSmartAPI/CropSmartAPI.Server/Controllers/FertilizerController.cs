using CropSmartAPI.Core.Filters;
using CropSmartAPI.Server.Commands.Crop;
using CropSmartAPI.Server.Commands.Fertilizer;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CropSmartAPI.Server.Controllers;

[ApiController]
[ServiceFilter(typeof(AccessCheckFilter))]
[Route("[controller]")]
public class FertilizerController: ControllerBase
{
    private readonly IMediator _mediator;

    public FertilizerController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpGet("get")]
    public async Task<IActionResult> GetFertilizer([FromHeader(Name = "Key")] string key, [FromQuery] GetFertilizerQuery query)
    {
        var result = await _mediator.Send(query);

        if (result.IsFailure)
        {
            return NotFound(result.Error);
        }

        return Ok(result.Value);
    }


    [HttpGet ("getbycropid")]
    public async Task<IActionResult> GetFertilizerByCrop([FromHeader(Name = "Key")] string key, [FromQuery] GetFertilizerByCropQuery query)
    {
        var result = await _mediator.Send(query);

        if (result.IsFailure)
        {
            return NotFound(result.Error);
        }

        return Ok(result.Value);
    }

    [HttpPost("add")]
    public async Task<IActionResult> AddFertilizer([FromHeader(Name = "Key")] string key, [FromQuery] AddFertilizerQuery query)
    {
        var result = await _mediator.Send(query);

        if (result.IsFailure)
        {
            return NotFound(result.Error);
        }

        return Ok(result.Value);
    }

    [HttpPut("update")]
    public async Task<IActionResult> UpdateFertilizer([FromHeader(Name = "Key")] string key, [FromQuery] UpdateFertilizerQuery query)
    {
        var result = await _mediator.Send(query);

        if (result.IsFailure)
        {
            return NotFound(result.Error);
        }

        return Ok(result.Value);
    }

    [HttpDelete("delete")]
    public async Task<IActionResult> DeleteFertilizer([FromHeader(Name = "Key")] string key, [FromQuery] DeleteFertilizerQuery query)
    {
        var result = await _mediator.Send(query);

        if (result.IsFailure)
        {
            return NotFound(result.Error);
        }

        return Ok(result.Value);
    }

}
