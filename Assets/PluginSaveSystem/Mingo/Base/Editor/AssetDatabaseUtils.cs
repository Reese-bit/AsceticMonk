using UnityEngine;

namespace Mingo.Base.Editor
{
  public static class AssetDatabaseUtils
  {
    public static string GetRelativePath(string fullPath)
    {
      return "Assets" + fullPath[Application.dataPath.Length];
    }
  }
}