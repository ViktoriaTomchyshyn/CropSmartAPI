﻿using CropSmartAPI.Server.Commands.Crop;
using CropSmartAPI.Server.Commands.Fertilizer;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CropSmartAPI.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class FertilizerController: ControllerBase
{
    private readonly IMediator _mediator;

    public FertilizerController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpPost("get")]
    public async Task<IActionResult> GetFertilizer([FromQuery] GetFertilizerQuery query)
    {
        var result = await _mediator.Send(query);

        if (result.IsFailure)
        {
            return NotFound(result.Error);
        }

        return Ok(result.Value);
    }


    [HttpPost("getbycropid")]
    public async Task<IActionResult> GetFertilizerByCrop([FromQuery] GetFertilizerByCropQuery query)
    {
        var result = await _mediator.Send(query);

        if (result.IsFailure)
        {
            return NotFound(result.Error);
        }

        return Ok(result.Value);
    }

    [HttpPost("add")]
    public async Task<IActionResult> AddFertilizer([FromQuery] AddFertilizerQuery query)
    {
        var result = await _mediator.Send(query);

        if (result.IsFailure)
        {
            return NotFound(result.Error);
        }

        return Ok(result.Value);
    }

    [HttpPost("update")]
    public async Task<IActionResult> UpdateFertilizer([FromQuery] UpdateFertilizerQuery query)
    {
        var result = await _mediator.Send(query);

        if (result.IsFailure)
        {
            return NotFound(result.Error);
        }

        return Ok(result.Value);
    }

    [HttpPost("delete")]
    public async Task<IActionResult> DeleteFertilizer([FromQuery] DeleteFertilizerQuery query)
    {
        var result = await _mediator.Send(query);

        if (result.IsFailure)
        {
            return NotFound(result.Error);
        }

        return Ok(result.Value);
    }

}
