using CropSmartAPI.Core.Filters;
using CropSmartAPI.Server.Commands.Crop;
using CropSmartAPI.Server.Commands.Fertilizer;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CropSmartAPI.Server.Controllers;

[ApiController]
[ServiceFilter(typeof(AccessCheckFilter))]
[Route("fertilizers")]
public class FertilizerController: ControllerBase
{
    private readonly IMediator _mediator;

    public FertilizerController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpGet("{Id}")]
    public async Task<IActionResult> GetFertilizer([FromRoute] GetFertilizerQuery query)
    {
        var result = await _mediator.Send(query);

        if (result.IsFailure)
        {
            return NotFound(result.Error);
        }

        return Ok(result.Value);
    }


    [HttpGet]
    public async Task<IActionResult> GetFertilizerByCrop([FromQuery] GetFertilizerByCropQuery query)
    {
        var result = await _mediator.Send(query);

        if (result.IsFailure)
        {
            return NotFound(result.Error);
        }

        return Ok(result.Value);
    }

    [HttpPost]
    public async Task<IActionResult> AddFertilizer([FromBody] AddFertilizerQuery query)
    {
        var result = await _mediator.Send(query);

        if (result.IsFailure)
        {
            return NotFound(result.Error);
        }

        return Ok(result.Value);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateFertilizer([FromBody] UpdateFertilizerQuery query)
    {
        var result = await _mediator.Send(query);

        if (result.IsFailure)
        {
            return NotFound(result.Error);
        }

        return Ok(result.Value);
    }

    [HttpDelete("{Id}")]
    public async Task<IActionResult> DeleteFertilizer([FromRoute] DeleteFertilizerQuery query)
    {
        var result = await _mediator.Send(query);

        if (result.IsFailure)
        {
            return NotFound(result.Error);
        }

        return Ok(result.Value);
    }

}
