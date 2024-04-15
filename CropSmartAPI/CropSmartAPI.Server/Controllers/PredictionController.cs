﻿using CropSmartAPI.Server.Commands.PredictFertility;
using CropSmartAPI.Server.Commands.User;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CropSmartAPI.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class PredictionController : ControllerBase
{
    private readonly IMediator _mediator;

    public PredictionController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("predictFertility")]
    public async Task<IActionResult> PredictFertility([FromQuery] PredictFertilityQuery query)
    {
        var result = await _mediator.Send(query);

        if (result.IsFailure)
        {
            return NotFound(result.Error);
        }

        return Ok(result.Value);
    }
}