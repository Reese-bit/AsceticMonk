using UnityEditor;
using UnityEngine;

namespace Mingo.Base.Runtime
{
  public class MingoSettings : ScriptableObject
  {
    public static MingoSettings Get()
    {
      return AssetDatabase.LoadAssetAtPath<MingoSettings>("Assets/Mingo/Base/Runtime/MingoSettings.asset");
    }

    public string asepritePathMac;
    public string asepritePathWin;
  }
}