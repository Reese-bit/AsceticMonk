using System.Collections.Generic;
using Mingo.Base.Runtime.Extensions;
using UnityEditor;
using UnityEngine;

namespace Mingo.Saves.Runtime.Data
{
  public class PreferencesData<T> : PreferenceBaseData
  {
    
    public T initial;
    [Disable]
    public T value;

    private Dictionary<string, T> _preferences;
    
    protected virtual void Awake()
    {
      if (id.IsNullOrWhitespace())
      {
        GeneratePrefsId();
      }
      
      var saveMgr = SaveManager.Instance;
      var valueType = typeof(T);
      if (valueType == typeof(int))
      {
        _preferences = saveMgr.Current.preferences.ints as Dictionary<string, T>;
      } else if (valueType == typeof(bool))
      {
        _preferences = saveMgr.Current.preferences.bools as Dictionary<string, T>;
      } else if (valueType == typeof(string))
      {
        _preferences = saveMgr.Current.preferences.strings as Dictionary<string, T>;
      }
      value = Exists() ? _preferences[id] : initial;
    }

    public bool Exists()
    {
      return _preferences.ContainsKey(id);
    }

    public void Set(T value)
    {
      this.value = value;
      _preferences[id] = value;
    }

    public override object GetValue()
    {
      return value;
    }
  }
}