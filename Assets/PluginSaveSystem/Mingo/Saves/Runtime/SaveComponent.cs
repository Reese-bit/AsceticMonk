using Mingo.Base.Runtime.Utils;
using UnityEngine;

namespace Mingo.Saves.Runtime
{
  public class SaveComponent : MonoBehaviour
  {
    protected virtual SaveFile ApplySaveFile()
    {
      var saveFile = SaveManager.Instance.Current;
      saveFile.scene = gameObject.scene.name;
      saveFile.savePoint = $"Save-{TimeUtils.NowMillis()}";
      saveFile.title = $"Save at {TimeUtils.Now()}";
      saveFile.desc = "";
      saveFile.cover = "";
      return saveFile;
    }
    
    public void Save()
    {
      ApplySaveFile();
      SaveManager.Instance.Save();
    }
  }
}