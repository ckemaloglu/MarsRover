using FluentAssertions;
using MarsRover.Contracts.Requests;
using MarsRover.Services;
using NUnit.Framework;
using System.Threading.Tasks;

namespace MarsRover.Tests
{
  public class PositionServiceTests
  {
    PositionService _sut;

    [SetUp]
    public  void Setup()
    {
      _sut = new PositionService();
    }

    [Test]
    public async Task PositionService_When_RoverRequestIsNull_ShouldReturnError()
    {
 
      var result = await _sut.MovingAsync(null);

      result.HasError.Should().BeTrue();
    }

    [Test]
    public async Task PositionService_When_StartPositionIsNull_ShouldReturnError()
    {

      var roverRequest = new RoverRequest
      {
        Moves = "MRRMMRMRRM",
        StartPosition = null
      };
      var result = await _sut.MovingAsync(roverRequest);

      result.HasError.Should().BeTrue();
    }

    [Test]
    public async Task PositionService_When_X_0_ShouldReturnError()
    {
   
      var roverRequest = new RoverRequest
      {
        Moves = "MRRMMRMRRM",
        StartPosition = new StartPosition
        {
          X = 0,
          Y = 3,
          Direction = "E"
        }
      };
      var result = await _sut.MovingAsync(roverRequest);

      result.HasError.Should().BeTrue();
    }

    [Test]
    public async Task PositionService_When_Y_0_ShouldReturnError()
    {
      var roverRequest = new RoverRequest
      {
        Moves = "MRRMMRMRRM",
        StartPosition = new StartPosition
        {
          X = 1,
          Y = 0,
          Direction = "E"
        }
      };
      var result = await _sut.MovingAsync(roverRequest);

      result.HasError.Should().BeTrue();
    }

    [Test]
    public async Task PositionService_When_X_GreaterThan_5_ShouldReturnError()
    {
      var roverRequest = new RoverRequest
      {
        Moves = "MRRMMRMRRM",
        StartPosition = new StartPosition
        {
          X =6,
          Y = 3,
          Direction = "E"
        }
      };
      var result = await _sut.MovingAsync(roverRequest);

      result.HasError.Should().BeTrue();
    }

    [Test]
    public async Task PositionService_When_Y_GreaterThan_5_ShouldReturnError()
    {
      var roverRequest = new RoverRequest
      {
        Moves = "MRRMMRMRRM",
        StartPosition = new StartPosition
        {
          X = 1,
          Y = 6,
          Direction = "E"
        }
      };
      var result = await _sut.MovingAsync(roverRequest);

      result.HasError.Should().BeTrue();
    }

    [Test]
    public async Task PositionService_Direction_WrongCharacter_ShouldReturnError()
    {
      var roverRequest = new RoverRequest
      {
        Moves = "MRRMMRMRRM",
        StartPosition = new StartPosition
        {
          X = 1,
          Y = 6,
          Direction = "Z"
        }
      };
      var result = await _sut.MovingAsync(roverRequest);

      result.HasError.Should().BeTrue();
    }

    [Test]
    public async Task PositionService_12N_LMLMLMLMM_TrueStroy()
    {
      var roverRequest = new RoverRequest
      {
        Moves = "LMLMLMLMM",
        StartPosition = new StartPosition
        {
          X = 1,
          Y = 2,
          Direction = "N"
        }
      };
     var result= await _sut.MovingAsync(roverRequest);

      var actualOutput = $"X:1, Y:3 ,Direction: N";

      Assert.AreEqual(result.Result, actualOutput);
    }

    [Test]
    public async Task PositionService_33E_MRRMMRMRRM_TrueStroy()
    {
      var roverRequest = new RoverRequest
      {
        Moves = "MRRMMRMRRM",
        StartPosition = new StartPosition
        {
          X = 3,
          Y = 3,
          Direction = "E"
        }
      };
      var result = await _sut.MovingAsync(roverRequest);

      var actualOutput = $"X:2, Y:3 ,Direction: S";

      Assert.AreEqual(result.Result, actualOutput);
    }

    [Test]
    public async Task PositionService_33E_MMRMMRMRRM_TrueStroy()
    {
      var roverRequest = new RoverRequest
      {
        Moves = "MMRMMRMRRM",
        StartPosition = new StartPosition
        {
          X = 3,
          Y = 3,
          Direction = "E"
        }
      };
      var result = await _sut.MovingAsync(roverRequest);

      var actualOutput = $"X:5, Y:1 ,Direction: E";

      Assert.AreEqual(result.Result, actualOutput);
    }
  }
}