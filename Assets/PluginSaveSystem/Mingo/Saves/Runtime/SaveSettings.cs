using UnityEngine;

namespace Mingo.Saves.Runtime
{
  
  [CreateAssetMenu(menuName = "Mingo/Save System/Save Settings")]
  public class SaveSettings : ScriptableObject
  {
    public ESaveTarget target;
    public int maxSaves;
    public string folder;
    public string fileName;
    public string password;

  }
}