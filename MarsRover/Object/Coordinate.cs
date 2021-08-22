using MarsRover.Enums;

namespace MarsRover.Object
{
  public class Coordinate
  {
    public Coordinate()
    {
      X = 0;
      Y = 0;
      Direction = Directions.N;
    }
    public int X { get; set; }
    public int Y { get; set; }
    public Directions Direction { get; set; }
  }
}
