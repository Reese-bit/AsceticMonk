using System.Collections;
using System.ComponentModel;
using System.Linq;
using UnityEngine.SceneManagement;

namespace Mingo.Base.Runtime.Utils
{
  public static class SceneUtils
  {

    public static void LoadIfNotExists(string sceneName, LoadSceneMode mode)
    {
      var scene = SceneManager.GetSceneByName(sceneName);
      if (scene.isLoaded) return;
      SceneManager.LoadScene(sceneName, mode);
    }
    
    public static T FindInRoots<T>() where T : Component
    {
      T result = null;
      for (var i = 0; i < SceneManager.sceneCount; i++)
      {
        var scene = SceneManager.GetSceneAt(i);
        if (!scene.isLoaded) continue;
        var roots = scene.GetRootGameObjects();
        var found = roots.FirstOrDefault(o => o.TryGetComponent<T>(out result));
        if (found)
        {
          return result;
        }
      }

      return null;
    }
    
  }
}