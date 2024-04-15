using CropSmartAPI.Core.Filters;
using CropSmartAPI.Core.Services.Interfaces;
using CropSmartAPI.Server.Commands.Fertilizer;
using CropSmartAPI.Server.Commands.Field;
using CropSmartAPI.Server.Commands.User;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;

namespace CropSmartAPI.Server.Controllers;


[ApiController]
[Route("[controller]")]


public class FieldController: ControllerBase
{
    private readonly IMediator _mediator;
    private ISessionControlService _sessionControlService;

    public FieldController(IMediator mediator, ISessionControlService sessionControlService)
    {
        _mediator = mediator;
        _sessionControlService = sessionControlService;
    }


    [HttpPost("get")]
    [ServiceFilter(typeof(AccessCheckFilter))]

    public async Task<IActionResult> GetField([FromHeader(Name = "Key")] string key, [FromQuery] GetFieldQuery query)
    {
        var result = await _mediator.Send(query);

        if (result.IsFailure)
        {
            return NotFound(result.Error);
        }

        return Ok(result.Value);
    }


    [HttpPost("getbyuserid")]
    public async Task<IActionResult> GetFieldsByUser([FromQuery] GetFieldsByUserQuery query)
    {
        var result = await _mediator.Send(query);

        if (result.IsFailure)
        {
            return NotFound(result.Error);
        }

        return Ok(result.Value);
    }

    [HttpPost("add")]
    public async Task<IActionResult> AddField([FromQuery] AddFieldQuery query)
    {
        var result = await _mediator.Send(query);

        if (result.IsFailure)
        {
            return NotFound(result.Error);
        }

        return Ok(result.Value);
    }

    [HttpPost("update")]
    public async Task<IActionResult> UpdateField([FromQuery] UpdateFieldQuery query)
    {
        var result = await _mediator.Send(query);

        if (result.IsFailure)
        {
            return NotFound(result.Error);
        }

        return Ok(result.Value);
    }

    [HttpPost("delete")]
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
