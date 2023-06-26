using UnityEngine;

namespace Mingo.Base.Editor
{
  public static class BackgroundStyle
  {
    private static GUIStyle style;
    private static Texture2D texture;
 
 
    public static GUIStyle Get(Color color)
    {
      if (!texture)
      {
        texture = new Texture2D(1, 1);
      }
      if (style == null)
      {
        style = new GUIStyle();
      }
      texture.SetPixel(0, 0, color);
      texture.Apply();
      style.normal.background = texture;
      return style;
    }
  }
}