using UnityEngine;
using UnityEngine.SceneManagement;

namespace Mingo.Saves.Runtime
{
  [RequireComponent(typeof(SaveManager))]
  public abstract class SavePlugin : MonoBehaviour
  {
    public abstract string PluginId { get; }
    
    internal virtual void InitListeners()
    {
      SaveManager.Instance.onLoad.AddListener(OnSaveLoadInternal);
      SaveManager.Instance.onSceneLoad.AddListener(OnSceneLoadInternal);
      SaveManager.Instance.onBeforeSave.AddListener(OnBeforeSaveInternal);
      SaveManager.Instance.onAfterSave.AddListener(OnAfterSaveInternal);
    }

    private void OnSceneLoadInternal(Scene scene)
    {
      OnSceneLoad(SaveManager.Instance.Current, scene);
    }

    private void OnAfterSaveInternal()
    {
      OnAfterSave(SaveManager.Instance.Current);
    }

    private void OnBeforeSaveInternal()
    {
      OnBeforeSave(SaveManager.Instance.Current);
    }

    private void OnSaveLoadInternal()
    {
      OnSaveLoad(SaveManager.Instance.Current);
    }

    protected abstract void OnSaveLoad(SaveFile file);
    protected abstract void OnSceneLoad(SaveFile file, Scene scene);
    protected abstract void OnBeforeSave(SaveFile file);
    protected abstract void OnAfterSave(SaveFile file);
  }
}