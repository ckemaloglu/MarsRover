using MarsRover.Contracts.Requests;
using MarsRover.Enums;
using MarsRover.Extensions;
using MarsRover.Object;
using MarsRover.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace MarsRover.Services
{
  public class PositionService : IPositionService
  {
    public int X { get; set; }
    public int Y { get; set; }
    public Directions Direction { get; set; }
    BaseResponse<string> response;
    public PositionService()
    {
      X = 0;
      Y = 0;
      Direction = Directions.N;
      response = new BaseResponse<string>();
    }

    private void ReturnLeft()
    {
      switch (this.Direction)
      {
        case Directions.N:
          this.Direction = Directions.W;
          break;
        case Directions.S:
          this.Direction = Directions.E;
          break;
        case Directions.E:
          this.Direction = Directions.N;
          break;
        case Directions.W:
          this.Direction = Directions.S;
          break;
        default:
          break;
      }
    }

    private void ReturnRight()
    {
      switch (this.Direction)
      {
        case Directions.N:
          this.Direction = Directions.E;
          break;
        case Directions.S:
          this.Direction = Directions.W;
          break;
        case Directions.E:
          this.Direction = Directions.S;
          break;
        case Directions.W:
          this.Direction = Directions.N;
          break;
        default:
          break;
      }
    }

    private void Continue()
    {
      switch (this.Direction)
      {
        case Directions.N:
          this.Y += 1;
          break;
        case Directions.S:
          this.Y -= 1;
          break;
        case Directions.E:
          this.X += 1;
          break;
        case Directions.W:
          this.X -= 1;
          break;
        default:
          break;
      }
    }

    public async Task<BaseResponse<string>> MovingAsync(RoverRequest roverRequest)
    {
      var validateResult = await ValidateRequestAsync(roverRequest);
      if (validateResult.HasError) { return validateResult; };

      SetCurrentPosition(roverRequest.StartPosition);
      Move(roverRequest.Moves);

      response.Result = $"X:{X}, Y:{Y} ,Direction: {Direction}";
      return await Task.FromResult(response);
    }

    private void SetCurrentPosition(StartPosition startPosition)
    {
      X = startPosition.X;
      Y = startPosition.Y;
      Direction = (Directions)Enum.Parse(typeof(Directions), startPosition.Direction);
    }

    private void Move(string moves)
    {

      foreach (var move in moves)
      {
        switch (move)
        {
          case 'M':
            this.Continue();
            break;
          case 'L':
            this.ReturnLeft();
            break;
          case 'R':
            this.ReturnRight();
            break;
          default:
            response.Message = $"You entered the wrong character. Example moves 'LMLMLMLMM'";
            break;
        }
      }
    }

    public async Task<BaseResponse<string>> ValidateRequestAsync(RoverRequest roverRequest)
    {
      if (roverRequest == null)
      { response.Message = "RoverRequest cannot be empty"; return await Task.FromResult(response); }

      if (roverRequest.StartPosition == null)
      { response.Message = "StartPositions cannot be empty"; return await Task.FromResult(response); }


      if (!roverRequest.StartPosition.Direction.ContainsAny("M", "N", "S", "E"))
      { response.Message = "Direction format wrong. Example directions 'S','M','N','E' "; return await Task.FromResult(response); }

      if (roverRequest.StartPosition.X <= 0 || roverRequest.StartPosition.X > 5)
      { response.Message = "StartPosition.X must be a value between 0 and 5 "; return await Task.FromResult(response); }

      if (roverRequest.StartPosition.Y <= 0 || roverRequest.StartPosition.Y > 5)
      { response.Message = "StartPosition.Y  must be a value between 0 and 5 "; return await Task.FromResult(response); }

      return await Task.FromResult(response);
    }
  }
}
