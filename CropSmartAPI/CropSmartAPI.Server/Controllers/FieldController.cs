﻿using CropSmartAPI.Server.Commands.Fertilizer;
using CropSmartAPI.Server.Commands.Field;
using CropSmartAPI.Server.Commands.User;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace CropSmartAPI.Server.Controllers;


[ApiController]
[Route("[controller]")]
public class FieldController: ControllerBase
{
    private readonly IMediator _mediator;

    public FieldController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("get")]
    public async Task<IActionResult> GetField([FromQuery] GetFieldQuery query)
    {
        var result = await _mediator.Send(query);

        if (result.IsFailure)
        {
            return NotFound(result.Error);
        }

        return Ok(result.Value);
    }


    [HttpGet("getbyuserid")]
    public async Task<IActionResult> GetFieldsByUser([FromQuery] GetFieldsByUserQuery query)
    {
        var result = await _mediator.Send(query);

        if (result.IsFailure)
        {
            return NotFound(result.Error);
        }

        return Ok(result.Value);
    }

    [HttpGet("add")]
    public async Task<IActionResult> AddField([FromQuery] AddFieldQuery query)
    {
        var result = await _mediator.Send(query);

        if (result.IsFailure)
        {
            return NotFound(result.Error);
        }

        return Ok(result.Value);
    }

    [HttpGet("update")]
    public async Task<IActionResult> UpdateField([FromQuery] UpdateFieldQuery query)
    {
        var result = await _mediator.Send(query);

        if (result.IsFailure)
        {
            return NotFound(result.Error);
        }

        return Ok(result.Value);
    }

    [HttpGet("delete")]
    public async Task<IActionResult> DeleteField([FromQuery] DeleteFieldQuery query)
    {
        var result = await _mediator.Send(query);

        if (result.IsFailure)
        {
            return NotFound(result.Error);
        }

        return Ok(result.Value);
    }

}
