using Newtonsoft.Json;

namespace MarsRover.Extensions
{
  public static class ObjectExtensions
  {
    public static string ToJson(this object obj)
    {
      return JsonConvert.SerializeObject(obj);
    }

    public static bool ContainsAny(this string haystack, params string[] needles)
    {
      foreach (string needle in needles)
      {
        if (haystack.Contains(needle))
          return true;
      }

      return false;
    }
  }
}
