using MarsRover.Contracts.Requests;
using MarsRover.Extensions;
using MarsRover.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Net;
using System.Threading.Tasks;

namespace MarsRover.Controllers
{
  [ApiController]
  [Route("Rover")]
  public class MarsRoverController : ControllerBase
  {

    private IPositionService _positionService;
    public MarsRoverController(IPositionService positionService)
    {
      _positionService = positionService;
    }

    [HttpPost]
    [SwaggerResponse((int)HttpStatusCode.OK, "Return last rover position", typeof(RoverRequest))]
    [SwaggerResponse((int)HttpStatusCode.BadRequest, "returns bad request when request is not valid", typeof(BadRequestResult))]
    public async Task<IActionResult> MoveAsync(RoverRequest roverRequest)
    {
      //This code for log
      Console.WriteLine($"Start RoverRequest:{roverRequest.ToJson()}");

     var result = await _positionService.MovingAsync(roverRequest);
      if (result.HasError)
      {
        result.Result = null;
        return BadRequest(result);
      }

      Console.WriteLine($"End RoverRequest:{roverRequest.ToJson()}");
      return Ok(result);
    }

  }
}
