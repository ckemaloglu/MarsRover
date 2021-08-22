namespace MarsRover.Object
{
  public class BaseResponse<T>
  {
    public bool HasError => !string.IsNullOrEmpty(Message);
    public string ErrorCode { get; set; }
    public string Message { get; set; }
    public T Result { get; set; }
  }
}
