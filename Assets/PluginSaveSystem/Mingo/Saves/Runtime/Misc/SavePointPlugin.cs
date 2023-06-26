using Mingo.Base.Runtime.Extensions;
using UnityEngine.SceneManagement;

namespace Mingo.Saves.Runtime.Misc
{
  public class SavePointPlugin : SavePlugin
  {
    public string defaultSavePoint;
    
    public override string PluginId => "Mingo.SavePoint";
    
    protected override void OnSaveLoad(SaveFile file)
    {
      if (file.savePoint.IsNullOrWhitespace())
      {
        file.savePoint = defaultSavePoint;
      }
    }

    protected override void OnSceneLoad(SaveFile file, Scene scene)
    {
    }

    protected override void OnBeforeSave(SaveFile file)
    {
      
    }

    protected override void OnAfterSave(SaveFile file)
    {
    }
  }
}