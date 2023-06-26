using System;

namespace Mingo.Base.Runtime.Extensions
{
  public static class EnumExtensions
  {
    public static string Name<T>(this T value) where T : Enum
    {
      return Enum.GetName(value.GetType(), value);
    }
  }
}