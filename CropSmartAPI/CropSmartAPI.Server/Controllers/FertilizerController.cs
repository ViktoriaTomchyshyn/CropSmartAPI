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

    [HttpGet ("getbycropid")]
    public async Task<IActionResult> GetFertilizer([FromQuery] GetFertilizerQuery query)
    {
        var result = await _mediator.Send(query);

        if (result.IsFailure)
        {
            return NotFound(result.Error);
        }

        return Ok(result.Value);
    }


}
