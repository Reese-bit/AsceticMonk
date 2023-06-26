using System;
using System.Collections.Generic;
using Mingo.Base.Runtime.DataStruct;
using UnityEngine;

namespace Mingo.Saves.Runtime
{
  [Serializable]
  public class SaveFile
  {
    [NonSerialized]
    public int index;
    [NonSerialized]
    public bool exists;
    public long time;

    public string title;
    public string desc;
    public string cover;
    public string scene;
    public string savePoint;
    public PlayerPosition playerPosition;

    public SavePreferences preferences;
    public SerializableDictionaryBytesValue pluginData;

    public void FillDefaults()
    {
      if (preferences == null)
      {
        preferences = new SavePreferences();
      }

      if (pluginData == null)
      {
        pluginData = new SerializableDictionaryBytesValue();
      }
    }

    public byte[] GetPluginData(SavePlugin plugin)
    {
      if (pluginData.ContainsKey(plugin.PluginId))
      {
        return pluginData[plugin.PluginId];
      }

      return null;
    }

    public void SetPluginData(SavePlugin plugin, byte[] bytes)
    {
      pluginData[plugin.PluginId] = bytes;
    }

    [Serializable]
    public class PlayerPosition
    {
      public float x;
      public float y;

      public PlayerPosition(Vector2 pos)
      {
        x = pos.x;
        y = pos.y;
      }
    }
  }
}