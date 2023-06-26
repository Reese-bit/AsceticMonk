using System.Reflection;
using Mingo.Base.Runtime;
using UnityEditor;
using UnityEngine;

namespace Mingo.Base.Editor
{
  public static class MingoSettingsMenus
  {
    [MenuItem("Mingo/Settings")]
    public static void OpenSettings()
    {
      var mingoSettings = AssetDatabase.LoadAssetAtPath<MingoSettings>("Assets/Mingo/Base/Runtime/MingoSettings.asset");
      InspectTarget(mingoSettings);
    }

    private static void InspectTarget(Object target)
    {
      var inspectorType = typeof(UnityEditor.Editor).Assembly.GetType("UnityEditor.InspectorWindow");
      var inspectorInstance = ScriptableObject.CreateInstance(inspectorType) as EditorWindow;
      inspectorInstance.Show();
      var prevSelection = Selection.activeObject;
      Selection.activeObject = target;
      var isLocked = inspectorType.GetProperty("isLocked", BindingFlags.Instance | BindingFlags.Public);
      isLocked.GetSetMethod().Invoke(inspectorInstance, new object[] { true });
      Selection.activeObject = prevSelection;
    }
  }
}