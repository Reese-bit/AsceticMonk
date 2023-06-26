using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Mingo.Base.Editor
{
  public static class MingoImportingManager
  {
    public static readonly List<string> AssetImportByRunning = new List<string>();

    static MingoImportingManager()
    {
      EditorApplication.update += OnUpdate;
    }

    private static void OnUpdate()
    {
      if (_reimporting) return;
      if (EditorApplication.isPlaying) return;
      if (AssetImportByRunning.Count > 0)
      {
        ReimportAllByRunning();
      }
    }

    private static bool _reimporting;

    public static void ReimportAllByRunning()
    {
      _reimporting = true;
      foreach (var s in AssetImportByRunning)
      {
        Debug.Log($"reimport {s}");
        var importer = AssetImporter.GetAtPath(s);
        if (importer)
        {
          importer.SaveAndReimport();
        }
      }
      
      AssetImportByRunning.Clear();
      _reimporting = false;
    }
  }
}