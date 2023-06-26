using UnityEngine;

namespace Mingo.Base.Runtime.Utils
{
  public static class ObjectUtils
  {
    public static void DestroyChildren(this Transform transform)
    {
      if (Application.isEditor && !Application.isPlaying)
      {
        for (var i = transform.childCount - 1; i >= 0; --i)
          Object.DestroyImmediate(transform.GetChild(i).gameObject);
      }
      else
      {
        foreach (Transform child in transform)
        {
          Object.Destroy(child.gameObject);
        }
      }
    }

    public static void DestroyChildren<T>(this Transform transform)
    {
      if (Application.isEditor && !Application.isPlaying)
      {
        for (var i = transform.childCount - 1; i >= 0; --i)
        {
          var child = transform.GetChild(i);
          if (child.TryGetComponent<T>(out var _))
          {
            Object.DestroyImmediate(child.gameObject);
          }
        }
      }
      else
      {
        foreach (Transform child in transform)
        {
          if (child.TryGetComponent<T>(out var _))
          {
            Object.Destroy(child.gameObject);
          }
        }
      }
    }
  }
}