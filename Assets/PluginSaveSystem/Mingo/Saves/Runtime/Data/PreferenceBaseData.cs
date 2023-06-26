using UnityEditor;
#if UNITY_EDITOR
using UnityEngine;
#endif

namespace Mingo.Saves.Runtime.Data
{
  public abstract class PreferenceBaseData : MonoBehaviour
  {
    [Header("Prefs id")] [HideLabel] [EditorButton(nameof(GeneratePrefsId))]
    public string id;

    public abstract object GetValue();

    public void GeneratePrefsId()
    {
      if (Application.isPlaying)
      {
        id = PreferencesUtils.RandomHumanId(transform);
      }
      else
      {
#if UNITY_EDITOR
        Debug.Log("generate");
        Undo.RecordObject(this, "Generate Prefs Id");
        id = PreferencesUtils.RandomHumanId(transform);
        EditorUtility.SetDirty(this);
#endif
      }
    }
  }
}