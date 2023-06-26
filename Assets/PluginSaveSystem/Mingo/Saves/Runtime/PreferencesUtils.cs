using Mingo.Base.Runtime.Extensions;
using UnityEngine;

namespace Mingo.Saves.Runtime
{
  public static class PreferencesUtils 
  {
    public static string RandomHumanId(Transform transform)
    {
      var parent = transform;
      var idStr = "";
      while (true)
      {
        if (idStr.IsNullOrWhitespace())
        {
          idStr = parent.name;
        }
        else
        {
          idStr = parent.name + "/" + idStr;
        }

        parent = parent.parent;
        if (!parent)
        {
          break;
        }
      }

      idStr = transform.gameObject.scene.name + ":" + idStr;
      return idStr;
    }
  }
}