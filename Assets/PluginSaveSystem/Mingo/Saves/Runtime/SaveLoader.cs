using System.Collections;
using Mingo.Base.Runtime.Extensions;
using Mingo.Base.Runtime.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Mingo.Saves.Runtime
{
  public class SaveLoader : MonoBehaviour
  {
    public int index;
    public bool loadOnStart;
    public LoadSceneMode loadSceneMode;
    public string defaultScene;

    private void Start()
    {
      if (loadOnStart)
      {
        SaveManager.Instance.StartCoroutine(LoadAsync());
      }
    }

    public void Load(int index)
    {
      this.index = index;
      SaveManager.Instance.StartCoroutine(LoadAsync());
    }

    public IEnumerator LoadAsync()
    {
      SaveManager.Instance.Load(index);
      
      var current = SaveManager.Instance.Current;
      if (current.scene.IsNullOrWhitespace())
      {
        current.scene = defaultScene;
      }
      SceneUtils.LoadIfNotExists(current.scene, loadSceneMode);
      yield return new WaitForEndOfFrame();
      yield return new WaitForEndOfFrame();
      
      SaveManager.Instance.onSceneLoad.Invoke(SceneManager.GetSceneByName(current.scene));
      
      if (!current.savePoint.IsNullOrWhitespace())
      {
        var savePoint = SavePoint.Cache.ContainsKey(current.savePoint) ? SavePoint.Cache[current.savePoint] : null;
        if (savePoint)
        {
          savePoint.OnLoad();
        }
      }
      
    }
  }
}