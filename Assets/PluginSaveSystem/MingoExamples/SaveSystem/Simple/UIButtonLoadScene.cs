using System.Collections;
using Mingo.Base.Runtime.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MingoExamples.SaveSystem.Simple
{
  public class UIButtonLoadScene : MonoBehaviour
  {
    public string sceneName;
    public LoadSceneMode loadMode;
    public bool unloadCurrent;

    public void Load()
    {
      StartCoroutine(LoadAsync());
    }

    private IEnumerator LoadAsync()
    {
      if (unloadCurrent)
      {
        yield return SceneManager.UnloadSceneAsync(gameObject.scene);
      }
      SceneUtils.LoadIfNotExists(sceneName, loadMode);
    }
  }
}