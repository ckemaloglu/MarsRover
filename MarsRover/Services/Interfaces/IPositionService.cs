using MarsRover.Contracts.Requests;
using MarsRover.Object;
using System.Threading.Tasks;

namespace MarsRover.Services.Interfaces
{
  public interface IPositionService
  {
    Task<BaseResponse<string>> MovingAsync(RoverRequest roverRequest);
    Task<BaseResponse<string>> ValidateRequestAsync(RoverRequest roverRequest);
  }
}
