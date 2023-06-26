using System;
using System.Collections.Generic;

namespace Mingo.Base.Editor
{
  public static class Code
  {
    public static IEnumerable<Type> GetAllTypes() {
      foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
      {
        var name = assembly.GetName().Name;
        if (name.StartsWith("UnityEngine.") || 
            name.StartsWith("UnityEditor.") || 
            name.StartsWith("Unity.") ||
            name.StartsWith("System.") ||
            name.Equals("UnityEditor") ||
            name.Equals("UnityEngine") ||
            name.Equals("System") ||
            name.Equals("mscorlib") ||
            name.StartsWith("JetBrains.") ||
            name.StartsWith("nunit.framework") ||
            name.StartsWith("Mono.") ||
            name.StartsWith("com.unity.") ||
            name.StartsWith("Microsoft.")
           ) {
          continue;
        }
        foreach (var type in assembly.GetTypes()) {
          yield return type;
        }
      }
    }
  }
}